using System.Threading.Tasks;
using System;
using System.IO;
using Redbridge.Security;

namespace Redbridge.Identity
{
	public interface IAuthenticationClient
	{
		bool IsConnected { get; }
        string Username { get; }
		string AccessToken { get; }
		Task BeginLoginAsync ();
		Task LogoutAsync();
        string ClientType { get; }
		IObservable<ClientConnectionStatus> ConnectionStatus { get; }
		ClientConnectionStatus CurrentConnectionStatus { get; }
        Task<Stream> SaveAsync ();
        Task<UserCredentials> LoadAsync(Stream stream);
        void SetCredentials(UserCredentials credentials);
	}
}
