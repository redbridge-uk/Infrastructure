using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Redbridge.Security;

namespace Redbridge.Identity
{
    public class AnonymousAuthenticationClient : IAuthenticationClient
    {
        public string AuthenticationMethod => "None";

        public string Username => string.Empty;

        public string AccessToken => string.Empty;

        public string ClientType => "Anonymous";

        public bool IsConnected => true;

        public IObservable<ClientConnectionStatus> ConnectionStatus => Observable.Return(ClientConnectionStatus.Connected);

        public ClientConnectionStatus CurrentConnectionStatus => ClientConnectionStatus.Connected;

        public Task BeginLoginAsync()
        {
            return Task.CompletedTask;
        }

        public Task<UserCredentials> LoadAsync(Stream stream)
        {
            return Task.FromResult(UserCredentials.Empty(AuthenticationMethod));
        }

        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }

        public Task<Stream> SaveAsync()
        {
            return Task.FromResult((Stream)new MemoryStream());
        }

        public void SetCredentials(UserCredentials credentials) { }

        protected Task OnBeginLoginAsync()
        {
            return Task.CompletedTask;
        }

        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
            }
            _disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
