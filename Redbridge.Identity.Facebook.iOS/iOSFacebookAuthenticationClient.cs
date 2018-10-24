using System;
using System.IO;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using Redbridge.Configuration;
using Redbridge.Diagnostics;
using Redbridge.Security;
using UIKit;

namespace Redbridge.Identity.Facebook.iOS
{
    public class iOSFacebookAuthenticationClient : IAuthenticationClient, IExternalAuthenticationProvider
    {
        private readonly IApplicationSettingsRepository _settings;
        private readonly ILogger _logger;
        private readonly BehaviorSubject<ClientConnectionStatus> _status = new BehaviorSubject<ClientConnectionStatus>(ClientConnectionStatus.Disconnected);
        private AccessToken _accessToken;
        private LoginManager _manager = new LoginManager();

        public iOSFacebookAuthenticationClient(IApplicationSettingsRepository settings, ILogger logger)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.WriteDebug("Created instance of iOS Facebook Authentication client.");
        }

        protected IApplicationSettingsRepository Settings => _settings;

        public virtual bool IsConnected => _accessToken != null && !_accessToken.IsExpired;

        public virtual string Username => _accessToken.UserID;

        public virtual string AccessToken => _accessToken.TokenString;

        public IObservable<ClientConnectionStatus> ConnectionStatus => _status;

        public ClientConnectionStatus CurrentConnectionStatus => _status.Value;

        public System.Threading.Tasks.Task BeginLoginAsync()
        {
            _logger.WriteDebug("Beginning login for iOS Facebook Authentication client...");
            _status.OnNext(ClientConnectionStatus.Connecting);

            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            _manager.LogInWithReadPermissions(new string[] { "public_profile" },
                                                 vc,
                                                 (result, error) =>
                                                 {
                                                     if (error == null && !result.IsCancelled)
                                                     {
                    
                                                         _accessToken = result.Token;
                                                          OnProcessSignInAsync(result, error);
                                                         _status.OnNext(ClientConnectionStatus.Connected);
                                                     }
                                                 });
            return System.Threading.Tasks.Task.CompletedTask;
        }

        protected virtual System.Threading.Tasks.Task OnProcessSignInAsync(LoginManagerLoginResult result, NSError error) 
        {
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task LogoutAsync()
        {
            _manager.LogOut();
            OnProcessLogOut();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        protected virtual System.Threading.Tasks.Task OnProcessLogOut() 
        {
            return System.Threading.Tasks.Task.CompletedTask;            
        }

        public virtual Task<Stream> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<UserCredentials> LoadAsync(Stream stream)
        {
            throw new NotImplementedException();
        }

        public virtual void SetCredentials(UserCredentials credentials)
        {
            throw new NotSupportedException("You cannot directly set credentials on the facebook oauth client.");
        }

        public string ClientType => ClientTypeId;
        public string ProviderName => "Facebook";
        public static string ClientTypeId = "Facebook";

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _status?.Dispose();
                    _manager?.Dispose();
                    _accessToken?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~iOSFacebookAuthenticationClient() {
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
