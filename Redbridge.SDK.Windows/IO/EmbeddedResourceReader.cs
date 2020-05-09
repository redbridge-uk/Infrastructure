﻿using System;
using System.IO;
using System.Reflection;

namespace Redbridge.IO
{
public class EmbeddedResourceReader
{
	public EmbeddedResourceReader() : this(Assembly.GetCallingAssembly()) { }

	public EmbeddedResourceReader(string rootPath) : this(Assembly.GetCallingAssembly(), rootPath) { }

	public EmbeddedResourceReader(Assembly sourceAssembly) : this(sourceAssembly, string.Empty) { }

	public EmbeddedResourceReader(Assembly sourceAssembly, string rootPath)
	{
		if (sourceAssembly == null)
			throw new ArgumentNullException(nameof(sourceAssembly), "The source assembly is not permitted to be null.");

		SourceAssembly = sourceAssembly;
		RootPath = rootPath;
	}

	public string RootPath
	{
		get;
		set;
	}

	private Assembly SourceAssembly
	{
		get;
	}

	private string BuildResourcePath(string resourcePath)
	{
		string fullPath;
		string assemblyPath = SourceAssembly.GetName().Name;

		if (!string.IsNullOrWhiteSpace(RootPath))
		{
			if (!RootPath.StartsWith(assemblyPath, StringComparison.Ordinal))
				fullPath = $"{assemblyPath}.{RootPath}.{resourcePath}";
			else
				fullPath = $"{RootPath}.{resourcePath}";
		}
		else
		{
			if (resourcePath.StartsWith(assemblyPath, StringComparison.Ordinal))
				fullPath = resourcePath;
			else
				fullPath = $"{assemblyPath}.{resourcePath}";
		}

		return fullPath;
	}

	public string ReadContent(string resourcePath)
	{
		return GetStream(resourcePath).AsString();
	}

	public Stream GetStream(string resourcePath)
	{
		var fullPath = BuildResourcePath(resourcePath);
		var stream = SourceAssembly.GetManifestResourceStream(fullPath);
		return stream;
	} }
}
