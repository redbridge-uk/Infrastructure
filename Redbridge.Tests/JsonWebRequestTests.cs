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
        }

        [Test]
        public void JsonWebRequest_ToHttpClient_ExpectSuccess()
        {
            var request = new TestWebRequest("https://localhost:1234", HttpVerb.Post);
            var mockClient = new Mock<IHttpClientFactory>();
            mockClient.Setup(c => c.Create()).Returns(new HttpClient());
            var client = request.ToHttpClient(mockClient.Object);
            Assert.IsNotNull(client);
        }
    }
}