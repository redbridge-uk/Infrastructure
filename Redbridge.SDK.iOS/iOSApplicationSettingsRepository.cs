using System;
using System.Threading;
using Foundation;
using Redbridge.Configuration;
using Redbridge.Diagnostics;

namespace Redbridge.SDK.iOS
{
	public class iOSApplicationSettingsRepository : IApplicationSettingsRepository
	{
        private readonly Lazy<NSDictionary> _settings = new Lazy<NSDictionary>(() => NSDictionary.FromFile("AppSettings.plist"), LazyThreadSafetyMode.PublicationOnly);
        private readonly ILogger _logger;

        private NSDictionary Settings => _settings.Value;
       
        public iOSApplicationSettingsRepository (ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

		public bool GetBooleanValue(string key)
		{

			var booleanString = GetStringValue(key);
            _logger.WriteDebug($"Configuration value for key {key} returned as {booleanString} (boolean)...");
			return bool.Parse(booleanString);
		}

        public Guid GetGuidValue(string key)
        {
            return Guid.Parse(GetStringValue(key));
        }

        public int GetInt32Value(string key)
		{
			var intString = GetStringValue(key);
			return int.Parse(intString);
		}

		public T GetSection<T>(string name) where T : class
		{
			throw new NotImplementedException("Unable to return a section from the iOS application settings.");
		}

		public string GetStringValue(string key)
		{
            _logger.WriteDebug($"Attempting to recall configuration value for key {key}...");
			var stringValue = Settings[key].ToString();
            _logger.WriteDebug($"Configuration value for key {key} returned as '{stringValue}'...");
			return stringValue;
		}

        public string GetStringValueOrDefault(string key, string defaultValue)
        {
            _logger.WriteDebug($"Attempting to recall configuration value for key {key}...");
            var stringValue = Settings[key];
            if (stringValue != null)
            {
                _logger.WriteDebug($"Configuration value for key {key} returned as '{stringValue}'...");
                return stringValue.ToString();
            }

            _logger.WriteDebug($"Configuration value for key {key} not found returning default '{defaultValue}'...");
            return defaultValue;
        }

		public Uri GetUrl(string key)
		{
			var stringValue = Settings[key].ToString();
			return new Uri(stringValue);
		}

        public Uri GetUrlOrDefault(string key, Uri defaultUri)
        {
            var stringValue = Settings[key];

            if ( stringValue != null )
                return new Uri(stringValue.ToString());

            return defaultUri;
        }
	}
}
