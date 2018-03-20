using System;
using System.Threading.Tasks;
using Redbridge.Configuration;
using Redbridge.Diagnostics;
using Redbridge.SDK;
using Redbridge.Web.Messaging;

namespace Redbridge.Identity.OAuthServer
{
    public class OAuthNonInteractiveAuthenticationClient : OAuthRefreshAuthenticationClient
    {
		public OAuthNonInteractiveAuthenticationClient(IApplicationSettingsRepository settings, ILogger logger) : base(settings, logger){}

		public override string Username => string.Empty;
		public override string ClientType => ClientTypeId;
        public override string AuthenticationMethod => RegistrationMethods.NonInteractiveDaemon;
        public static string ClientTypeId = "oauth2:ClientSecret";

        protected override async Task OnBeginLoginAsync()
        {
			if (!string.IsNullOrWhiteSpace(RefreshToken))
			{
				Logger.WriteInfo($"Discovered refresh token...reconnecting to service at url {ServiceUri}...");
				await OnRefreshAccessTokenAsync();
			}
			else
			{
				Logger.WriteInfo($"Connecting to service at url {ServiceUri} as non-interactive client {ClientId})");
				var uri = new Uri(ServiceUri, "oauth/token");
				var request = new FormServiceRequest<OAuthTokenResult>(uri, HttpVerb.Post);
				var data = new OAuthAccessTokenRequestData() { ClientId = ClientId, ClientSecret = ClientSecret, GrantType = GrantTypes.ClientSecret };
				var token = await request.ExecuteAsync(data.AsDictionary());

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
					Logger.WriteWarning("The login process returned no response from the oauth service.");
					SetStatus(ClientConnectionStatus.Disconnected);
				}
			}
        }
    }
}
