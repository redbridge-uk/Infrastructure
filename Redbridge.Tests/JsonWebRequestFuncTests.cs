using System;
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
        private HttpRequestMessage _message;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _message = request;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }

        public HttpRequestMessage LastMessage => _message;
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
        public void Construct_JsonWebRequestDeleteFunc_ExpectSuccess()
        {
            var request = new TestWebRequestFunc("https://localhost:1234", HttpVerb.Delete);
            Assert.AreEqual(0, request.Converters.Count());
            Assert.AreEqual("application/json", request.ContentType);
        }

        [Test]
        public void Construct_JsonWebRequestGetFunc_ExpectSuccess()
        {
            var request = new TestWebRequestFunc("https://localhost:1234", HttpVerb.Get);
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
            mockHttpRequestFactory.Setup(rf => rf.CreateClient(It.IsAny<string>())).Returns(new HttpClient(new TestHttpHandler()));
            await request.ExecuteAsync(mockHttpRequestFactory.Object, 42);
        }

        [Test]
        public async Task Construct_JsonWebRequestFuncPut_ExpectSuccess()
        {
            var request = new TestWebRequestBodyFunc("https://localhost:1234", HttpVerb.Put);
            Assert.AreEqual(0, request.Converters.Count());
            Assert.AreEqual("application/json", request.ContentType);

            var mockHttpRequestFactory = new Mock<IHttpClientFactory>();
            mockHttpRequestFactory.Setup(rf => rf.CreateClient(It.IsAny<string>())).Returns(new HttpClient(new TestHttpHandler()));
            await request.ExecuteAsync(mockHttpRequestFactory.Object, 42);
        }

        [Test]
        public async Task Construct_JsonWebRequestFuncPost_ExpectSuccess()
        {
            var request = new TestWebRequestBodyFunc("https://localhost:1234", HttpVerb.Post);
            Assert.AreEqual(0, request.Converters.Count());
            Assert.AreEqual("application/json", request.ContentType);

            var mockHttpRequestFactory = new Mock<IHttpClientFactory>();
            mockHttpRequestFactory.Setup(rf => rf.CreateClient(It.IsAny<string>())).Returns(new HttpClient(new TestHttpHandler()));
            await request.ExecuteAsync(mockHttpRequestFactory.Object, 42);
        }

        [Test]
        public void Construct_JsonWebRequestFuncDelete_ExpectException()
        {
            var request = new TestWebRequestBodyFunc("https://localhost:1234", HttpVerb.Delete);
            Assert.AreEqual(0, request.Converters.Count());
            Assert.AreEqual("application/json", request.ContentType);

            var mockHttpRequestFactory = new Mock<IHttpClientFactory>();
            mockHttpRequestFactory.Setup(rf => rf.CreateClient(It.IsAny<string>())).Returns(new HttpClient(new TestHttpHandler()));
            var nse = Assert.ThrowsAsync<NotSupportedException>(() => request.ExecuteAsync(mockHttpRequestFactory.Object, 42));
            Assert.IsNotNull(nse);
            Assert.AreEqual("Only patch, post and put are currently supported for sending requests with a body.", nse.Message);
        }
    }
}