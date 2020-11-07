using System;
using System.Threading.Tasks;

namespace Redbridge.Web.Messaging
{
	public abstract class RestWebClient
	{
        protected RestWebClient(IWebRequestFactory webRequestFactory, IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            RequestFactory = webRequestFactory ?? throw new ArgumentNullException(nameof(webRequestFactory));
        }

        protected IHttpClientFactory ClientFactory { get; }

        public bool IsConnected => RequestFactory.SessionManager?.Authority?.IsConnected ?? false;

        public IWebRequestFactory RequestFactory { get; }

		protected async Task Execute(string url, HttpVerb verb = HttpVerb.Get, params object[] args)
		{
			var request = RequestFactory.CreateRequest(url, verb, args);
			await request.ExecuteAsync(ClientFactory);
		}

		protected async Task ExecuteAction<TBody>(TBody body, string url, HttpVerb verb = HttpVerb.Post, params object[] args)
		{
			var request = RequestFactory.CreateActionRequest<TBody>(url, verb, args);
			await request.ExecuteAsync(ClientFactory, body);
		}

		protected async Task<TResult> ExecuteFunc<TResult>(string url, HttpVerb verb = HttpVerb.Get, params object[] args)
		{
			var request = RequestFactory.CreateFuncRequest<TResult>(url, verb, args);
			return await request.ExecuteAsync(ClientFactory);
		}

		protected async Task<TResult> ExecuteFunc<TResult, TBody>(TBody body, string url, HttpVerb verb = HttpVerb.Get, params object[] args)
		{
			var request = RequestFactory.CreateFuncRequest<TResult, TBody>(url, verb, args);
			return await request.ExecuteAsync(ClientFactory, body);
		}
	}
}
