using System;
using Redbridge.SDK;

namespace Redbridge.Windows.Xml
{
	public class XmlSerializationException : RedbridgeException
	{
		public XmlSerializationException() : base() { }

		public XmlSerializationException(string message) : base(message) { }

		public XmlSerializationException(string message, Exception innerException) : base(message, innerException) { }
	}

}
