using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redbridge.Configuration;
using Redbridge.Diagnostics;
using Redbridge.Web.Messaging;

namespace Redbridge.Identity.OAuthServer
{
    public class OAuthNonInteractiveAuthenticationClient : OAuthRefreshAuthenticationClient
    {
		public OAuthNonInteractiveAuthenticationClient(IApplicationSettingsRepository settings, ILogger logger, IHttpClientFactory clientFactory) 
            : base(settings, logger, clientFactory){}

		public override string Username => string.Empty;
		public override string ClientType => ClientTypeId;
        public override string AuthenticationMethod => RegistrationMethods.NonInteractiveDaemon;
        public static string ClientTypeId = "oauth2:ClientSecret";

        protected override async Task OnBeginLoginAsync()
        {
			if (!string.IsNullOrWhiteSpace(RefreshToken))
			{
				Logger.LogInformation($"Discovered refresh token...reconnecting to service at url {ServiceUri}...");
				await OnRefreshAccessTokenAsync();
			}
			else
			{
				Logger.LogInformation($"Connecting to service at url {ServiceUri} as non-interactive client {ClientId})");
				var uri = new Uri(ServiceUri, "oauth/token");
				var request = new FormWebRequest<OAuthTokenResult>(uri, HttpVerb.Post);
				var data = new OAuthAccessTokenRequestData() { ClientId = ClientId, ClientSecret = ClientSecret, GrantType = GrantTypes.ClientSecret };
				var token = await request.ExecuteAsync(ClientFactory, data.AsDictionary());

				if (token != null)
				{
					SetRefreshToken(token.RefreshToken);
					SetAccessToken(token.AccessToken);

					if (!string.IsNullOrWhiteSpace(AccessToken))
					{
						SetStatus(ClientConnectionStatus.Connected);
						SetupExpiry(token.ExpiresInSeconds);
					}
					else
						SetStatus(ClientConnectionStatus.Disconnected);
				}
				else
				{
					Logger.LogWarning("The login process returned no response from the oauth service.");
					SetStatus(ClientConnectionStatus.Disconnected);
				}
			}
        }
    }
}
