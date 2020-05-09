using System;

namespace Redbridge.Exceptions
{
	public class UserNotAuthenticatedException : RedbridgeException
	{
		public UserNotAuthenticatedException() { }

		public UserNotAuthenticatedException(string message) : base(message) { }

		public UserNotAuthenticatedException(string message, Exception inner) : base(message, inner) { }
	}
}
