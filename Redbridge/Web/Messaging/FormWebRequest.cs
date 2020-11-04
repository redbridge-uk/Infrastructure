using System;
using System.Collections.Generic;

namespace Redbridge.Web.Messaging
{
	public class FormServiceRequest<TResponse> : JsonWebRequestFunc<TResponse, IDictionary<string, string>>
	{
		public FormServiceRequest(Uri actionUri, HttpVerb verb) : base(actionUri.AbsoluteUri, verb)
		{
			ContentType = "application/x-www-form-urlencoded";
		}

        protected override string OnCreatePayload(IDictionary<string, string> input)
        {
			var values = HttpUtility.ParseQueryString(string.Empty);
			input.ForEach(valuePair => values.Add(valuePair.Key, valuePair.Value));
			return values.ToString();
        }
	}
}
