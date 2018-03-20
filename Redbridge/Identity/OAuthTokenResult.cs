using System.Runtime.Serialization;

namespace Redbridge.Identity
{
	[DataContract]
	public class OAuthTokenResult
	{
		[DataMember(Name = "Access_Token")]
		public string AccessToken { get; set; }
		[DataMember(Name = "Refresh_Token")]
		public string RefreshToken { get; set; }
		[DataMember(Name = "Expires_In")]
		public int ExpiresInSeconds { get; set; }
	}
}
