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
            return Task.FromResult<UserCredentials>(UserCredentials.Empty(AuthenticationMethod));
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AnonymousAuthenticationClient() {
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
