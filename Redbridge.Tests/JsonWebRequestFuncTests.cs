using System.Linq;
using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    [TestFixture]
    public class JsonWebRequestFuncTests
    {
        public class TestWebRequestFunc : JsonWebRequestAction
        {
            public TestWebRequestFunc(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb)
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
    }
}