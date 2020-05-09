using System;
using System.IO;
using System.Reflection;

namespace Redbridge.IO
{

public class IOException : RedbridgeException
{
	public IOException() { }

	public IOException(string message) : base(message) { }

	public IOException(string message, Exception innerException) : base(message, innerException) { }	}

}
