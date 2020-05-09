using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Redbridge.Reflection
{
public static class PropertyInfoExtensions
{
	public static bool HasAttribute<TAttribute>(this PropertyInfo property)
	{
		if (property == null) throw new ArgumentNullException("type");
		return property.GetCustomAttributes(typeof(TAttribute), true).Any();
	}

	public static TAttribute GetAttribute<TAttribute>(this PropertyInfo property)
	{
		if (property == null) throw new ArgumentNullException("type");
		return property.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().Single();
	}

	public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this PropertyInfo property)
	{
		if (property == null) throw new ArgumentNullException("type");
		return property.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>();
	} }
}
