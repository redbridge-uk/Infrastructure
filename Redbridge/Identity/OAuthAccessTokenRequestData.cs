using System;
using System.Collections.Generic;

namespace Redbridge.Identity
{
    public class OAuthAccessTokenRequestData
	{
		public string GrantType { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public IDictionary<string, string> AsDictionary()
		{
			var formData = new Dictionary<string, string>
			{
				{"grant_type", GrantType},
				{"client_id", ClientId},
				{"client_secret", ClientSecret},
				{"username", Email},
				{"password", Password}
			};
			return formData;
		}
	}
}
