using System;
namespace Redbridge.SDK
{
	public class WebProxyException : RedbridgeException
	{
		public WebProxyException()
		{
		}

		public WebProxyException(string message) : base(message)
		{
		}

		public WebProxyException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
