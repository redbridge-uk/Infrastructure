using System;
namespace Redbridge.SDK
{
    public class UserNotAuthorizedException : RedbridgeException
	{
		public UserNotAuthorizedException() { }

		public UserNotAuthorizedException(string message) : base(message) { }

		public UserNotAuthorizedException(string message, Exception inner) : base(message, inner) { }
	}
}
