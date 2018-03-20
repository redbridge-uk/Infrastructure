using System;
using System.IO;
using System.Reflection;
using Redbridge.SDK;

namespace Redbridge.IO
{

public class IOException : RedbridgeException
{
	public IOException() { }

	public IOException(string message) : base(message) { }

	public IOException(string message, Exception innerException) : base(message, innerException) { }	}

}
