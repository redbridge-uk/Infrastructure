using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Redbridge.Configuration;

namespace Easilog.Forms.Android.Implementations
{
    /// <summary>
    /// A "app_settings.json" file needs to be placed in the Android "Assets" folder with a 
    /// build action of "AndroidAsset".
    /// </summary>
    public class AndroidApplicationSettingsRepository : IApplicationSettingsRepository
    {
        private readonly Lazy<JObject> _settings = new Lazy<JObject>(() =>
        {
            using (var asset = Application.Context.Assets.Open("app_settings.json"))
            using (var streamReader = new StreamReader(asset))
            {
                var json = streamReader.ReadToEnd();
                return JObject.Parse(json);
            }
        }, LazyThreadSafetyMode.PublicationOnly);

        private JObject Settings => _settings.Value;

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
            throw new NotImplementedException("Unable to return a section from the Android application settings.");
        }

        public string GetStringValue(string key)
        {
            var setting = this.Settings[key];
            return setting.Value<String>();
        }

        public string GetStringValueOrDefault(string key, string defaultValue)
        {
            var setting = this.Settings[key];
            if ( setting != null )
                return setting.Value<String>();

            return defaultValue;
        }

        public Uri GetUrl(string key)
        {
            return new Uri(this.GetStringValue(key));
        }

        public Uri GetUrlOrDefault(string key, Uri defaultUri)
        {
            var setting = this.Settings[key];

            if ( setting != null )
                return new Uri(setting.Value<string>());

            return defaultUri;
        }
    }
}