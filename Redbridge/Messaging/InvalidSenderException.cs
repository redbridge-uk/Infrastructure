using System;

namespace Redbridge.Messaging
{
	public class InvalidSenderException : RedbridgeException
	{
		public InvalidSenderException() { }

		public InvalidSenderException(string message) : base(message) { }

		public InvalidSenderException(string message, Exception inner) : base(message, inner) { }
	}
}
