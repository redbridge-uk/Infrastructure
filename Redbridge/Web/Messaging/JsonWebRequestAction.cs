using System.Net.Http;
using System.Threading.Tasks;
using Redbridge.Exceptions;

namespace Redbridge.Web.Messaging
{
	public class JsonWebRequestAction : JsonWebRequest
	{
		public JsonWebRequestAction(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb) { }

		public async Task ExecuteAsync(IHttpClientFactory clientFactory)
		{
			using (var response = await OnExecuteRequestAsync(clientFactory))
			{
                try
                {
				    if (!response.IsSuccessStatusCode)
					    await response.ThrowResponseException();
                }
                catch (UnhandledWebException uwe)
                {
                    OnHandleUnhandledWebException(uwe);
                }
			}
		}
	}

	public class JsonWebRequestAction<TInput1> : JsonWebRequest
	{
		public JsonWebRequestAction(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb) { }

		public async Task ExecuteAsync(IHttpClientFactory clientFactory, TInput1 input1)
		{
				OnExtractParameters(input1);
				using (var response = await OnExecuteRequestAsync(clientFactory, input1))
				{
                    try
                    {
                        if (!response.IsSuccessStatusCode)
                            await response.ThrowResponseException();
                    }
                    catch (UnhandledWebException uwe)
                    {
                        OnHandleUnhandledWebException(uwe);
                    }
				}
		}

		protected virtual void OnExtractParameters(TInput1 input1) { }
	}
}
