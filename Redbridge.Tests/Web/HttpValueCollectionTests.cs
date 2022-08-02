using NUnit.Framework;
using Redbridge.Web;

namespace Redbridge.Tests.Web
{
    [TestFixture]
    public class HttpValueCollectionTests
    {
        [Test]
        public void Construct_HttpValueCollection_DefaultConstructor()
        {
            var collection = new HttpValueCollection();
            Assert.AreEqual(0, collection.Count);
        }
    }
}