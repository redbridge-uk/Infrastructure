using System.Linq;
using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    [TestFixture]
    public class HttpStreamRequestTests
    {
        [Test]
        public void Construct_HttpStreamRequest_ExpectSuccess()
        {
            var request = new HttpStreamRequest("https://localhost:1234", HttpVerb.Post);
            Assert.AreEqual(0, request.Converters.Count());
        }
    }
}