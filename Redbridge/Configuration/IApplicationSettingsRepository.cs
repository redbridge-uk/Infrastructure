using System;

namespace Redbridge.Configuration
{
    public interface IApplicationSettingsRepository
	{
        Guid GetGuidValue(string key);

		int GetInt32Value(string key);

		bool GetBooleanValue(string key);

		string GetStringValue(string key);

        string GetStringValueOrDefault(string key, string defaultValue);

		T GetSection<T>(string name) where T : class;

		Uri GetUrl(string key);

        Uri GetUrlOrDefault(string key, Uri defaultUri);
	}
}
