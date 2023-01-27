using System;
using Microsoft.Extensions.Configuration;
using Redbridge.Exceptions;

namespace Redbridge.Configuration
{
    public class ApplicationSettingsRepository : IApplicationSettingsRepository
    {
        private readonly IConfiguration _configuration;

        public ApplicationSettingsRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private void ValidateKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
        }

        public Guid GetGuidValue(string key)
        {
            return Guid.Parse(GetStringValue(key));
        }

        public int GetInt32Value(string key)
        {
            ValidateKey(key);

            var stringValue = GetStringValue(key);

            if (!int.TryParse(stringValue, out var value))
            {
                throw new InvalidConfigurationRepositoryValueException($"Unable to return the application key '{key}' from the configuration or the returned value is not convertible to an Int32 type.");
            }

            return value;
        }

        public bool GetBooleanValue(string key)
        {
            ValidateKey(key);

            var stringValue = GetStringValue(key);

            if (!bool.TryParse(stringValue, out var value))
            {
                throw new InvalidConfigurationRepositoryValueException(
                    $"Unable to return the application key '{key}' from the configuration or the returned value is not convertible to an Boolean type.");
            }

            return value;
        }

        public string GetStringValue(string key)
        {
            ValidateKey(key);

            var stringValue = _configuration[key];
            return stringValue;
        }

        public string GetStringValueOrDefault(string key, string defaultValue)
        {
            ValidateKey(key);

            var stringValue = GetStringValue(key);
            if ( stringValue != null )
                return stringValue;

            return defaultValue;
        }

        public Uri GetUrl(string key)
        {
            ValidateKey(key);

            var stringValue = GetStringValue(key);
            return new Uri(stringValue);
        }

        public Uri GetUrlOrDefault(string key, Uri defaultUri)
        {
            ValidateKey(key);

            var stringValue = GetStringValue(key);
            if ( stringValue != null )
                return new Uri(stringValue);

            return defaultUri;
        }

        public T GetSection<T>(string name) where T : class
        {
            var section = _configuration.GetSection(name);
            if (section == null) throw new InvalidConfigurationRepositoryValueException($"Unable to locate section {name} in the configuration file.");
            return (T)section;
        }
    }
}