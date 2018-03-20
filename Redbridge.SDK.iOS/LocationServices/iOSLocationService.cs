using System;
using System.Reactive.Subjects;
using CoreLocation;
using Redbridge.LocationServices;
using UIKit;

namespace Redbridge.SDK.iOS.LocationServices
{
    public class iOSLocationService: ILocationService
    {
        protected CLLocationManager _locMgr;
        private BehaviorSubject<Location> _locationSubject = new BehaviorSubject<Location>(null);

        public iOSLocationService()
        {
            _locMgr = new CLLocationManager();
            _locMgr.PausesLocationUpdatesAutomatically = false;

            // iOS 8 has additional permissions requirements
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                _locMgr.RequestAlwaysAuthorization(); // works in background
                                                      //locMgr.RequestWhenInUseAuthorization (); // only in foreground
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                _locMgr.AllowsBackgroundLocationUpdates = true;
            }
        }

        public CLLocationManager LocMgr
        {
            get { return _locMgr; }
        }

        public IObservable<Location> Location => _locationSubject;

        public void Stop()
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                LocMgr.StopUpdatingLocation();
            }
        }

        public void Start()
        {

            if (CLLocationManager.LocationServicesEnabled)
            { //set the desired accuracy, in meters 
                LocMgr.DesiredAccuracy = 1;
                LocMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    // fire our custom Location Updated event 
                    var location = (e.Locations[e.Locations.Length - 1]);
                    _locationSubject.OnNext(new Location()
                    {
                        Longitude = location.Coordinate.Longitude,
                        Latitude = location.Coordinate.Latitude,
                    });
                };
                LocMgr.StartUpdatingLocation();
            }
        }
    }
}
