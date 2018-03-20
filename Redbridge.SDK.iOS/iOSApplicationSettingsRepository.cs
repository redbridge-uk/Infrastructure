using System;
using System.Threading;
using Foundation;
using Redbridge.Configuration;

namespace Redbridge.SDK.iOS
{
	public class iOSApplicationSettingsRepository : IApplicationSettingsRepository
	{
        private readonly Lazy<NSDictionary> _settings = new Lazy<NSDictionary>(() => NSDictionary.FromFile("AppSettings.plist"), LazyThreadSafetyMode.PublicationOnly);

        private NSDictionary Settings => _settings.Value;
       
		public bool GetBooleanValue(string key)
		{
			var booleanString = GetStringValue(key);
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
			var stringValue = Settings[key].ToString();
			return stringValue;
		}

		public Uri GetUrl(string key)
		{
			var stringValue = Settings[key].ToString();
			return new Uri(stringValue);
		}
	}
}
