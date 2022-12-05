using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redbridge.Identity;

namespace Redbridge.Web.Messaging
{
	public class WebRequestSignatureService : IWebRequestSignatureService
	{
        protected WebRequestSignatureService(ILogger logger, Uri serviceUri, IAuthenticationClient client)
		{
            Logger = logger;
			ServiceUri = serviceUri ?? throw new ArgumentNullException(nameof(serviceUri));
            Authority = client ?? throw new ArgumentNullException(nameof(client));
		}

		protected ILogger Logger { get; }

        public IAuthenticationClient Authority { get; }

        public void SignRequest(HttpClient client, AuthenticationMethod method = AuthenticationMethod.Bearer)
		{
			if (method != AuthenticationMethod.None)
			{
				if (Authority.IsConnected)
				{
					OnAssignAuthenticationMethod(client, method);
				}
				else
				{
					throw new WebProxyException(
						"Unable to sign request as the session manager is not currently connected and AutoConnect is false.");
				}
			}
		}

        public void SignRequest(WebClient client, AuthenticationMethod method = AuthenticationMethod.Bearer)
        {
            if (method != AuthenticationMethod.None)
            {
                if (Authority.IsConnected)
                {
                    OnAssignAuthenticationMethod(client, method);
                }
                else
                {
                    throw new WebProxyException(
                        "Unable to sign request as the session manager is not currently connected and AutoConnect is false.");
                }
            }
        }

        protected virtual void OnAssignAuthenticationMethod(WebClient client, AuthenticationMethod method)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            switch (method)
            {
                case AuthenticationMethod.Bearer:
                    client.Headers.Add(HeaderNames.Authorization,
                        BearerTokenFormatter.CreateToken(Authority.AccessToken));
                    break;

                case AuthenticationMethod.QueryString:
                    client.QueryString.Add(QueryStringParts.Authentication, Authority.AccessToken);
                    break;

                case AuthenticationMethod.PostParameter:
                    throw new NotSupportedException(
                        "Unable to add a post parameter authentication method to a web client.");
            }
        }

        protected virtual void OnAssignAuthenticationMethod(HttpClient client, AuthenticationMethod method)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			switch (method)
			{
				case AuthenticationMethod.Bearer:
					client.DefaultRequestHeaders.Add(HeaderNames.Authorization,
						BearerTokenFormatter.CreateToken(Authority.AccessToken));
					break;
					
				case AuthenticationMethod.QueryString:
					client.BaseAddress = client.BaseAddress.AddQueryParameter(QueryStringParts.Authentication, Authority.AccessToken);
					break;

				case AuthenticationMethod.PostParameter:
					throw new NotSupportedException(
						"Unable to add a post parameter authentication method to a web client.");
			}
		}

		public Uri ServiceUri { get; }

		public virtual string PostProcessUrlString(string queriedUriString) { return queriedUriString; }

		protected virtual Task OnAfterLogin() 
		{
			return Task.FromResult(true);
		}
        
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Authority?.Dispose();
                }
                
                _disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
    }
}
