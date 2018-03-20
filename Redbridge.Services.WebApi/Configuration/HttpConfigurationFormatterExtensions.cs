using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Redbridge.Services.WebApi.Configuration
{
    public static class HttpConfigurationFormatterExtensions
    {
        public static void InstallJsonFormatting (this HttpConfiguration configuration, bool disableXmlSupport = true)
        {
            configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
			configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
			configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
			configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			configuration.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			configuration.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            if (disableXmlSupport)
            {
                var appXmlType = configuration.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");

                if ( appXmlType != null )
                    configuration.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            }
        }
    }
}
