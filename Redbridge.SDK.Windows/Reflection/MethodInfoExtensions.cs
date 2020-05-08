using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Redbridge.Windows.Reflection
{
public static class MethodInfoExtensions
{
	public static bool HasAttribute<TAttribute>(this MethodInfo type)
	{
		if (type == null)
		{
			throw new ArgumentNullException(nameof(type));
		}

		return type.GetCustomAttributes(typeof(TAttribute), true).Any();
	}


	public static TAttribute GetAttribute<TAttribute>(this MethodInfo type)
	{
		if (type == null)
		{
			throw new ArgumentNullException(nameof(type));
		}

		return type.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().SingleOrDefault();
	}

	public static bool TryGetAttribute<TAttribute>(this MethodInfo type, out TAttribute attribute)
	{
		if (type == null)
		{
			throw new ArgumentNullException(nameof(type));
		}

		attribute = type.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().SingleOrDefault();

		return attribute != null;
	}

	public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MethodInfo type)
	{
		if (type == null)
		{
			throw new ArgumentNullException(nameof(type));
		}

		return type.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>();
	}

	public static bool TryGetAttributes<TAttribute>(this MethodInfo type, out IEnumerable<TAttribute> attributes)
	{
		if (type == null)
		{
			throw new ArgumentNullException(nameof(type));
		}

		attributes = type.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>();

		return attributes != null;
	} }
}
