using System;
using Redbridge.SDK;

namespace Redbridge.Messaging
{
	public class InvalidSubjectException : RedbridgeException
	{
		public InvalidSubjectException() { }

		public InvalidSubjectException(string message) : base(message) { }

		public InvalidSubjectException(string message, Exception inner) : base(message, inner) { }
	}
}
