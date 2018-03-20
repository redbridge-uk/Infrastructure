using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Redbridge.IO;

namespace Redbridge.SDK
{
	public static class WebExceptionProcessor
	{
		public static async Task ThrowResponseException(this HttpResponseMessage httpWebResponse)
		{
			if (httpWebResponse.StatusCode == (HttpStatusCode)422)
			{
				// Address how this should be done, probably should be treated as a validation error item in itself.
				// Although, sometimes unknown entities are translated into validation errors too. So we need to ensure that the filter
				// in the web space turns this into a validation collection (with a single item if necessary).
				var json = await httpWebResponse.Content.ReadAsStreamAsync();
				dynamic body = json.DeserializeJson();

				// For your future self, this makes no sense at all.
				// Depending on implementation, you may get back an array here.
				var message = "No validation message supplied.";

				if (body is JArray)
				{
					var bodyItem = body[0]["message"];
					message = bodyItem.Value;
				}
				else
					message = body["message"].Value;

				if (message != null)
					throw new ValidationException(message);
				else
					throw new ValidationException();
			}

			if (httpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new UserNotAuthenticatedException(httpWebResponse.ReasonPhrase);
			}

			if (httpWebResponse.StatusCode == HttpStatusCode.Forbidden)
			{
				throw new UserNotAuthorizedException(httpWebResponse.ReasonPhrase);
			}
		}
	}
}
