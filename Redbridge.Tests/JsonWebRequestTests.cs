using System;
using System.Linq;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    [TestFixture]
    public class JsonWebRequestTests
    {
        public class TestWebRequest : JsonWebRequest
        {
            public TestWebRequest(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb)
            {
            }
        }

        [Test]
        public void Construct_JsonWebRequest_ExpectSuccess()
        {
            var request = new TestWebRequest("https://localhost:1234", HttpVerb.Post);
            Assert.AreEqual(0, request.Converters.Count());
            Assert.AreEqual("application/json", request.ContentType);
            Assert.AreEqual(AuthenticationMethod.Bearer, request.AuthenticationMethod);
            Assert.AreEqual(HttpVerb.Post, request.HttpVerb);
            Assert.AreEqual(0, request.Parameters.Count);
            Assert.IsNull(request.RootUri);
            Assert.AreEqual(new Uri("https://localhost:1234"), request.RequestUri);
            Assert.IsNull(request.Logger);
            Assert.IsFalse(request.RequiresSignature);
        }

        [Test]
        public void JsonWebRequest_AddParameter_ExpectSuccess()
        {
            var request = new TestWebRequest("https://localhost:1234", HttpVerb.Post);
            var mockClient = new Mock<IHttpClientFactory>();
            mockClient.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(new HttpClient());
            request.Parameters.Add("Tester");
            Assert.AreEqual(1, request.Parameters.Count);
        }

        [Test]
        public void JsonWebRequest_ToHttpClient_ExpectSuccess()
        {
            var request = new TestWebRequest("https://localhost:1234", HttpVerb.Post);
            var mockClient = new Mock<IHttpClientFactory>();
            mockClient.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(new HttpClient());
            var client = request.ToHttpClient(mockClient.Object);
            Assert.IsNotNull(client);
        }

        [Test]
        public void JsonWebRequest_RegisterConverters_ExpectSuccess()
        {
            var request = new TestWebRequest("https://localhost:1234", HttpVerb.Post);
            request.RegisterConverters(new []{ new TestIntegrationJsonConverter() });
            Assert.AreEqual(1, request.Converters.Count());
        }
    }
}