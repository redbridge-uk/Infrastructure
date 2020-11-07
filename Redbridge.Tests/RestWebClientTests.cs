using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    [TestFixture]
    public class RestWebClientTests
    {
        public class TestRestWebClient : RestWebClient
        {
            public TestRestWebClient(IWebRequestFactory webRequestFactory, IHttpClientFactory clientFactory) : base(webRequestFactory, clientFactory)
            {
            }
        }

        [Test]
        public void Construct_RestWebClient_ExpectSuccess()
        {
            var mockFactory = new Mock<IWebRequestFactory>();
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var client = new TestRestWebClient(mockFactory.Object, mockHttpClientFactory.Object);
            Assert.IsNotNull(client);
            Assert.IsFalse(client.IsConnected);
        }
    }
}
