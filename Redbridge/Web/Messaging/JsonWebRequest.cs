using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Redbridge.Diagnostics;
using Redbridge.Exceptions;

namespace Redbridge.Web.Messaging
{
    public abstract class JsonWebRequest
	{
		private readonly string _requestUriString;
        private readonly UrlParameterCollection _parameters = new UrlParameterCollection();
		private readonly ICollection<JsonConverter> _converters = new List<JsonConverter>();

		protected JsonWebRequest(string requestUri, HttpVerb httpVerb)
		{
            _requestUriString = requestUri ?? throw new ArgumentNullException(nameof(requestUri));
			HttpVerb = httpVerb;
            AuthenticationMethod = AuthenticationMethod.Bearer;
		}

		public UrlParameterCollection Parameters => _parameters;

        public void RegisterConverters(IEnumerable<JsonConverter> converters)
        {
            if (converters == null) throw new ArgumentNullException(nameof(converters));
            foreach (var converter in converters)
                _converters.Add(converter);
        }
		
		public IEnumerable<JsonConverter> Converters => _converters;

		public AuthenticationMethod AuthenticationMethod { get; set; }

		public Uri RootUri { get; set; }

		internal IWebRequestSignatureService SessionManager { get; set; }

		public HttpClient ToHttpClient(IHttpClientFactory clientFactory)
        {
            var client = clientFactory.CreateClient();
            client.BaseAddress = OnCreateEndpointUri();
            return client;
        }

        public virtual bool RequiresSignature => false;

        protected void AddParameter(string parameterName)
        {
            if (parameterName == null) throw new ArgumentNullException(nameof(parameterName));
            _parameters.Add(parameterName);
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
        
		protected async Task<HttpResponseMessage> OnExecuteRequestAsync (IHttpClientFactory clientFactory, object body = null)
		{
			var endpointUri = OnCreateEndpointUri();
            var request = clientFactory.CreateClient();
			
			OnApplySignature(request);

			if (HttpVerb == HttpVerb.Post || HttpVerb == HttpVerb.Put || HttpVerb == HttpVerb.Patch)
			{
				var payload = OnCreatePayload(body);
				var content = new StringContent(payload, Encoding.UTF8, ContentType);

				if (HttpVerb == HttpVerb.Put)
					return await request.PutAsync(endpointUri, content);
				
				if ( HttpVerb == HttpVerb.Post)
				    return await request.PostAsync(endpointUri, content);

                var method = new HttpMethod("PATCH");
                var message = new HttpRequestMessage(method, endpointUri) { Content = content };
                return await request.SendAsync(message);
            }

            if (body != null)
            {
                throw new NotSupportedException(
                    "Only patch, post and put are currently supported for sending requests with a body.");
            }

            if (HttpVerb == HttpVerb.Delete)
                return await request.DeleteAsync(endpointUri);

            return await request.GetAsync(endpointUri);
        }

        public virtual string ContentType { get; protected set; } = "application/json";

        protected virtual string OnCreatePayload (object body)
        {
            return JsonConvert.SerializeObject(body, _converters.ToArray());
        }

		protected virtual void OnAddConverters(JsonSerializer serializer)
		{
			foreach (var converter in _converters)
				serializer.Converters.Add(converter);
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
            return SessionManager != null ? SessionManager.PostProcessUrlString(queriedUriString) : queriedUriString;
        }

		protected virtual string AppendQuery(string baseUri)
		{
			return baseUri;
		}

		public Uri RequestUri => OnCreateEndpointUri();

		public HttpVerb HttpVerb { get; }

        public ILogger Logger { get; set; }
	}
}
