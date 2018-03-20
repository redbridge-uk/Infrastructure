using System;
using System.IO;
using System.Threading.Tasks;
using Redbridge.Security;

namespace Redbridge.Identity.Facebook.iOS
{
    public class iOSFacebookAuthenticationClient : IAuthenticationClient
    {

        public bool IsConnected => throw new NotImplementedException();

        public string Username => throw new NotImplementedException();

        public string AccessToken => throw new NotImplementedException();


        public IObservable<ClientConnectionStatus> ConnectionStatus => throw new NotImplementedException();

        public ClientConnectionStatus CurrentConnectionStatus => throw new NotImplementedException();

        public Task BeginLoginAsync()
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
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
