using System;
using System.Linq;
using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    [TestFixture]
    public class FormWebRequestTests
    {
        public class TestResponse
        {
        }

        [Test]
        public void Construct_FormWebRequest_ExpectSuccess()
        {
            var request = new FormWebRequest<TestResponse>(new Uri("https://localhost:1234"), HttpVerb.Post);
            Assert.AreEqual("application/x-www-form-urlencoded", request.ContentType);
            Assert.AreEqual(0, request.Converters.Count());
        }
    }
}
