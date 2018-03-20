using System;
using System.Linq.Expressions;

namespace Redbridge.Linq.Expressions
{
	public static class ExpressionExtensions
	{
		public static string GetPropertyName(this Expression<Func<object>> property)
		{
			string propertyName = null;
			MemberExpression memberExpression = null;
			if (property.Body is UnaryExpression)
			{
				memberExpression = (property.Body as UnaryExpression).Operand as MemberExpression;
			}
			else
			{
				memberExpression = property.Body as MemberExpression;
			}

			if (memberExpression != null)
			{
				propertyName = memberExpression.Member.Name;
			}
			else
			{
				throw new ArgumentException("Type of expression is not correct please ensure it is in this form () => this.AProperty");
			}

			return propertyName;
		}

		public static string GetPropertyName<TIn, TOut>(this Expression<Func<TIn, TOut>> property)
		{
			string propertyName = null;
			MemberExpression memberExpression = null;

			if (property.Body is UnaryExpression)
			{
				memberExpression = (property.Body as UnaryExpression).Operand as MemberExpression;
			}
			else
			{
				memberExpression = property.Body as MemberExpression;
			}

			if (memberExpression != null)
			{
				propertyName = memberExpression.Member.Name;
			}
			else
			{
				throw new ArgumentException("Type of expression is not correct please ensure it is in this form () => this.AProperty");
			}

			return propertyName;
		}
	}
}
