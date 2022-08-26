using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    public class TestHttpHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }

    [TestFixture]
    public class JsonWebRequestFuncTests
    {
        public class TestWebRequestFunc : JsonWebRequestAction
        {
            public TestWebRequestFunc(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb)
            {
            }
        }

        public class TestWebRequestBodyFunc : JsonWebRequestAction<int>
        {
            public TestWebRequestBodyFunc(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb)
            {
            }
        }

        [Test]
        public void Construct_JsonWebRequestFunc_ExpectSuccess()
        {
            var request = new TestWebRequestFunc("https://localhost:1234", HttpVerb.Post);
            Assert.AreEqual(0, request.Converters.Count());
            Assert.AreEqual("application/json", request.ContentType);
        }

        [Test]
        public async Task Construct_JsonWebRequestFuncPatch_ExpectSuccess()
        {
            var request = new TestWebRequestBodyFunc("https://localhost:1234", HttpVerb.Patch);
            Assert.AreEqual(0, request.Converters.Count());
            Assert.AreEqual("application/json", request.ContentType);

            var mockHttpRequestFactory = new Mock<IHttpClientFactory>();
            mockHttpRequestFactory.Setup(rf => rf.Create()).Returns(new HttpClient(new TestHttpHandler()));
            await request.ExecuteAsync(mockHttpRequestFactory.Object, 42);
        }
    }
}