using System;
using System.Net.Http;
using System.Threading.Tasks;
using Redbridge.Diagnostics;
using Redbridge.Identity;

namespace Redbridge.SDK
{
	public class WebRequestSignatureService : IWebRequestSignatureService
	{
        private readonly IAuthenticationClient _client;

		protected WebRequestSignatureService(ILogger logger, Uri serviceUri, IAuthenticationClient client)
		{
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (serviceUri == null) throw new ArgumentNullException(nameof(serviceUri));
			Logger = logger;
			ServiceUri = serviceUri;
            _client = client;
		}

		protected ILogger Logger { get; }

        public IAuthenticationClient Authority => _client;

		public void SignRequest(HttpClient client, AuthenticationMethod method = AuthenticationMethod.Bearer)
		{
			if (method != AuthenticationMethod.None)
			{
				if (_client.IsConnected)
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

		protected virtual void OnAssignAuthenticationMethod(HttpClient client, AuthenticationMethod method)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			switch (method)
			{
				case AuthenticationMethod.Bearer:
					client.DefaultRequestHeaders.Add(HeaderNames.Authorization,
						BearerTokenFormatter.CreateToken(_client.AccessToken));
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
	}
}
