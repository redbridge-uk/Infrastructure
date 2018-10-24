using System.Threading.Tasks;

namespace Redbridge.SDK
{
	public class JsonWebRequestAction : JsonWebRequest
	{
		public JsonWebRequestAction(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb) { }

		public async Task ExecuteAsync()
		{
			using (var response = await OnExecuteRequestAsync())
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

		public async Task ExecuteAsync(TInput1 input1)
		{
				OnExtractParameters(input1);
				using (var response = await OnExecuteRequestAsync(input1))
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
