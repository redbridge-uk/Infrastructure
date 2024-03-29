﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Redbridge.Configuration;
using Redbridge.Security;
using Redbridge.Web.Messaging;

namespace Redbridge.Identity.OAuthServer
{
    public class OAuthUsernamePasswordAuthenticationClient : OAuthRefreshAuthenticationClient
    {
        private string _username;
        private string _password;

        private readonly IHashingService _hashingService;

        public OAuthUsernamePasswordAuthenticationClient(IConfiguration settings, ILogger<OAuthUsernamePasswordAuthenticationClient> logger, IHashingService hashingService, IHttpClientFactory clientFactory) 
            : base(settings, logger, clientFactory) 
        {
            _hashingService = hashingService ?? throw new ArgumentNullException(nameof(hashingService));
        }

        protected override string ClientSettingsIdentifier => "UsernamePasswordClient";

        protected override void OnSetCredentials(UserCredentials credentials)
        {
			if (credentials == null)throw new ArgumentNullException(nameof(credentials));
            Logger.LogInformation($"OAuth client: setting credentials for {credentials.Username}");
            base.OnSetCredentials(credentials);
			_username = credentials.Username;
			_password = credentials.Password;
        }

        public override string AuthenticationMethod => RegistrationMethods.UsernamePassword;
        public override string Username => _username;
        public override string ClientType => ClientTypeId;
        public static string ClientTypeId = "oauth2:UsernamePassword";


		protected override async Task<Stream> OnSaveAsync()
		{
			Logger.LogInformation("Saving authentication settings...");
			var memoryStream = new MemoryStream();
			var settings = JsonConvert.SerializeObject(new AuthSettings() { RefreshToken = RefreshToken, AuthenticationType = AuthenticationMethod, Username = _username, PasswordHash = _hashingService.CreateHash(_password) });
			var writer = new StreamWriter(memoryStream);
			writer.AutoFlush = true;
			await writer.WriteAsync(settings);
			return memoryStream;
		}

        public async Task RefreshAccessTokenAsync ()
        {
            if (CanRefresh)
            {
                Logger.LogInformation($"Discovered refresh token...reconnecting to service at url {ServiceUri} using refresh token...");
                await OnRefreshAccessTokenAsync();
            }
            else
            {
                Logger.LogDebug("Authentication client has been set with credentials requiring a password.");
            }
        }

        protected override async Task OnBeginLoginAsync()
        {
            if ( CanRefresh )
            {
                Logger.LogInformation($"Discovered refresh token...reconnecting to service at url {ServiceUri} using refresh token...");
				await OnRefreshAccessTokenAsync();
            }
            else
            {
                Logger.LogInformation($"Connecting to service at url {ServiceUri} as user {Username} (Client Id {ClientId})");
                var uri = new Uri(ServiceUri, "oauth/token");
                var request = new FormWebRequest<OAuthTokenResult>(uri, HttpVerb.Post);
                var data = new OAuthAccessTokenRequestData() { ClientId = ClientId, ClientSecret = ClientSecret, Email = _username, Password = _password, GrantType = GrantTypes.Password };
                var token = await request.ExecuteAsync(ClientFactory, data.AsDictionary());

                if (token != null )
                {
                    if (!string.IsNullOrWhiteSpace(token.AccessToken) && !string.IsNullOrWhiteSpace(token.RefreshToken))
                    {
                        Logger.LogDebug($"Refreshed access token: {token.AccessToken}");
                        Logger.LogDebug($"Refresh token: {token.RefreshToken}");
                        SetRefreshToken(token.RefreshToken);
                        SetAccessToken(token.AccessToken);
                        SetStatus(ClientConnectionStatus.Connected);
                        SetupExpiry(token.ExpiresInSeconds);
                    }
                    else
                    {
                        Logger.LogWarning("The login process returned a response from the oauth service with either an access token or refresh token missing - disconnecting.");
                        Logger.LogDebug($"Refreshed access token: {token.AccessToken}");
                        Logger.LogDebug($"Refresh token: {token.RefreshToken}");
                        SetStatus(ClientConnectionStatus.Disconnected);
                    }
                }
                else
                {
                    Logger.LogWarning("The login process returned no response from the oauth service - disconnecting.");
                    SetStatus(ClientConnectionStatus.Disconnected);
                }
            }
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
					var token = UserCredentials.FromRefreshToken(RefreshToken, AuthenticationMethod);
                    token.Username = result.Username;
                    return token;
				}

                Logger.LogWarning($"Unable to load authentication settings from the supplied store. Returning empty credentials.");
				return UserCredentials.Empty(AuthenticationMethod);
			}
		}
    }
}
