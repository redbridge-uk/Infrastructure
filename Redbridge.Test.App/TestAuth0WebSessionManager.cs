using System;
using Redbridge.Diagnostics;
using Redbridge.Identity;
using Redbridge.SDK;

namespace TesterApp
{
	public class TestAuth0WebSessionManager : WebRequestSignatureService
	{
		public TestAuth0WebSessionManager(IAuthenticationClient client, ILogger logger) : base(logger, new Uri("http://smuggoat-api.azurewebsites.net"), client){}
	}
}
