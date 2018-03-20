//using System;
//using System.IO;
//using System.Reactive.Subjects;
//using System.Threading.Tasks;
//using Auth0.OidcClient;
//using Redbridge.Identity;
//using Redbridge.Security;

//namespace Redbridge.Xamarin.Forms.iOS
//{
//	public class Auth0ClientIos : IAuthenticationClient
//	{
//		private readonly Auth0Client _platformAuth0;
//		private Auth0User _userCredentials;
//		private BehaviorSubject<ClientConnectionStatus> _connectionStatus = new BehaviorSubject<ClientConnectionStatus>(ClientConnectionStatus.Disconnected);

//		public Auth0ClientIos(string domain, string clientId)
//		{
//			_platformAuth0 = new Auth0Client()domain, clientId);
//		}

//		public ClientConnectionStatus CurrentConnectionStatus => _connectionStatus.Value;

//		public async Task BeginLoginAsync ()
//		{
//			_connectionStatus.OnNext(ClientConnectionStatus.Connecting);
//			var uiViewController = RootViewControllerFinder.GetCurrent();
//			_userCredentials = await _platformAuth0.LoginAsync(uiViewController);

//			if (string.IsNullOrWhiteSpace(_userCredentials.Auth0AccessToken))
//				_connectionStatus.OnNext(ClientConnectionStatus.Disconnected);
//			else
//				_connectionStatus.OnNext(ClientConnectionStatus.Connected);

//		}

//        public string ClientType => "Auth0";

//		public IObservable<ClientConnectionStatus> ConnectionStatus
//		{
//			get { return _connectionStatus; }
//		}

//		public bool IsConnected
//		{
//			get { return _userCredentials != null; }
//		}

//		public string AccessToken
//		{
//			get { return _userCredentials?.Auth0AccessToken; }
//		}

//		public string BearerToken
//		{
//			get { return _userCredentials?.IdToken; }
//		}

//        public string Username
//        {
//            get { throw new NotImplementedException("You need to crack open the JSON profile and get this information."); }
//        }

//		public Task LogoutAsync ()
//		{
//			_platformAuth0.Logout();
//			_userCredentials = null;

//			if ( _connectionStatus.Value != ClientConnectionStatus.Disconnected )
//				_connectionStatus.OnNext(ClientConnectionStatus.Disconnected);

//            return Task.CompletedTask;
//		}

//        public Task<Stream> SaveAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<UserCredentials> LoadAsync(Stream stream)
//        {
//            throw new NotImplementedException();
//        }

//        public void SetCredentials(UserCredentials credentials)
//        {
//            throw new NotSupportedException("You cannot explicitly set the credentials here at the moment.");
//        }
//    }
//}
