using NUnit.Framework;
using Redbridge.Web;

namespace Redbridge.Tests.Web
{
    [TestFixture]
    public class HttpValueTests
    {
        [Test]
        public void Construct_HttpValue_CheckKeyValue()
        {
            var value = new HttpValue("Key", "Value");
            Assert.AreEqual("Key", value.Key);
            Assert.AreEqual("Value", value.Value);
        }
    }
}
