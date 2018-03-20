using System;
using Redbridge.SDK;

namespace Redbridge.Forms
{
	public class ViewModelFactoryException : RedbridgeException
	{
		public ViewModelFactoryException() { }

		public ViewModelFactoryException(string message) : base(message) { }

		public ViewModelFactoryException(string message, Exception inner) : base(message, inner) { }
	}
}
