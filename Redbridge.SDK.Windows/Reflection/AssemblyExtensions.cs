using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Redbridge.Reflection
{
public static class AssemblyExtensions
{
	public static bool HasAttribute<TAttribute>(this Assembly assembly)
	{
		if (assembly == null)
			throw new ArgumentNullException(nameof(assembly));

		return assembly.GetCustomAttributes(typeof(TAttribute), true).Any();
	}

	public static TAttribute GetSingleAttribute<TAttribute>(this Assembly assembly)
	{
		if (assembly == null)
			throw new ArgumentNullException(nameof(assembly));

		return assembly.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().Single();
	}

	public static IEnumerable<Type> GetAttributedTypes<TAttribute>(this Assembly assembly)
	{
		if (assembly == null)
		{
			throw new ArgumentNullException(nameof(assembly));
		}

		return assembly.GetTypes().Where(t => t.HasAttribute<TAttribute>());
	}

	public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Assembly assembly)
	{
		if (assembly == null)
			throw new ArgumentNullException(nameof(assembly));

		return assembly.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>();
	}

	public static TAttribute GetAttribute<TAttribute>(this Assembly assembly)
	{
		if (assembly == null)
			throw new ArgumentNullException(nameof(assembly));

		return assembly.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().SingleOrDefault();
	} }
}
