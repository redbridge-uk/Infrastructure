using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Redbridge.Diagnostics;

namespace Redbridge.SDK
{
	public abstract class WebRequestFactory : IWebRequestFactory
	{
		private readonly Uri _baseUri;
		private readonly IWebRequestSignatureService _sessionManager;
		private readonly ILogger _logger;
		private readonly ICollection<JsonConverter> _converters = new List<JsonConverter>();

		public WebRequestFactory(Uri baseUri, IWebRequestSignatureService sessionManager) : this(baseUri, sessionManager, new BlackholeLogger()){}

		public WebRequestFactory(Uri baseUri, IWebRequestSignatureService sessionManager, ILogger logger)
		{
			if (baseUri == null) throw new ArgumentNullException(nameof(baseUri));
			if (sessionManager == null) throw new ArgumentNullException(nameof(sessionManager));
			_baseUri = baseUri;
			_sessionManager = sessionManager;
			_logger = logger;

			RegisterConverters();
		}

		protected void RegisterConverters() 
		{
			OnRegisterConverters();
		}

		protected virtual void OnRegisterConverters() 
		{
			_converters.Add(new StringEnumConverter());
		}

		protected ICollection<JsonConverter> Converters => _converters;

		public IWebRequestSignatureService SessionManager { get { return _sessionManager; } }

		public TRequest CreateRequest<TRequest>()
			where TRequest : JsonWebRequest, new()
		{
			var request = new TRequest
			{
				SessionManager = _sessionManager,
				RootUri = _baseUri,
				Logger = _logger,
			};
			request.RegisterConverters(_converters);
			return request;
		}

		public TRequest CreateRequest<TRequest>(AuthenticationMethod method = AuthenticationMethod.Bearer) where TRequest : JsonWebRequest, new()
		{
			var request = new TRequest
			{
				RootUri = _baseUri,
				SessionManager = _sessionManager,
				AuthenticationMethod = method,
			};
			request.RegisterConverters(_converters);
			return request;
		}

		public JsonWebRequestAction CreateRequest(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments)
		{
			var request = new JsonWebRequestAction(string.Format(url, arguments), verb)
			{
				RootUri = _baseUri,
				SessionManager = _sessionManager,
				AuthenticationMethod = AuthenticationMethod.Bearer,
			};
			request.RegisterConverters(_converters);
			return request;
		}

		public JsonWebRequestAction<TBody> CreateActionRequest<TBody>(string url, HttpVerb verb = HttpVerb.Post, params object[] arguments)
		{
			var request = new JsonWebRequestAction<TBody>(string.Format(url, arguments), verb)
			{
				RootUri = _baseUri,
				SessionManager = _sessionManager,
				AuthenticationMethod = AuthenticationMethod.Bearer,
			};
			request.RegisterConverters(_converters);
			return request;
		}

		public JsonWebRequestFunc<TResponse> CreateFuncRequest<TResponse>(string url, HttpVerb verb = HttpVerb.Get, params object[] arguments)
		{
			var request = new JsonWebRequestFunc<TResponse>(string.Format(url, arguments), verb)
			{
				RootUri = _baseUri,
				SessionManager = _sessionManager,
				AuthenticationMethod = AuthenticationMethod.Bearer,
			};
			request.RegisterConverters(_converters);
			return request;
		}

		public JsonWebRequestFunc<TBody, TResponse> CreateFuncRequest<TBody, TResponse>(string url, HttpVerb verb = HttpVerb.Post, params object[] arguments)
		{
			var request = new JsonWebRequestFunc<TBody, TResponse>(string.Format(url, arguments), verb)
			{
				RootUri = _baseUri,
				SessionManager = _sessionManager,
				AuthenticationMethod = AuthenticationMethod.Bearer,
			};
			request.RegisterConverters(_converters);
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
