using NUnit.Framework;
using Redbridge.IO;

namespace Easilog.Tests
{
    [TestFixture]
    public class EmbeddedResourceReaderTest
    {
        [Test]
        public void ReadEmbeddedTextFile_FromCurrentAssembly_FullQualified()
        {
            var reader = new EmbeddedResourceReader();
            var result = reader.ReadContent("Resources.EmbeddedTextFile_1.txt");
            Assert.AreEqual("Some Text", result, "Unexpected result.");
        }
        
        [Test]
        public void ReadEmbeddedTextFile_FromCurrentAssembly_PathQualified()
        {
            var reader = new EmbeddedResourceReader("Resources");
            var result = reader.ReadContent("EmbeddedTextFile_1.txt");
            Assert.AreEqual("Some Text", result, "Unexpected result.");
        }
        
        [Test]
        public void ReadEmbeddedImage_FromCurrentAssembly_PathQualified()
        {
            var reader = new EmbeddedResourceReader("Resources");
            reader.ReadContent("AUD.gif");
        }
    }
}
