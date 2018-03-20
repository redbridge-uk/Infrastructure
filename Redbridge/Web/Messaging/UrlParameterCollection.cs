using System;
using System.Collections.Generic;

namespace Redbridge.SDK
{
	public class UrlParameterCollection
	{
		private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

		public object this[string key]
		{
			get { return _parameters[key.ToLowerInvariant()]; }
			set { _parameters[key.ToLowerInvariant()] = value; }
		}

		public void Add(string key)
		{
			Add(key, null);
		}

		public void Add(string key, object value)
		{
			_parameters.Add(key, value);
		}

		public string ParseUriString(string uri)
		{
			var resultantUri = uri;
			foreach (var parameter in _parameters)
			{
				var parameterPattern = $"{{{parameter.Key}}}".ToLowerInvariant();
				if (parameter.Value is DateTime)
				{
					resultantUri = resultantUri.Replace(parameterPattern, ((DateTime)parameter.Value).ToString("yyyy/MM/dd HH:mm:ss"));
				}
				else
					resultantUri = resultantUri.Replace(parameterPattern, parameter.Value?.ToString() ?? string.Empty);
			}
			return resultantUri;
		}
	}
}
