using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Redbridge.Data;

namespace Redbridge.Linq
{
	public static class PageRequestExtensions
	{
		public static IQueryable<T> GetTotalRecordCountQuery<T>(this PageRequest pageRequest, IQueryable<T> source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			// If a filter has been supplied...
			if (pageRequest.Filter != null)
				source = pageRequest.Filter.CreateQuery(source);

			return source;
		}

		public static IQueryable<T> CreateQuery<T>(this PageRequest pageRequest, IQueryable<T> source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			// If a filter has been supplied...
			if (pageRequest.Filter != null)
				source = pageRequest.Filter.CreateQuery(source);

            source = source.Distinct();

			// If no size has been defined, then we simply return all of the source without a query.
			if (pageRequest.Size.HasValue)
			{
				if (pageRequest.Sort != null && pageRequest.Sort.Any())
				{
					if (pageRequest.Skip > 0)
						return Queryable.Take(Queryable.Skip(ApplySort(pageRequest, source), pageRequest.Skip), pageRequest.Size.Value);

					if (pageRequest.Skip == 0)
						return Queryable.Take(ApplySort(pageRequest, source), pageRequest.Size.Value);
				}
				else
				{
					if (pageRequest.Skip > 0 && !pageRequest.SkipHandledExternally)
						throw new NotSupportedException(
						"Unable to skip records without a sort supplied. Provide at least a single sort field.");

					if (pageRequest.Skip == 0)
						return Queryable.Take(ApplySort(pageRequest, source), pageRequest.Size.Value);
				}
			}
			else
			{
				if (pageRequest.Sort != null && pageRequest.Sort.Any())
				{
					if (pageRequest.Skip > 0)
						return Queryable.Skip(ApplySort(pageRequest, source), pageRequest.Skip);

					if (pageRequest.Skip == 0)
						return ApplySort(pageRequest, source);
				}
				else
				{
					if (pageRequest.Skip > 0)
						throw new NotSupportedException(
						"Unable to skip records without a sort supplied. Provide at least a single sort field.");
				}
			}

			return source;
		}

		public static IQueryable<T> ApplySort<T>(this PageRequest pageRequest, IQueryable<T> source)
		{
			IQueryable<T> orderedQueryable = source;

			if (pageRequest.Sort != null)
			{
				foreach (var sortProperty in pageRequest.Sort)
				{
					if (sortProperty.SortDirection == SortDirection.Ascending)
					{
						orderedQueryable = OrderByProperty(source, sortProperty.Property.ToTitleCase());
					}
					else
					{
						orderedQueryable = OrderByPropertyDescending(source, sortProperty.Property.ToTitleCase());
					}
				}
			}

			return orderedQueryable;
		}

		public static IQueryable<TSource> OrderByProperty<TSource>(IQueryable<TSource> source, string propertyName)
		{
			var sourceType = typeof(TSource);
			var parameter = Expression.Parameter(sourceType, "item");
			var propertyInfo = GetProperty(sourceType, propertyName);
			var orderByProperty = Expression.Property(parameter, propertyInfo);
			var orderBy = Expression.Lambda(orderByProperty, new[] { parameter });
			return Queryable.OrderBy((dynamic)source, (dynamic)orderBy);
		}

		private static PropertyInfo GetProperty(Type type, string propertyName)
		{
			var prop = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (prop == null)
			{
				var baseTypesAndInterfaces = new List<Type>();
				if (type.BaseType != null) baseTypesAndInterfaces.Add(type.BaseType);
				baseTypesAndInterfaces.AddRange(type.GetInterfaces());
				foreach (var t in baseTypesAndInterfaces)
				{
					prop = GetProperty(t, propertyName);
					if (prop != null)
						break;
				}
			}
			return prop;
		}

		public static IQueryable<TSource> OrderByPropertyDescending<TSource>(IQueryable<TSource> source, string propertyName)
		{
			var sourceType = typeof(TSource);
			var parameter = Expression.Parameter(sourceType, "item");
			var propertyInfo = GetProperty(sourceType, propertyName);
			var orderByProperty = Expression.Property(parameter, propertyInfo);
			var orderBy = Expression.Lambda(orderByProperty, new[] { parameter });
			return Queryable.OrderByDescending((dynamic)source, (dynamic)orderBy);
		}
	}
}
