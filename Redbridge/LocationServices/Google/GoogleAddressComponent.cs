using System.Runtime.Serialization;

namespace Redbridge.LocationServices.Google
{
    [DataContract]
    public class GoogleAddressComponent
    {
        [DataMember(Name = "short_name")]
        public string Shortname { get; set; }
        [DataMember(Name = "long_name")]
        public string Longname { get; set; }
        [DataMember(Name = "types")]
        public string[] ComponentType { get; set; }
    }

}
