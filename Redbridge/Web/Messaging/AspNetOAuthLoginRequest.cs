using System;
using System.Runtime.Serialization;

namespace Redbridge.SDK
{
	[DataContract]
	public class AspNetOAuthLoginRequest
	{
		[DataMember]
		public string Username { get; set; }
		[DataMember]
		public string Password { get; set; }
		[DataMember(Name="client_id")]
		public string ClientId { get; set; }
		[DataMember(Name = "client_secret")]
		public string ClientSecret { get; set; }
	}
}
