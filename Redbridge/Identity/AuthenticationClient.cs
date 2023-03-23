using System;
using System.IO;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Redbridge.Configuration;
using Redbridge.Security;

namespace Redbridge.Identity
{

    public abstract class AuthenticationClient : IAuthenticationClient
    {
		private readonly BehaviorSubject<ClientConnectionStatus> _status = new BehaviorSubject<ClientConnectionStatus>(ClientConnectionStatus.Disconnected);
        private readonly IConfiguration _settings;

        protected ILogger Logger { get; set; }

        public static IAuthenticationClient Anonymous => new AnonymousAuthenticationClient();

        protected AuthenticationClient(IConfiguration settings, ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}

        public void SetCredentials (UserCredentials credentials)
        {
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            OnSetCredentials(credentials);
        }

        public abstract string AuthenticationMethod { get; }

        protected virtual void OnSetCredentials (UserCredentials credentials) {}

        protected void SetStatus (ClientConnectionStatus status)
        {
            Logger.LogDebug($"Authentication client status changing to: {status}");
            _status.OnNext(status);
        }

        public virtual bool IsConnected => _status.Value == ClientConnectionStatus.Connected;

        public abstract string Username { get; }

        public abstract string AccessToken { get; }

        public abstract string ClientType { get; }

        public IObservable<ClientConnectionStatus> ConnectionStatus => _status;

        public ClientConnectionStatus CurrentConnectionStatus => _status.Value;

        public Task BeginLoginAsync()
        {
            Logger.LogInformation($"Beginning login process for client type {ClientType} for user {Username ??  "[Anonymous]"}...");
            SetStatus(ClientConnectionStatus.Connecting);
            return OnBeginLoginAsync();
        }

        protected abstract Task OnBeginLoginAsync();

        public async Task LogoutAsync()
        {
            Logger.LogInformation($"Logging out client type {ClientType}...");
            await OnLogoutAsync();
            SetStatus(ClientConnectionStatus.Disconnected);
        }

        protected virtual Task OnLogoutAsync()
        {
            return Task.CompletedTask;
        }

        public Task<Stream> SaveAsync()
        {
            Logger.LogInformation($"Saving security state for client type {ClientType}...");
            return OnSaveAsync();
        }

        protected virtual Task<Stream> OnSaveAsync ()
        {
            var stream = new MemoryStream();
            return Task.FromResult((Stream)stream);
        }

        public Task<UserCredentials> LoadAsync(Stream stream)
        {
            Logger.LogInformation($"Restoring security state for client type {ClientType}...");
            return OnLoadAsync(stream);
        }

        protected virtual Task<UserCredentials> OnLoadAsync (Stream stream)
        {
			if (stream == null) throw new ArgumentNullException(nameof(stream));
            return Task.FromResult(UserCredentials.New());
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _status?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AuthenticationClient() {
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
