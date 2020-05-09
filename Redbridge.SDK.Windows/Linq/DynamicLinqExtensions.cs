using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Redbridge.Linq
{
	public static class DynamicQueryable
	{
		public static IQueryable<T> Where<T>(this IQueryable<T> source, string predicate, params object[] values)
		{
			return (IQueryable<T>)Where((IQueryable)source, predicate, values);
		}

		public static IQueryable Where(this IQueryable source, string predicate, params object[] values)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (predicate == null) throw new ArgumentNullException("predicate");
			LambdaExpression lambda = DynamicExpression.ParseLambda(source.ElementType, typeof(bool), predicate, values);
			return source.Provider.CreateQuery(
				Expression.Call(
					typeof(Queryable), "Where",
					new Type[] { source.ElementType },
					source.Expression, Expression.Quote(lambda)));
		}

		public static IQueryable Select(this IQueryable source, string selector, params object[] values)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (selector == null) throw new ArgumentNullException("selector");
			LambdaExpression lambda = DynamicExpression.ParseLambda(source.ElementType, null, selector, values);
			return source.Provider.CreateQuery(
				Expression.Call(
					typeof(Queryable), "Select",
					new Type[] { source.ElementType, lambda.Body.Type },
					source.Expression, Expression.Quote(lambda)));
		}

		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
		{
			return (IQueryable<T>)OrderBy((IQueryable)source, ordering, values);
		}

		public static IQueryable OrderBy(this IQueryable source, string ordering, params object[] values)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (ordering == null) throw new ArgumentNullException("ordering");

			var parameters = new[] { Expression.Parameter(source.ElementType, "") };

			var parser = new ExpressionParser(parameters, ordering, values);
			var orderings = parser.ParseOrdering();
			var queryExpr = source.Expression;
			var methodAsc = "OrderBy";
			var methodDesc = "OrderByDescending";

			foreach (DynamicOrdering o in orderings)
			{
				queryExpr = Expression.Call(
					typeof(Queryable), o.Ascending ? methodAsc : methodDesc,
					new[] { source.ElementType, o.Selector.Type },
					queryExpr, Expression.Quote(Expression.Lambda(o.Selector, parameters)));
				methodAsc = "ThenBy";
				methodDesc = "ThenByDescending";
			}
			return source.Provider.CreateQuery(queryExpr);
		}

		public static IQueryable Take(this IQueryable source, int count)
		{
			if (source == null) throw new ArgumentNullException("source");
			return source.Provider.CreateQuery(
				Expression.Call(
					typeof(Queryable), "Take",
					new[] { source.ElementType },
					source.Expression, Expression.Constant(count)));
		}

		public static IQueryable Skip(this IQueryable source, int count)
		{
			if (source == null) throw new ArgumentNullException("source");
			return source.Provider.CreateQuery(
				Expression.Call(
					typeof(Queryable), "Skip",
					new[] { source.ElementType },
					source.Expression, Expression.Constant(count)));
		}

		public static IQueryable GroupBy(this IQueryable source, string keySelector, string elementSelector, params object[] values)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (keySelector == null) throw new ArgumentNullException("keySelector");
			if (elementSelector == null) throw new ArgumentNullException("elementSelector");
			LambdaExpression keyLambda = DynamicExpression.ParseLambda(source.ElementType, null, keySelector, values);
			LambdaExpression elementLambda = DynamicExpression.ParseLambda(source.ElementType, null, elementSelector, values);
			return source.Provider.CreateQuery(
				Expression.Call(
					typeof(Queryable), "GroupBy",
					new Type[] { source.ElementType, keyLambda.Body.Type, elementLambda.Body.Type },
					source.Expression, Expression.Quote(keyLambda), Expression.Quote(elementLambda)));
		}

		public static bool Any(this IQueryable source)
		{
			if (source == null) throw new ArgumentNullException("source");
			return (bool)source.Provider.Execute(
				Expression.Call(
					typeof(Queryable), "Any",
					new Type[] { source.ElementType }, source.Expression));
		}

		public static int Count(this IQueryable source)
		{
			if (source == null) throw new ArgumentNullException("source");
			return (int)source.Provider.Execute(
				Expression.Call(
					typeof(Queryable), "Count",
					new[] { source.ElementType }, source.Expression));
		}
	}

	public abstract class DynamicClass
	{
		public override string ToString()
		{
			var props = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var sb = new StringBuilder();
			sb.Append("{");
			for (int i = 0; i < props.Length; i++)
			{
				if (i > 0) sb.Append(", ");
				sb.Append(props[i].Name);
				sb.Append("=");
				sb.Append(props[i].GetValue(this, null));
			}
			sb.Append("}");
			return sb.ToString();
		}
	}

	public static class DynamicExpression
	{
		public static Expression Parse(Type resultType, string expression, params object[] values)
		{
			var parser = new ExpressionParser(null, expression, values);
			return parser.Parse(resultType);
		}

		public static LambdaExpression ParseLambda(Type itType, Type resultType, string expression, params object[] values)
		{
			return ParseLambda(new[] { Expression.Parameter(itType, "") }, resultType, expression, values);
		}

		public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
		{
			var parser = new ExpressionParser(parameters, expression, values);
			return Expression.Lambda(parser.Parse(resultType), parameters);
		}

		public static Expression<Func<T, S>> ParseLambda<T, S>(string expression, params object[] values)
		{
			return (Expression<Func<T, S>>)ParseLambda(typeof(T), typeof(S), expression, values);
		}

		public static Type CreateClass(params DynamicProperty[] properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}

		public static Type CreateClass(IEnumerable<DynamicProperty> properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}
	}

	internal class DynamicOrdering
	{
		public Expression Selector;
		public bool Ascending;
	}

	internal class Signature : IEquatable<Signature>
	{
		public DynamicProperty[] properties;
		public int hashCode;

		public Signature(IEnumerable<DynamicProperty> properties)
		{
			this.properties = properties.ToArray();
			hashCode = 0;
			foreach (DynamicProperty p in properties)
			{
				hashCode ^= p.Name.GetHashCode() ^ p.Type.GetHashCode();
			}
		}

		public override int GetHashCode()
		{
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			return obj is Signature ? Equals((Signature)obj) : false;
		}

		public bool Equals(Signature other)
		{
			if (properties.Length != other.properties.Length) return false;
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].Name != other.properties[i].Name ||
					properties[i].Type != other.properties[i].Type) return false;
			}
			return true;
		}
	}

	static class Res
	{
		public const string DuplicateIdentifier = "The identifier '{0}' was defined more than once";
		public const string ExpressionTypeMismatch = "Expression of type '{0}' expected";
		public const string ExpressionExpected = "Expression expected";
		public const string InvalidCharacterLiteral = "Character literal must contain exactly one character";
		public const string InvalidIntegerLiteral = "Invalid integer literal '{0}'";
		public const string InvalidRealLiteral = "Invalid real literal '{0}'";
		public const string UnknownIdentifier = "Unknown identifier '{0}'";
		public const string NoItInScope = "No 'it' is in scope";
		public const string IifRequiresThreeArgs = "The 'iif' function requires three arguments";
		public const string FirstExprMustBeBool = "The first expression must be of type 'Boolean'";
		public const string BothTypesConvertToOther = "Both of the types '{0}' and '{1}' convert to the other";
		public const string NeitherTypeConvertsToOther = "Neither of the types '{0}' and '{1}' converts to the other";
		public const string MissingAsClause = "Expression is missing an 'as' clause";
		public const string ArgsIncompatibleWithLambda = "Argument list incompatible with lambda expression";
		public const string TypeHasNoNullableForm = "Type '{0}' has no nullable form";
		public const string NoMatchingConstructor = "No matching constructor in type '{0}'";
		public const string AmbiguousConstructorInvocation = "Ambiguous invocation of '{0}' constructor";
		public const string CannotConvertValue = "A value of type '{0}' cannot be converted to type '{1}'";
		public const string NoApplicableMethod = "No applicable method '{0}' exists in type '{1}'";
		public const string MethodsAreInaccessible = "Methods on type '{0}' are not accessible";
		public const string MethodIsVoid = "Method '{0}' in type '{1}' does not return a value";
		public const string AmbiguousMethodInvocation = "Ambiguous invocation of method '{0}' in type '{1}'";
		public const string UnknownPropertyOrField = "No property or field '{0}' exists in type '{1}'";
		public const string NoApplicableAggregate = "No applicable aggregate method '{0}' exists";
		public const string CannotIndexMultiDimArray = "Indexing of multi-dimensional arrays is not supported";
		public const string InvalidIndex = "Array index must be an integer expression";
		public const string NoApplicableIndexer = "No applicable indexer exists in type '{0}'";
		public const string AmbiguousIndexerInvocation = "Ambiguous invocation of indexer in type '{0}'";
		public const string IncompatibleOperand = "Operator '{0}' incompatible with operand type '{1}'";
		public const string IncompatibleOperands = "Operator '{0}' incompatible with operand types '{1}' and '{2}'";
		public const string UnterminatedStringLiteral = "Unterminated string literal";
		public const string InvalidCharacter = "Syntax error '{0}'";
		public const string DigitExpected = "Digit expected";
		public const string SyntaxError = "Syntax error";
		public const string TokenExpected = "{0} expected";
		public const string ParseExceptionFormat = "{0} (at index {1})";
		public const string ColonExpected = "':' expected";
		public const string OpenParenExpected = "'(' expected";
		public const string CloseParenOrOperatorExpected = "')' or operator expected";
		public const string CloseParenOrCommaExpected = "')' or ',' expected";
		public const string DotOrOpenParenExpected = "'.' or '(' expected";
		public const string OpenBracketExpected = "'[' expected";
		public const string CloseBracketOrCommaExpected = "']' or ',' expected";
		public const string IdentifierExpected = "Identifier expected";
	}
}
