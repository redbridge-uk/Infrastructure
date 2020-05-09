using NUnit.Framework;
using System;
using Redbridge.SDK;
using Moq;
using System.Runtime.Serialization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Redbridge.Identity;
using Redbridge.Configuration;
using Redbridge.Diagnostics;
using Redbridge.Web.Messaging;

namespace Tests
{
	public class TestIntegrationJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException("TestIntegrationJsonConverter has not implemented method WriteJson");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader == null) throw new ArgumentNullException(nameof(reader));
			if (objectType == null) throw new ArgumentNullException(nameof(objectType));
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			var jObject = JObject.Load(reader);
			JToken jToken;

			if (jObject.TryGetValue("type", StringComparison.CurrentCultureIgnoreCase, out jToken))
			{
				object targetObject = null;
				serializer.Populate(jObject.CreateReader(), targetObject);
				return targetObject;
			}

			throw new NotImplementedException("Unable to deserialize the integration data type as the 'type' attribute cannot be located.");
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IntegrationData);
		}
	}

	[DataContract]
	public class IntegrationData
	{
		
	}

	public class SpecializedWebRequestFactory : WebRequestFactory
	{
		public SpecializedWebRequestFactory(IWebRequestSignatureService sessionManager) : base(new Uri("http://smuggoat-api.azurewebsites.net"), sessionManager, new BlackholeLogger()) { }

		protected override IWebRequestFactory OnCreateFactory(Uri baseUri, IWebRequestSignatureService sessionManager, ILogger logger)
		{
			return new SpecializedWebRequestFactory(sessionManager);
		}

		protected override void OnRegisterConverters()
		{
			base.OnRegisterConverters();
			Converters.Add(new TestIntegrationJsonConverter());
		}
	}

	[TestFixture()]
	public class WebRequestFactoryTests
	{
		[Test()]
		public void CreateSpecializedFactoryFuncRequestExpectCustomJsonConverter()
		{
			var sessionManager = new Mock<IWebRequestSignatureService>();
			var factory = new SpecializedWebRequestFactory(sessionManager.Object);
			var request = factory.CreateFuncRequest<IntegrationData[]>($"integrations/my");
			Assert.AreEqual(2, request.Converters.Count());
		}
	}
}
