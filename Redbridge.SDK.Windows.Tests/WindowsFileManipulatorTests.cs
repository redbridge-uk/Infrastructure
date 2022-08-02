using NUnit.Framework;
using Redbridge.IO;

namespace Redbridge.Windows.Tests
{
    [TestFixture]
    public class WindowsFileManipulatorTests
    {
        [Test]
        public void Create_RandomPath_ExpectSuccess()
        {
            var manipulator = new WindowsFileManipulator();
            var fileName = manipulator.GetTempFileName();
            Assert.IsNotEmpty(fileName);
        }
    }
}
