using System;
using Redbridge.Configuration;

namespace Redbridge.SDK.Droid
{
	public class DroidApplicationSettingsRepository : IApplicationSettingsRepository
	{
		public Guid GetGuidValue(string key)
		{
			return Guid.Parse(GetStringValue(key));
		}

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
			throw new NotImplementedException();
		}

		public Uri GetUrl(string key)
		{
			throw new NotImplementedException();
		}
	}
}
