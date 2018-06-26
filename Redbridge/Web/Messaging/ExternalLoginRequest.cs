using System.Runtime.Serialization;

namespace Redbridge.SDK
{
    [DataContract]
    public class ExternalLoginRequest
    {
        [DataMember]
        public string Provider { get; set; }
        [DataMember]
        public string ClientId { get; set; }
        [DataMember]
        public string Error { get; set; }
        [DataMember]
        public string RedirectUrl { get; set; }
    }
}
