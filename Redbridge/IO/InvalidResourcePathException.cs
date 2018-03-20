using System;
using System.IO;
using System.Reflection;
using Redbridge.SDK;

namespace Redbridge.IO
{

public class InvalidResourcePathException : IOException
{
	public InvalidResourcePathException() { }

	public InvalidResourcePathException(string message) : base(message) { }

	public InvalidResourcePathException(string message, Exception innerException) : base(message, innerException) { }	}

}
