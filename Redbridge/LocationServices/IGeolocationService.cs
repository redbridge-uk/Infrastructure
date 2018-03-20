using System;
using System.Threading.Tasks;

namespace Redbridge.LocationServices
{
    public interface IGeolocationService
    {
        Task<Address> GetGeolocationAddressAsync (Location location);
    }
}
