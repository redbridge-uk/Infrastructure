using NUnit.Framework;
using Redbridge.Security;

namespace Redbridge.Windows.Tests
{
    [TestFixture()]
    public class Sha256CryptoServiceHashingServiceTests
    {
        [Test()]
        public void HashString()
        {
            var hasher = new Sha256CryptoServiceHashingService();
            var result = hasher.CreateHash("Balls");
            Assert.AreEqual("DNSLq4ic/Ec8SN9GM4g7DZTMw+gzFIkSZi+qNJRKMs4=", result);
        }
    }
}
