using System;
using System.IO;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Auth0.Windows;
using Redbridge.Configuration;
using Redbridge.Security;

namespace Redbridge.Identity.Auth0.Windows
{
    public class Auth0CodeClient : IAuthenticationClient
    {
        private readonly IApplicationSettingsRepository _settings;
        private readonly Auth0Client _client;
        private Auth0User _currentUser;
        private readonly BehaviorSubject<ClientConnectionStatus> _status = new BehaviorSubject<ClientConnectionStatus>(ClientConnectionStatus.Disconnected);

        public Auth0CodeClient(IApplicationSettingsRepository settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            _settings = settings;
            _client = new Auth0Client(_settings.GetStringValue("auth0:domain"), _settings.GetStringValue("auth0:clientid"));
        }

        public async Task BeginLoginAsync()
        {
            _currentUser = await _client.LoginAsync("Username-Password-Authentication", Username, Password);

            if (_currentUser != null)
            {
                _status.OnNext(ClientConnectionStatus.Connected);
            }
            else
                _status.OnNext(ClientConnectionStatus.Disconnected);
        }

        public string ClientType => "Auth0";
        public string Username { get; set; }
        public string Password { get; set; }

		public Task LogoutAsync()
		{
			_client.Logout();
            return Task.FromResult(true);
		}

        public Task<Stream> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserCredentials> LoadAsync(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void SetCredentials(UserCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool IsConnected => _currentUser != null;
		public string AccessToken => _currentUser?.Auth0AccessToken;

		public IObservable<ClientConnectionStatus> ConnectionStatus => _status;
		public ClientConnectionStatus CurrentConnectionStatus => _status.Value;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _status.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Auth0CodeClient() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
