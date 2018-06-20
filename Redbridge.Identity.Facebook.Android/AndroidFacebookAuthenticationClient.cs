using System;
using System.IO;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Redbridge.Configuration;
using Redbridge.Diagnostics;
using Redbridge.Security;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

namespace Redbridge.Identity.Facebook.iOS
{
    public class AndroidFacebookAuthenticationClient : IAuthenticationClient
    {
        private readonly IApplicationSettingsRepository _settings;
        private readonly ILogger _logger;
        private readonly BehaviorSubject<ClientConnectionStatus> _status = new BehaviorSubject<ClientConnectionStatus>(ClientConnectionStatus.Disconnected);
        private AccessToken _accessToken;
        private LoginManager _manager = LoginManager.Instance;

        public AndroidFacebookAuthenticationClient(IApplicationSettingsRepository settings, ILogger logger)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.WriteDebug("Created instance of Android Facebook Authentication client.");
        }


        public bool IsConnected => _accessToken != null && !_accessToken.IsExpired;

        public string Username => _accessToken.UserId;

        public string AccessToken => _accessToken.Token;


        public IObservable<ClientConnectionStatus> ConnectionStatus => _status;

        public ClientConnectionStatus CurrentConnectionStatus => _status.Value;

        public System.Threading.Tasks.Task BeginLoginAsync()
        {
            _logger.WriteDebug("Beginning login for iOS Facebook Authentication client...");
            _status.OnNext(ClientConnectionStatus.Connecting);

            //var vc = Android.App.SharedApplication.KeyWindow.RootViewController; TODO: get this for android.

            //_manager.LogInWithReadPermissions(null, new string[] { "public_profile" });

            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task LogoutAsync()
        {
            _manager.LogOut();
            return System.Threading.Tasks.Task.CompletedTask;
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
            throw new NotSupportedException("You cannot directly set credentials on the facebook oauth client.");
        }

        public string ClientType => ClientTypeId;

        public static string ClientTypeId = "Facebook";
    }
}
