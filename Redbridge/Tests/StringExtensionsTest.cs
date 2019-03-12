using NUnit.Framework;
using Redbridge.IO;

namespace Redbridge.Tests
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        public void TestSurroundsWith()
        {
            string input = "I am a column name";
            string result = input.SurroundWith("'");
            Assert.AreEqual("'I am a column name'", result, "Unexpected surrounded text.");
        }
        
        [Test]
        public void TestQuoted()
        {
            string input = "I am a column name";
            string result = input.SingleQuote();
            Assert.AreEqual("'I am a column name'", result, "Unexpected surrounded text.");
        }
        
        [Test]
        public void TestDoubleQuoted()
        {
            string input = "I am a column name";
            string result = input.DoubleQuote();
            Assert.AreEqual("\"I am a column name\"", result, "Unexpected surrounded text.");
        }
        
        [Test]
        public void TestToCamelCaseBlankString()
        {
            string input = "";
            string result = input.ToCamelCase();
            Assert.AreEqual("", result);
        }
        
        [Test]
        public void TestToCamelCaseSingleLetter()
        {
            string input = "A";
            string result = input.ToCamelCase();
            Assert.AreEqual("a", result);
        }
        
        [Test]
        public void TestToCamelCaseFullWord()
        {
            string input = "Airport";
            string result = input.ToCamelCase();
            Assert.AreEqual("airport", result);
        }
        
        [Test]
        public void TestToCamelCaseFullWordAlreadyCased()
        {
            string input = "airport";
            string result = input.ToCamelCase();
            Assert.AreEqual("airport", result);
        }
    }
}
