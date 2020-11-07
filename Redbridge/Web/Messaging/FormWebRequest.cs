using System;
using System.Collections.Generic;

namespace Redbridge.Web.Messaging
{
	public class FormWebRequest<TResponse> : JsonWebRequestFunc<TResponse, IDictionary<string, string>>
	{
		public FormWebRequest(Uri actionUri, HttpVerb verb) : base(actionUri.AbsoluteUri, verb)
		{
			ContentType = "application/x-www-form-urlencoded";
		}

        protected override string OnCreatePayload(IDictionary<string, string> input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            var values = HttpUtility.ParseQueryString(string.Empty);
			input.ForEach(valuePair => values.Add(valuePair.Key, valuePair.Value));
			return values.ToString();
        }
	}
}
