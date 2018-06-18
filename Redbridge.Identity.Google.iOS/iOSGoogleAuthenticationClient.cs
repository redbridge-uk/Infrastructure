using System;
using System.IO;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Foundation;
using Google.SignIn;
using Redbridge.Configuration;
using Redbridge.Diagnostics;
using Redbridge.Identity;
using Redbridge.Security;
using UIKit;

namespace Easilog.iOS
{
    public class iOSGoogleAuthenticationClient : SignInUIDelegate, ISignInDelegate, IAuthenticationClient
    {
        private readonly IApplicationSettingsRepository _settings;
        private GoogleUser _currentUser;
        private readonly ILogger _logger;
        private readonly BehaviorSubject<ClientConnectionStatus> _status = new BehaviorSubject<ClientConnectionStatus>(ClientConnectionStatus.Disconnected);

        public iOSGoogleAuthenticationClient(IApplicationSettingsRepository settings, ILogger logger)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _settings = settings;
            _logger = logger;

            _logger.WriteDebug("Created instance of iOS Google Authentication client.");
        }

        public Task BeginLoginAsync ()
        {
            _logger.WriteDebug("Beginning login for iOS Google Authentication client...");
            _status.OnNext(ClientConnectionStatus.Connecting);

            SignIn.SharedInstance.ClientID = "469587141175-4lhsihnumetjplrkea5uhci5g6i6o018.apps.googleusercontent.com";
            SignIn.SharedInstance.Delegate = this;
            SignIn.SharedInstance.UIDelegate = this;
            SignIn.SharedInstance.SignInUser();

            return Task.CompletedTask;
        }

        public Task LogoutAsync()
        {
            _logger.WriteDebug("Signing out iOS Google Authentication client...");
            SignIn.SharedInstance.DisconnectUser();
            return Task.CompletedTask;
        }

        public bool IsConnected => _currentUser != null;
        public string BearerToken => _currentUser?.Authentication.IdToken;
        public string AccessToken => _currentUser?.Authentication.AccessToken;
        public string Username => _currentUser?.Profile.Email;

        public IObservable<ClientConnectionStatus> ConnectionStatus => _status;

        public ClientConnectionStatus CurrentConnectionStatus => _status.Value;

        public void SignOut()
        {
            _currentUser = null;
            _status.OnNext(ClientConnectionStatus.Disconnected);
        }

        public void DidSignIn(SignIn signIn, GoogleUser user, NSError error)
        {
            _logger.WriteDebug("Google Signin: Processing sign in result...");
            _currentUser = user;

            if (error != null)
                _logger.WriteError($"Google Signin failed: {error.ToString()}");

            if (error == null && user != null)
                _status.OnNext(ClientConnectionStatus.Connected);
            else
                _status.OnNext(ClientConnectionStatus.Disconnected);
        }

        [Export("signInWillDispatch:error:")]
        public override void WillDispatch(SignIn signIn, NSError error)
        {
        }

        [Export("signIn:presentViewController:")]
        public override void PresentViewController(SignIn signIn, UIViewController viewController)
        {
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(viewController, true, null);
        }

        [Export("signIn:dismissViewController:")]
        public override void DismissViewController(SignIn signIn, UIViewController viewController)
        {
            UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null);
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
            throw new NotSupportedException("You cannot directly set credentials on the google oauth client.");
        }

		public string ClientType => ClientTypeId;

		public static string ClientTypeId = "Google";
    }
}
