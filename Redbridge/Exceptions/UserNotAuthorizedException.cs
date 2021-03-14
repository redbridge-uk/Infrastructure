using System;
using System.Runtime.Serialization;

namespace Redbridge.Exceptions
{
    public class UserNotAuthorizedException : RedbridgeException
	{
		public UserNotAuthorizedException() { }

		public UserNotAuthorizedException(string message) : base(message) { }

		public UserNotAuthorizedException(string message, Exception inner) : base(message, inner) { }

        protected UserNotAuthorizedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
	}
}
