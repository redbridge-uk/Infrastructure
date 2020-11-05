using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Redbridge.Configuration;
using Redbridge.Diagnostics;
using Redbridge.Security;
using Redbridge.Web.Messaging;

namespace Redbridge.Identity.OAuthServer
{
    public abstract class OAuthRefreshAuthenticationClient : AuthenticationClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private IDisposable _tokenExpiredObservable;
		private string _accessToken;
		private string _refreshToken;
		private readonly string _clientId;
		private readonly string _clientSecret;
		private readonly Uri _serviceUri;

		protected OAuthRefreshAuthenticationClient(IApplicationSettingsRepository settings, ILogger logger, IHttpClientFactory clientFactory) : base(settings, logger)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _clientId = settings.GetStringValue("ClientId");
			_clientSecret = settings.GetStringValue("ClientSecret");
			_serviceUri = settings.GetUrl("AuthorisationServiceUrl");
		}

        protected Uri ServiceUri => _serviceUri;
        public override string AccessToken => _accessToken;
        protected string RefreshToken => _refreshToken;
        protected string ClientId => _clientId;
        protected string ClientSecret => _clientSecret;
        protected IHttpClientFactory ClientFactory => _clientFactory;

        public bool CanRefresh => !string.IsNullOrWhiteSpace(_refreshToken);

        protected override void OnSetCredentials(UserCredentials credentials)
        {
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            Logger.WriteDebug($"Setting credentials against OAuth base refresh client username:{credentials.Username ?? "Anonymous"}, refresh:{credentials.RefreshToken}");
            CancelRefresh();
			ClearTokens();
			base.OnSetCredentials(credentials);
            _refreshToken = credentials.RefreshToken;
            if ( CanRefresh )
            {
                Logger.WriteDebug("Authentication client has been set with credentials with a refresh token to use.");
            }
            else
            {
                Logger.WriteDebug("Authentication client has been set with credentials requiring a password.");
            }
        }

        protected void ClearTokens()
        {
            Logger.WriteDebug("Authentication client has cleared access and refresh tokens.");
			_accessToken = null;
			_refreshToken = null;
        }

        protected void SetAccessToken (string token)
        {
            Logger.WriteDebug("Authentication client stored access token.");
            _accessToken = token;
        }

        protected void SetRefreshToken (string token)
        {
            Logger.WriteDebug("Authentication client stored refresh token.");
            _refreshToken = token;
        }

		protected override Task OnLogoutAsync()
		{
            Logger.WriteDebug("Authentication client is beginning logout process...");
			_accessToken = null;
            CancelRefresh();
            return base.OnLogoutAsync();
		}

		protected void SetupExpiry(int expiresInSeconds)
		{
            Logger.WriteDebug("Authentication client is configuring refresh token expiry...");

			if (_tokenExpiredObservable != null)
			{
                Logger.WriteDebug("Authentication client is cancelling previous expiry timer...");
				_tokenExpiredObservable.Dispose();
				_tokenExpiredObservable = null;
			}

			if (expiresInSeconds > 0)
			{
                Logger.WriteDebug($"Authentication client is configuring expiry timer from requested {expiresInSeconds} seconds...");
				var eagerExpiryTime = Convert.ToInt32((95M / 100M) * expiresInSeconds);
				Logger.WriteInfo($"{ClientType} token expiry set as {expiresInSeconds} seconds from now. Setting local expiry to just before {eagerExpiryTime} seconds...");
				_tokenExpiredObservable = Observable.Timer(TimeSpan.FromSeconds(eagerExpiryTime)).Take(1)
													.Subscribe(async (l) =>
				{
					Logger.WriteInfo("Access token has expired requesting refresh...");
					await OnRefreshAccessTokenAsync();
				});
			}
			else
			{
				Logger.WriteInfo("The expiry time of the access token is 0 seconds. Unable to configure timeout.");
			}
		}

		protected async Task OnRefreshAccessTokenAsync()
		{
            SetStatus(ClientConnectionStatus.Refreshing);
            Logger.WriteDebug($"Refreshing token at service url {_serviceUri} as user {Username} token: {_refreshToken}...");
			var uri = new Uri(_serviceUri, "oauth/token");
			var request = new FormWebRequest<OAuthTokenResult>(uri, HttpVerb.Post);
			var data = new OAuthRefreshTokenAccessTokenRequestData() { ClientId = _clientId, ClientSecret = _clientSecret, RefreshToken = _refreshToken };
            Logger.WriteDebug($"Refresh request client id: {_clientId}");
            Logger.WriteDebug($"Refresh request client id: {_clientSecret?.Substring(0, 5)}XXXX");
            Logger.WriteDebug($"Refresh request client id: {_refreshToken}");
			var token = await request.ExecuteAsync(_clientFactory, data.AsDictionary());

            if (token != null)
            {
                Logger.WriteDebug($"Successfully received a response from the token service.");

                if (!string.IsNullOrEmpty(token.AccessToken) && !string.IsNullOrEmpty(token.RefreshToken))
                {
                    _refreshToken = token.RefreshToken;
                    _accessToken = token.AccessToken;
                    Logger.WriteDebug("Token refresh successful, configuring new expiry.");
                    SetupExpiry(token.ExpiresInSeconds);
                    SetStatus(ClientConnectionStatus.Connected);
                }
                else
                {
                    Logger.WriteWarning($"The refresh response from the token service returned either no access token or no refresh token, this is considered a failure.");
                    Logger.WriteWarning($"Refreshed access token: {token.AccessToken}");
                    Logger.WriteWarning($"Refresh token: {token.RefreshToken}");
                    CancelRefresh();
                    SetStatus(ClientConnectionStatus.Disconnected);
                }
            }
			else
			{
                Logger.WriteWarning("No token returned from refresh token request.");
                CancelRefresh();
				SetStatus(ClientConnectionStatus.Disconnected);
			}
		}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            CancelRefresh();
            _tokenExpiredObservable?.Dispose();
            ClearTokens();

        }

        private void CancelRefresh ()
        {
            if (_tokenExpiredObservable != null)
            {
                Logger.WriteInfo("Cancelling existing refresh process...");
                _tokenExpiredObservable.Dispose();
            }
        }

		protected override async Task<Stream> OnSaveAsync()
		{
            Logger.WriteInfo("Saving authentication settings...");
			var memoryStream = new MemoryStream();
			var settings = JsonConvert.SerializeObject(new AuthSettings() { RefreshToken = RefreshToken, AuthenticationType = AuthenticationMethod });
            var writer = new StreamWriter(memoryStream) {AutoFlush = true};
            await writer.WriteAsync(settings);
			return memoryStream;
		}

		protected override async Task<UserCredentials> OnLoadAsync(Stream stream)
		{
            Logger.WriteInfo("Loading authentication settings...");

			if (stream.CanSeek)
			{
				stream.Seek(0, SeekOrigin.Begin);
			}

			using (var reader = new StreamReader(stream))
			{
				var json = await reader.ReadToEndAsync();
				var result = JsonConvert.DeserializeObject<AuthSettings>(json);
				ClearTokens();
				if (result != null && !string.IsNullOrWhiteSpace(result.RefreshToken))
				{
                    Logger.WriteInfo("Refresh token found in authentication settings file. Using refresh token method.");
					SetRefreshToken(result.RefreshToken);
					return UserCredentials.FromRefreshToken(RefreshToken, AuthenticationMethod);
				}

                Logger.WriteWarning("Refresh token failed to deserialize from store returning an empty instance of the credentials for oauth.");
				return UserCredentials.Empty(AuthenticationMethod);
			}
		}
    }
}
