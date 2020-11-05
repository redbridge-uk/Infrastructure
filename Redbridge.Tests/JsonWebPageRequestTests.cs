using System.Linq;
using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    [TestFixture]
    public class JsonWebPageRequestTests
    {
        public class TestPageRequest : JsonWebPageRequest<TestPageResult>
        {
            public TestPageRequest(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb)
            {
            }
        }

        public class TestPageResult
        {
        }

        [Test]
        public void Construct_JsonWebPageRequest_ExpectSuccess()
        {
            var request = new TestPageRequest("https://localhost:1234", HttpVerb.Post);
            Assert.AreEqual(0, request.Converters.Count());
            Assert.AreEqual("application/json", request.ContentType);
        }
    }
}