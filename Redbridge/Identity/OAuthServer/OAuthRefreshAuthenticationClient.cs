﻿using System;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private Lazy<AuthenticationClientSettings> _settings;

		protected OAuthRefreshAuthenticationClient(IConfiguration settings, ILogger logger, IHttpClientFactory clientFactory) : base(settings, logger)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

            _settings = new Lazy<AuthenticationClientSettings>(() =>
            {
                var section = settings.GetRequiredSection(ClientSettingsIdentifier);
                var clientSettings = new AuthenticationClientSettings();
                section.Bind(clientSettings);
                return clientSettings;
            });
        }

        /// <summary>
        /// The identifier for the block in the appsettings that represents this client.
        /// </summary>
        protected abstract string ClientSettingsIdentifier { get; }
        protected Uri ServiceUri => new Uri(_settings.Value.AuthorisationServiceUrl);
        public override string AccessToken => _accessToken;
        protected string RefreshToken => _refreshToken;
        protected string ClientId => _settings.Value.ClientId;
        protected string ClientSecret => _settings.Value.ClientSecret;
        protected IHttpClientFactory ClientFactory => _clientFactory;

        public bool CanRefresh => !string.IsNullOrWhiteSpace(_refreshToken);

        protected override void OnSetCredentials(UserCredentials credentials)
        {
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            Logger.LogDebug($"Setting credentials against OAuth base refresh client username:{credentials.Username ?? "Anonymous"}, refresh:{credentials.RefreshToken}");
            CancelRefresh();
			ClearTokens();
			base.OnSetCredentials(credentials);
            _refreshToken = credentials.RefreshToken;
            if ( CanRefresh )
            {
                Logger.LogDebug("Authentication client has been set with credentials with a refresh token to use.");
            }
            else
            {
                Logger.LogDebug("Authentication client has been set with credentials requiring a password.");
            }
        }

        protected void ClearTokens()
        {
            Logger.LogDebug("Authentication client has cleared access and refresh tokens.");
			_accessToken = null;
			_refreshToken = null;
        }

        protected void SetAccessToken (string token)
        {
            Logger.LogDebug("Authentication client stored access token.");
            _accessToken = token;
        }

        protected void SetRefreshToken (string token)
        {
            Logger.LogDebug("Authentication client stored refresh token.");
            _refreshToken = token;
        }

		protected override Task OnLogoutAsync()
		{
            Logger.LogDebug("Authentication client is beginning logout process...");
			_accessToken = null;
            CancelRefresh();
            return base.OnLogoutAsync();
		}

		protected void SetupExpiry(int expiresInSeconds)
		{
            Logger.LogDebug("Authentication client is configuring refresh token expiry...");

			if (_tokenExpiredObservable != null)
			{
                Logger.LogDebug("Authentication client is cancelling previous expiry timer...");
				_tokenExpiredObservable.Dispose();
				_tokenExpiredObservable = null;
			}

			if (expiresInSeconds > 0)
			{
                Logger.LogDebug($"Authentication client is configuring expiry timer from requested {expiresInSeconds} seconds...");
				var eagerExpiryTime = Convert.ToInt32((95M / 100M) * expiresInSeconds);
				Logger.LogInformation($"{ClientType} token expiry set as {expiresInSeconds} seconds from now. Setting local expiry to just before {eagerExpiryTime} seconds...");
				_tokenExpiredObservable = Observable.Timer(TimeSpan.FromSeconds(eagerExpiryTime)).Take(1)
													.Subscribe(async (l) =>
				{
					Logger.LogInformation("Access token has expired requesting refresh...");
					await OnRefreshAccessTokenAsync();
				});
			}
			else
			{
				Logger.LogInformation("The expiry time of the access token is 0 seconds. Unable to configure timeout.");
			}
		}

		protected async Task OnRefreshAccessTokenAsync()
		{
            SetStatus(ClientConnectionStatus.Refreshing);
            Logger.LogDebug($"Refreshing token at service url {_settings.Value.AuthorisationServiceUrl} as user {Username} token: {_refreshToken}...");
			var uri = new Uri(new Uri(_settings.Value.AuthorisationServiceUrl), "oauth/token");
			var request = new FormWebRequest<OAuthTokenResult>(uri, HttpVerb.Post);
			var data = new OAuthRefreshTokenAccessTokenRequestData() { ClientId = _settings.Value.ClientId, ClientSecret = _settings.Value.ClientSecret, RefreshToken = _refreshToken };
            Logger.LogDebug($"Refresh request client id: {_settings.Value.ClientId}");
			var token = await request.ExecuteAsync(_clientFactory, data.AsDictionary());

            if (token != null)
            {
                Logger.LogDebug($"Successfully received a response from the token service.");

                if (!string.IsNullOrEmpty(token.AccessToken) && !string.IsNullOrEmpty(token.RefreshToken))
                {
                    _refreshToken = token.RefreshToken;
                    _accessToken = token.AccessToken;
                    Logger.LogDebug("Token refresh successful, configuring new expiry.");
                    SetupExpiry(token.ExpiresInSeconds);
                    SetStatus(ClientConnectionStatus.Connected);
                }
                else
                {
                    Logger.LogWarning("The refresh response from the token service returned either no access token or no refresh token, this is considered a failure.");
                    CancelRefresh();
                    SetStatus(ClientConnectionStatus.Disconnected);
                }
            }
			else
			{
                Logger.LogWarning("No token returned from refresh token request.");
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
                Logger.LogInformation("Cancelling existing refresh process...");
                _tokenExpiredObservable.Dispose();
            }
        }

		protected override async Task<Stream> OnSaveAsync()
		{
            Logger.LogInformation("Saving authentication settings...");
			var memoryStream = new MemoryStream();
			var settings = JsonConvert.SerializeObject(new AuthSettings() { RefreshToken = RefreshToken, AuthenticationType = AuthenticationMethod });
            var writer = new StreamWriter(memoryStream) {AutoFlush = true};
            await writer.WriteAsync(settings);
			return memoryStream;
		}

		protected override async Task<UserCredentials> OnLoadAsync(Stream stream)
		{
            Logger.LogInformation("Loading authentication settings...");

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
                    Logger.LogInformation("Refresh token found in authentication settings file. Using refresh token method.");
					SetRefreshToken(result.RefreshToken);
					return UserCredentials.FromRefreshToken(RefreshToken, AuthenticationMethod);
				}

                Logger.LogWarning("Refresh token failed to deserialize from store returning an empty instance of the credentials for oauth.");
				return UserCredentials.Empty(AuthenticationMethod);
			}
		}
    }
}
