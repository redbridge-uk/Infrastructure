using System;
using System.Threading.Tasks;

namespace Redbridge.SDK
{
	public abstract class RestWebClient
	{
		protected RestWebClient(IWebRequestFactory webRequestFactory)
		{
			if (webRequestFactory == null) throw new ArgumentNullException(nameof(webRequestFactory));
			RequestFactory = webRequestFactory;
		}

		public bool IsConnected
		{
			get { return RequestFactory.SessionManager.Authority.IsConnected; }
		}

		public IWebRequestFactory RequestFactory { get; }

		protected async Task Execute(string url, HttpVerb verb = HttpVerb.Get, params object[] args)
		{
			var request = RequestFactory.CreateRequest(url, verb, args);
			await request.ExecuteAsync();
		}

		protected async Task ExecuteAction<TBody>(TBody body, string url, HttpVerb verb = HttpVerb.Post, params object[] args)
		{
			var request = RequestFactory.CreateActionRequest<TBody>(url, verb, args);
			await request.ExecuteAsync(body);
		}

		protected async Task<TResult> ExecuteFunc<TResult>(string url, HttpVerb verb = HttpVerb.Get, params object[] args)
		{
			var request = RequestFactory.CreateFuncRequest<TResult>(url, verb, args);
			return await request.ExecuteAsync();
		}

		protected async Task<TResult> ExecuteFunc<TResult, TBody>(TBody body, string url, HttpVerb verb = HttpVerb.Get, params object[] args)
		{
			var request = RequestFactory.CreateFuncRequest<TResult, TBody>(url, verb, args);
			return await request.ExecuteAsync(body);
		}
	}
}
