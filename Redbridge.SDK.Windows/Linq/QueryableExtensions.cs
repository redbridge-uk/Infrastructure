using System;
using System.Linq;
using System.Linq.Expressions;
using Redbridge.SDK;

namespace Redbridge.Windows.Linq
{
	public static class QueryableExtensions
	{
		private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
		{
			ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
			MemberExpression property = Expression.PropertyOrField(param, propertyName);
			LambdaExpression sort = Expression.Lambda(property, param);

			MethodCallExpression call = Expression.Call(
				typeof(Queryable),
				(!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
				new[] { typeof(T), property.Type },
				source.Expression,
				Expression.Quote(sort));

			return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
		}

		public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool descending)
		{
			return OrderingHelper(source, propertyName, descending, false);
		}

		public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
		{
			return OrderingHelper(source, propertyName, false, false);
		}

		public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
		{
			return OrderingHelper(source, propertyName, true, false);
		}

		public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName, bool descending)
		{
			return OrderingHelper(source, propertyName, descending, true);
		}

		public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
		{
			return OrderingHelper(source, propertyName, false, true);
		}

		public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
		{
			return OrderingHelper(source, propertyName, true, true);
		}

		public static IQueryable<T> Where<T>(this IQueryable<T> query, string column, object value, WhereOperation operation)
		{
			if (string.IsNullOrEmpty(column))
				return query;

			ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");

			MemberExpression memberAccess = null;
			foreach (var property in column.Split('.'))
				memberAccess = Expression.Property
				   (memberAccess ?? (parameter as Expression), property);

			//change param value type necessary to getting bool from string
			ConstantExpression filter = Expression.Constant(ConvertType(memberAccess.Type, value.ToString().ToLowerInvariant()));

			//switch operation
			Expression condition = null;
			LambdaExpression lambda = null;
			Expression toString = Expression.Call(memberAccess, "ToString", null, null);
			Expression filterString = Expression.Call(filter, "ToString", null, null);
			Expression toLower = Expression.Call(toString, "ToLower", null, null);

			switch (operation)
			{
				//equal ==
				case WhereOperation.Equal:
					condition = Expression.Equal(toLower, filterString);
					lambda = Expression.Lambda(condition, parameter);
					break;

				//not equal !=
				case WhereOperation.NotEqual:
					condition = Expression.NotEqual(memberAccess, filterString);
					lambda = Expression.Lambda(condition, parameter);
					break;

				//string.StartsWith()
				case WhereOperation.StartsWith:
					condition = Expression.Call(toLower, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), filterString);
					lambda = Expression.Lambda(condition, parameter);
					break;

				case WhereOperation.EndsWith:
					condition = Expression.Call(toLower, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), filterString);
					lambda = Expression.Lambda(condition, parameter);
					break;

				//string.Contains()
				case WhereOperation.Contains:
					Expression containsLower = Expression.Call(memberAccess, "ToLower", null, null);
					condition = Expression.Call(containsLower, typeof(string).GetMethod("Contains"), filterString);
					lambda = Expression.Lambda(condition, parameter);
					break;
			}

			MethodCallExpression result = Expression.Call(
				   typeof(Queryable), "Where",
				   new[] { query.ElementType },
				   query.Expression,
				   lambda);

			return query.Provider.CreateQuery<T>(result);
		}

		private static object ConvertType(Type type, object value)
		{
			var converted = value;

			if (type == typeof(Guid))
			{
				return Guid.Parse(value.ToString());
			}

			if (type == typeof(bool?))
			{
				if (value != null)
					return bool.Parse(value.ToString());

				return null;
			}

			if (type == typeof(int?))
			{
				if (value != null)
					return (int?)(int.Parse(value.ToString()));

				return null;
			}

			return Convert.ChangeType(converted, type);
		}
	}
}
