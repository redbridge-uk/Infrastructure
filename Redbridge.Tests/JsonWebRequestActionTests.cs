using System.Linq;
using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    [TestFixture]
    public class JsonWebRequestActionTests
    {
        public class TestWebRequestAction : JsonWebRequestAction
        {
            public TestWebRequestAction(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb)
            {
            }
        }

        [Test]
        public void Construct_JsonWebRequestAction_ExpectSuccess()
        {
            var request = new TestWebRequestAction("https://localhost:1234", HttpVerb.Post);
            Assert.AreEqual(0, request.Converters.Count());
            Assert.AreEqual("application/json", request.ContentType);
        }
    }
}