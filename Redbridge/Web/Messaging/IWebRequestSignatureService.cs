using System.Net.Http;
using Redbridge.Identity;

namespace Redbridge.SDK
{
	public interface IWebRequestSignatureService
	{
        IAuthenticationClient Authority { get; }
		void SignRequest(HttpClient client, AuthenticationMethod method = AuthenticationMethod.Bearer);
		string PostProcessUrlString(string queriedUriString);
	}
}
