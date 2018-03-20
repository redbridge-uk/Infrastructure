using System.Runtime.Serialization;

namespace Redbridge.Identity.OAuthServer
{
    [DataContract]
    public class AuthSettings
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string PasswordHash { get; set; }
        [DataMember]
        public string RefreshToken { get; set; }
        [DataMember]
        public string AuthenticationType { get; set; }
    }
}
