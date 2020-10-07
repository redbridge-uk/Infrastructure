using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Redbridge.Configuration;
using Redbridge.Web.Messaging;

namespace Redbridge.LocationServices.Google
{
    public class GoogleGeolocationService : IGeolocationService
    {
        private readonly IHttpClientFactory _clientFactory;
        public IApplicationSettingsRepository Settings { get; }

        public GoogleGeolocationService(IApplicationSettingsRepository settings, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<Address> GetGeolocationAddressAsync (Location location)
        {
            // e.g. https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&key=YOUR_API_KEY
            var key = Settings.GetStringValue("GoogleApiKey");


            var client = _clientFactory.Create();
            var baseUri = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={location.Latitude},{location.Longitude}&key={key}";
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            var response = await client.GetAsync(baseUri);

            if (!response.IsSuccessStatusCode) return new Address();

            var responseJson = await response.Content.ReadAsStringAsync();
            var components = JsonConvert.DeserializeObject<GoogleLocationResults>(responseJson);
            var postcode = components.Results?.SelectMany(r => r.AddressComponents)
                .FirstOrDefault(ac => ac.ComponentType.Any(ct => ct == "postal_code"));

            return new Address()
            {
                Postcode = postcode?.Longname
            };
        }
    }

}
