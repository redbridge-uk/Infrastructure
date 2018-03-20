using System;
namespace Redbridge.SDK
{
	public class ValidationException : RedbridgeException
	{
		public ValidationException() { }

		public ValidationException(string message) : base(message) { }

		public ValidationException(string message, Exception inner) : base(message, inner) { }
	}
}
