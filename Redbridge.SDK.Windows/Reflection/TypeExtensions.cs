using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Redbridge.Linq.Expressions;

namespace Redbridge.Windows.Reflection
{
	public static class TypeExtensions
	{
		public static bool HasAttribute<TAttribute>(this Type type)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			return type.GetCustomAttributes(typeof(TAttribute), true).Any();
		}

		public static TAttribute GetAttribute<TAttribute>(this Type type)
		{
			return type.GetAttribute<TAttribute>(true);
		}

		public static TAttribute GetAttribute<TAttribute>(this Type type, bool includeInheritedClasses)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			return type.GetCustomAttributes(typeof(TAttribute), includeInheritedClasses).Cast<TAttribute>().Single();
		}

		public static bool TryGetAttribute<TAttribute>(this Type type, out TAttribute attribute)
		{
			return type.TryGetAttribute(true, out attribute);
		}

		public static bool TryGetAttribute<TAttribute>(this Type type, bool includeInheritedClasses, out TAttribute attribute)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			attribute = type.GetCustomAttributes(typeof(TAttribute), includeInheritedClasses).Cast<TAttribute>().SingleOrDefault();

			return attribute != null;
		}

		public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type type)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			return type.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>();
		}

		public static MethodInfo GetMethod<T>(Expression<Func<object>> expression)
		{
			string methodName = expression.GetPropertyName();
			return typeof(T).GetMethod(methodName);
		}
	}
}
