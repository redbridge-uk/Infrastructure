using System;
using System.IO;
using System.Reflection;

namespace Redbridge.IO
{
	public abstract class ResourceReader
	{
		protected Stream GetResourceStream(Assembly assembly, string resourcePath)
		{
			if (assembly == null)
				throw new ArgumentNullException(nameof(assembly), "An assembly must be supplied.");

			if (string.IsNullOrWhiteSpace(resourcePath))
				throw new InvalidResourcePathException("You must supply a valid resource path, null or empty strings are not supported.");

			AssemblyName name = assembly.GetName();

			if (!resourcePath.StartsWith(name.Name, StringComparison.Ordinal))
				resourcePath = $"{name.Name}.{resourcePath}";

			return assembly.GetManifestResourceStream(resourcePath);
		}
	}
}
