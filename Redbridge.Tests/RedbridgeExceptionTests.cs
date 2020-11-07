using System.IO;
using NUnit.Framework;

namespace Redbridge.Tests
{
    [TestFixture]
    public class RedbridgeExceptionTests
    {
        [Test]
        public void Construct_RedbridgeException_ExpectSuccess()
        {
            var ex = new RedbridgeException();
            Assert.AreEqual("Exception of type 'Redbridge.RedbridgeException' was thrown.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [Test]
        public void Construct_RedbridgeExceptionWithMessage_ExpectSuccess()
        {
            var ex = new RedbridgeException("Something bad happened");
            Assert.AreEqual("Something bad happened", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [Test]
        public void Construct_RedbridgeExceptionWithMessageAndInnerException_ExpectSuccess()
        {
            var inner = new FileNotFoundException();
            var ex = new RedbridgeException("Something bad happened", inner);
            Assert.AreEqual("Something bad happened", ex.Message);
            Assert.AreSame(inner, ex.InnerException);
        }
    }
}
