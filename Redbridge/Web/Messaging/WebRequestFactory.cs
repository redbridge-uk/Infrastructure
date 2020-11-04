using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Redbridge.Diagnostics;

namespace Redbridge.Web.Messaging
{
	public abstract class WebRequestFactory : IWebRequestFactory
	{
		private readonly Uri _baseUri;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;

        protected WebRequestFactory(Uri baseUri, IWebRequestSignatureService sessionManager, IHttpClientFactory clientFactory) 
            : this(baseUri, sessionManager, new BlackholeLogger(), clientFactory){}

        protected WebRequestFactory(Uri baseUri, IWebRequestSignatureService sessionManager, ILogger logger, IHttpClientFactory clientFactory)
		{
            _baseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
			SessionManager = sessionManager ?? throw new ArgumentNullException(nameof(sessionManager));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

            RegisterConverters();
		}

        protected IHttpClientFactory ClientFactory => _clientFactory;

		protected void RegisterConverters() 
		{
			OnRegisterConverters();
		}

		protected virtual void OnRegisterConverters() 
		{
			Converters.Add(new StringEnumConverter());
		}

		protected ICollection<JsonConverter> Converters { get; } = new List<JsonConverter>();

        public IWebRequestSignatureService SessionManager { get; }

        public WebClient CreateWebClient(AuthenticationMethod method = AuthenticationMethod.Bearer)
        {
            var client = new WebClient {BaseAddress = _baseUri.AbsoluteUri};
            SessionManager.SignRequest(client, method);
            return client;
        }

        public HttpClient CreateHttpClient(AuthenticationMethod method = AuthenticationMethod.Bearer)
        {
            var client = new HttpClient() { BaseAddress = _baseUri };
            SessionManager.SignRequest(client, method);
            return client;
        }

        public TRequest CreateRequest<TRequest>()
			where TRequest : JsonWebRequest, new()
		{
			var request = new TRequest
			{
				SessionManager = SessionManager,
				RootUri = _baseUri,
				Logger = _logger,
			};
			request.RegisterConverters(Converters);
			return request;
		}

		public TRequest CreateRequest<TRequest>(AuthenticationMethod method = AuthenticationMethod.Bearer) where TRequest : JsonWebRequest, new()
		{
			var request = new TRequest
			{
				RootUri = _baseUri,
				SessionManager = SessionManager,
				AuthenticationMethod = method,
			};
			request.RegisterConverters(Converters);
			return request;
		}

		public JsonWebRequestAction CreateRequest(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments)
		{
			var request = new JsonWebRequestAction(string.Format(url, arguments), verb)
			{
				RootUri = _baseUri,
				SessionManager = SessionManager,
				AuthenticationMethod = AuthenticationMethod.Bearer,
			};
			request.RegisterConverters(Converters);
			return request;
		}

		public JsonWebRequestAction<TBody> CreateActionRequest<TBody>(string url, HttpVerb verb = HttpVerb.Post, params object[] arguments)
		{
			var request = new JsonWebRequestAction<TBody>(string.Format(url, arguments), verb)
			{
				RootUri = _baseUri,
				SessionManager = SessionManager,
				AuthenticationMethod = AuthenticationMethod.Bearer,
			};
			request.RegisterConverters(Converters);
			return request;
		}

		public JsonWebRequestFunc<TResponse> CreateFuncRequest<TResponse>(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments)
		{
			var request = new JsonWebRequestFunc<TResponse>(string.Format(url, arguments), verb)
			{
				RootUri = _baseUri,
				SessionManager = SessionManager,
				AuthenticationMethod = AuthenticationMethod.Bearer,
			};
			request.RegisterConverters(Converters);
			return request;
		}

		public JsonWebRequestFunc<TBody, TResponse> CreateFuncRequest<TBody, TResponse>(string url, HttpVerb verb = HttpVerb.Post, params object[] arguments)
		{
			var request = new JsonWebRequestFunc<TBody, TResponse>(string.Format(url, arguments), verb)
			{
				RootUri = _baseUri,
				SessionManager = SessionManager,
				AuthenticationMethod = AuthenticationMethod.Bearer,
			};
			request.RegisterConverters(Converters);
			return request;
		}

		public IWebRequestFactory CreateFactory(IWebRequestSignatureService sessionManager)
		{
			if (sessionManager == null) throw new ArgumentNullException(nameof(sessionManager));
			return OnCreateFactory(_baseUri, sessionManager, _logger);
		}

		protected abstract IWebRequestFactory OnCreateFactory(Uri baseUri, IWebRequestSignatureService sessionManager, ILogger logger);
	}
}
