using NUnit.Framework;
using Redbridge.IO;

namespace Redbridge.Tests
{
    [TestFixture]
    public class TestStringExtensions
    {
        [Test]
        public void TestDoubleQuotedNullString()
        {
            string nullString = null;
            string resultString = nullString.DoubleQuote();
            Assert.AreEqual(null, resultString, "Expected a null result.");
        }
        
        [Test]
        public void TestDoubleQuotedString()
        {
            string inputString = "This is some text with commas,isn't that right?";
            string resultString = inputString.DoubleQuote();
            Assert.AreEqual("\"This is some text with commas,isn't that right?\"", resultString, "Expected a set of quotes around the input.");
        }
        
        [Test]
        public void TestSingleQuotedNullString()
        {
            string nullString = null;
            string resultString = nullString.SingleQuote();
            Assert.AreEqual(null, resultString, "Expected a set of quotes as the output result: ''");
        }
        
        [Test]
        public void TestSingleQuotedString()
        {
            string inputString = "This is some text with commas,isn't that right?";
            string resultString = inputString.SingleQuote();
            Assert.AreEqual("'This is some text with commas,isn't that right?'", resultString, "Expected a set of single quotes around the input.");
        }
        
        [Test]
        public void TestSurroundWithString()
        {
            string inputString = "Some Sentence";
            string resultString = inputString.SurroundWith("#");
            Assert.AreEqual("#Some Sentence#", resultString, "Expected a set of hash marks around the input: #Some Sentence#");
        }
    }
}
