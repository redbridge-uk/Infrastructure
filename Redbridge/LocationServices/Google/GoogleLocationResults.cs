using System.Runtime.Serialization;

namespace Redbridge.LocationServices.Google
{
    [DataContract]
    public class GoogleLocationResults
    {
        [DataMember(Name = "results")]
        public GoogleLocationResult[] Results { get; set; }
    }

}
