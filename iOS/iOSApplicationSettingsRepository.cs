using System;
using Easilog.SDK;
using Foundation;

namespace Easilog.iOS
{
	public class iOSApplicationSettingsRepository : IApplicationSettingsRepository
	{
		private readonly NSDictionary _settings = NSDictionary.FromFile("AppSettings.plist");

		public bool GetBooleanValue(string key)
		{
			throw new NotImplementedException();
		}

		public int GetInt32Value(string key)
		{
			throw new NotImplementedException();
		}

		public T GetSection<T>(string name) where T : class
		{
			throw new NotImplementedException();
		}

		public string GetStringValue(string key)
		{
			var stringValue = _settings[key].ToString();
			return stringValue;
		}

		public Uri GetUrl(string key)
		{
			var stringValue = _settings[key].ToString();
			return new Uri(stringValue);
		}
	}
}
