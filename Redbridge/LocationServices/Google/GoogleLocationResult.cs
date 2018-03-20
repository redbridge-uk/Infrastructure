using System.Runtime.Serialization;

namespace Redbridge.LocationServices.Google
{
    [DataContract]     public class GoogleLocationResult     {         [DataMember(Name = "address_components")]         public GoogleAddressComponent[] AddressComponents { get; set; }     } 
}
