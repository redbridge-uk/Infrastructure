using System;
namespace Redbridge.SDK
{
	public class UserNotAuthenticatedException : RedbridgeException
	{
		public UserNotAuthenticatedException() { }

		public UserNotAuthenticatedException(string message) : base(message) { }

		public UserNotAuthenticatedException(string message, Exception inner) : base(message, inner) { }
	}
}
