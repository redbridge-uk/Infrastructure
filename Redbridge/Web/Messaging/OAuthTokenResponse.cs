﻿using System;
using System.Runtime.Serialization;

namespace Redbridge.SDK
{
    [DataContract]
	public class OAuthTokenResponse
	{
		[DataMember(Name = "access_token")]
		public string AccessToken { get; set; }
		[DataMember(Name = "token_type")]
		public string TokenType { get; set; }
		[DataMember(Name = "expires_in")]
		public int ExpiresIn { get; set; }
	}
}
