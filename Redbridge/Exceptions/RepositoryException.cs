using System;

namespace Redbridge.Exceptions
{
	public class RepositoryException : RedbridgeException
	{
		public RepositoryException() : base() { }
		public RepositoryException(string message) : base(message) { }
		public RepositoryException(string message, Exception exception) : base(message, exception) { }
	}
}
