using System;
using Redbridge.SDK;

namespace Redbridge.Forms
{
	public class ViewFactoryException : RedbridgeException
	{
		public ViewFactoryException() { }

		public ViewFactoryException(string message) : base(message) { }

		public ViewFactoryException(string message, Exception inner) : base(message, inner) { }
	}
}
