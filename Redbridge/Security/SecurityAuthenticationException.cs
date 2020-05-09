using System;

namespace Redbridge.Security
{
	public class SecurityAuthenticationException : RedbridgeException
	{
		public SecurityAuthenticationException() { }

		public SecurityAuthenticationException(string message)
			: base(message) { }

		public SecurityAuthenticationException(string message, Exception innerException)
			: base(message, innerException) { }
	}
}
