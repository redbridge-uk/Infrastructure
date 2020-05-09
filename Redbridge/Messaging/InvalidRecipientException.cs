using System;

namespace Redbridge.Messaging
{
	public class InvalidRecipientException : RedbridgeException
	{
		public InvalidRecipientException() { }

		public InvalidRecipientException(string message) : base(message) { }

		public InvalidRecipientException(string message, Exception inner) : base(message, inner) { }
	}
}
