using System.Net;
using System.Net.Http;
using Redbridge.SDK;

namespace Redbridge.Web.Messaging
{
	public interface IWebRequestFactory
	{
		IWebRequestSignatureService SessionManager { get; }

		TRequest CreateRequest<TRequest>() where TRequest : JsonWebRequest, new();

		IWebRequestFactory CreateFactory(IWebRequestSignatureService sessionManager);

		TRequest CreateRequest<TRequest>(AuthenticationMethod method = AuthenticationMethod.Bearer) where TRequest : JsonWebRequest, new();

        WebClient CreateWebClient(AuthenticationMethod method = AuthenticationMethod.Bearer);

        HttpClient CreateHttpClient(AuthenticationMethod method = AuthenticationMethod.Bearer);

        JsonWebRequestAction CreateRequest(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments);

		JsonWebRequestAction<TBody> CreateActionRequest<TBody>(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments);

		JsonWebRequestFunc<TResponse> CreateFuncRequest<TResponse>(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments);

		JsonWebRequestFunc<TResponse, TBody> CreateFuncRequest<TResponse, TBody>(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments);
	}
}
