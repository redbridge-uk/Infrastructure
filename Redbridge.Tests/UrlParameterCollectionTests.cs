using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    [TestFixture]
    public class UrlParameterCollectionTests
    {
        [Test]
        public void Construct_UrlParameterCollection_ExpectSuccess()
        {
            var parameters = new UrlParameterCollection();
            Assert.AreEqual(0, parameters.Count);
        }

        [Test]
        public void UrlParameterCollection_AddParameter_ExpectSuccess()
        {
            var parameters = new UrlParameterCollection();
            parameters.Add("MyParam");
            Assert.AreEqual(1, parameters.Count);
        }

        [Test]
        public void UrlParameterCollection_AddParameterWithValue_ExpectSuccess()
        {
            var parameters = new UrlParameterCollection();
            parameters.Add("MyParam", "MyValue");
            Assert.AreEqual(1, parameters.Count);
        }
    }
}
