using System;
namespace Redbridge.LocationServices
{
    public interface ILocationService     {         void Start();         void Stop();         IObservable<Location> Location { get; }     }
}
