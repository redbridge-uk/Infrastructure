﻿using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
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
			if (jObject.TryGetValue("type", StringComparison.CurrentCultureIgnoreCase, out _))
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

	public class SpecializedWebRequestFactory : WebRequestFactory<SpecializedWebRequestFactory>
	{
		public SpecializedWebRequestFactory(IWebRequestSignatureService sessionManager, ILogger<SpecializedWebRequestFactory> factory, IHttpClientFactory clientFactory) 
            : base(new Uri("http://mytest-api.azurewebsites.net"), sessionManager, factory, clientFactory) { }

		protected override IWebRequestFactory OnCreateFactory(Uri baseUri, IWebRequestSignatureService sessionManager, ILogger<SpecializedWebRequestFactory> factory)
		{
			return new SpecializedWebRequestFactory(sessionManager, factory, ClientFactory);
		}

		protected override void OnRegisterConverters()
		{
			base.OnRegisterConverters();
			Converters.Add(new TestIntegrationJsonConverter());
		}
	}

	[TestFixture]
	public class WebRequestFactoryTests
	{
		[Test]
		public void CreateSpecializedFactoryFuncRequestExpectCustomJsonConverter()
		{
			var sessionManager = new Mock<IWebRequestSignatureService>();
            sessionManager.Setup(sm => sm.PostProcessUrlString(It.IsAny<string>())).Returns("integrations/my");
            var clientFactory = new Mock<IHttpClientFactory>();
            var loggerFactory = new Mock<ILogger<SpecializedWebRequestFactory>>();
			var factory = new SpecializedWebRequestFactory(sessionManager.Object, loggerFactory.Object, clientFactory.Object);
			var request = factory.CreateFuncRequest<IntegrationData[]>("integrations/my");
			Assert.AreEqual(2, request.Converters.Count());
            Assert.AreEqual(new Uri("http://mytest-api.azurewebsites.net"), request.RootUri);
            Assert.AreEqual(new Uri("http://mytest-api.azurewebsites.net/integrations/my"), request.RequestUri);
        }
	}
}
