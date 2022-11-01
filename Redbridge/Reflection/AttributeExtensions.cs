using System;
using System.Collections.Generic;
using System.Reflection;

namespace Redbridge.Reflection
{
public static class AttributeExtensions
{
	public static bool HasAttribute<T>(this PropertyInfo propertyInfo, bool inherit) where T : class
	{
		object[] attributes = propertyInfo.GetCustomAttributes(typeof(T), inherit);

		return (attributes != null && attributes.Length > 0);
	}

	public static T GetAttribute<T>(this PropertyInfo propertyInfo, bool inherit) where T : class
	{
		object[] attributes = propertyInfo.GetCustomAttributes(typeof(T), inherit);

		if (attributes == null || attributes.Length == 0)
			throw new ApplicationException(string.Format("Property '{0}' is not attributed with type '{1}'", propertyInfo.Name, typeof(T)));
		else if (attributes.Length > 1)
			throw new ApplicationException(string.Format("Property '{0}' has been attributed with type '{1}' more than once", propertyInfo.Name, typeof(T)));
		else
			return (attributes[0] as T);
	}

	public static PropertyInfo[] AttributedProperties<T>(this Type t, bool inherit) where T : class
	{
		var attributedProperties = new List<PropertyInfo>();

		PropertyInfo[] properties = t.GetProperties();

		foreach (PropertyInfo property in properties)
			if (property.HasAttribute<T>(inherit))
				attributedProperties.Add(property);

		return attributedProperties.ToArray();
	} }
}
