using Redbridge.Web.Messaging;

namespace Redbridge.SDK
{
	public interface IWebRequestFactory
	{
		IWebRequestSignatureService SessionManager { get; }

		TRequest CreateRequest<TRequest>() where TRequest : JsonWebRequest, new();

		IWebRequestFactory CreateFactory(IWebRequestSignatureService sessionManager);

		TRequest CreateRequest<TRequest>(AuthenticationMethod method = AuthenticationMethod.Bearer) where TRequest : JsonWebRequest, new();

		JsonWebRequestAction CreateRequest(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments);

		JsonWebRequestAction<TBody> CreateActionRequest<TBody>(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments);

		JsonWebRequestFunc<TResponse> CreateFuncRequest<TResponse>(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments);

		JsonWebRequestFunc<TResponse, TBody> CreateFuncRequest<TResponse, TBody>(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments);
	}
}
