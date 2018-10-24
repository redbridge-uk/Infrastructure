using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Redbridge.Diagnostics;

namespace Redbridge.SDK
{
	public abstract class JsonWebRequest
	{
		private readonly string _requestUriString;
		private readonly HttpVerb _httpVerb;
		private readonly UrlParameterCollection _parameters = new UrlParameterCollection();
		private readonly ICollection<JsonConverter> _converters = new List<JsonConverter>();

		protected JsonWebRequest(string requestUri, HttpVerb httpVerb)
		{
            _requestUriString = requestUri ?? throw new ArgumentNullException(nameof(requestUri));
			_httpVerb = httpVerb;
			AuthenticationMethod = AuthenticationMethod.Bearer;
		}

		public UrlParameterCollection Parameters => _parameters;

		protected void AddParameter(string parameterName)
		{
			if (parameterName == null) throw new ArgumentNullException(nameof(parameterName));
			_parameters.Add(parameterName);
		}

		public IEnumerable<JsonConverter> Converters => _converters;

		public AuthenticationMethod AuthenticationMethod { get; set; }

		public Uri RootUri { get; set; }

		internal IWebRequestSignatureService SessionManager { get; set; }

		public HttpClient ToHttpClient()
		{
			return new HttpClient()
			{
				BaseAddress = OnCreateEndpointUri(),
			};
		}

        protected virtual void OnHandleUnhandledWebException (UnhandledWebException uwe)
        {
            throw uwe;
        }

		protected virtual void OnApplySignature(HttpClient request)
		{
			if (RequiresSignature && (SessionManager == null || !SessionManager.Authority.IsConnected))
			{
				throw new RedbridgeException("This request requires a signature but no session manager is available or the session is not connected.");
			}

			if (SessionManager != null)
			{
				if (SessionManager.Authority.IsConnected)
					SessionManager.SignRequest(request);
			}
		}

		public virtual bool RequiresSignature => false;

		protected async Task<HttpResponseMessage> OnExecuteRequestAsync()
		{
			var endpointUri = OnCreateEndpointUri();

			using (var request = new HttpClient())
			{
				OnApplySignature(request);

				if (HttpVerb == HttpVerb.Get)
					return await request.GetAsync(endpointUri);

				if (HttpVerb == HttpVerb.Delete)
					return await request.DeleteAsync(endpointUri);

				if (HttpVerb == HttpVerb.Post)
					return await request.PostAsync(endpointUri, null); 
			}

			throw new NotSupportedException("Only gets are currently supported for making requests with no body.");
		}

        public virtual string ContentType { get; protected set; } = "application/json";

		protected async Task<HttpResponseMessage> OnExecuteRequestAsync (object body)
		{
			var endpointUri = OnCreateEndpointUri();

			using (var request = new HttpClient())
			{
				OnApplySignature(request);

				if (HttpVerb == HttpVerb.Post || HttpVerb == HttpVerb.Put)
				{
					var payload = OnCreatePayload(body);
					var content = new StringContent(payload, Encoding.UTF8, ContentType);

					if (HttpVerb == HttpVerb.Put)
						return await request.PutAsync(endpointUri, content);
					
					return await request.PostAsync(endpointUri, content);
				}

				throw new NotSupportedException("Only post and put are currently supported for sending requests with a body.");
			}
		}

        protected virtual string OnCreatePayload (object body)
        {
            return JsonConvert.SerializeObject(body, _converters.ToArray());
        }

		protected virtual void OnAddConverters(JsonSerializer serializer)
		{
			foreach (var converter in _converters)
				serializer.Converters.Add(converter);
		}

		public void RegisterConverters(IEnumerable<JsonConverter> converters)
		{
			if (converters == null) throw new ArgumentNullException(nameof(converters));
			foreach (var converter in converters)
				_converters.Add(converter);
		}

		protected virtual Uri OnCreateEndpointUri()
		{
			var requestWithParameters = OnCreateRelativeUri(_requestUriString);
			var requestUri = RootUri != null ? new Uri(RootUri, requestWithParameters) : requestWithParameters;
			return requestUri;
		}

		protected virtual Uri OnCreateRelativeUri(string uri)
		{
			var uriString = _parameters.ParseUriString(uri);
			var queriedUriString = AppendQuery(uriString);
			queriedUriString = PostProcessQueryString(queriedUriString);
			return new Uri(queriedUriString, UriKind.RelativeOrAbsolute);
		}

		protected string PostProcessQueryString(string queriedUriString)
		{
			if (SessionManager != null)
				return SessionManager.PostProcessUrlString(queriedUriString);

			return queriedUriString;
		}

		protected virtual string AppendQuery(string baseUri)
		{
			return baseUri;
		}

		public Uri RequestUri => OnCreateEndpointUri();

		public HttpVerb HttpVerb => _httpVerb;

		public ILogger Logger { get; set; }
	}
}
