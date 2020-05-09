using System;
using System.Net;
using System.Net.Http;
using Redbridge.Identity;

namespace Redbridge.Web.Messaging
{
    public interface IWebRequestSignatureService : IDisposable
	{
        IAuthenticationClient Authority { get; }
		void SignRequest(HttpClient client, AuthenticationMethod method = AuthenticationMethod.Bearer);
        void SignRequest(WebClient client, AuthenticationMethod method = AuthenticationMethod.Bearer);
        string PostProcessUrlString(string queriedUriString);
	}
}
