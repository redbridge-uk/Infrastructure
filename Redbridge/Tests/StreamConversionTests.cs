using NUnit.Framework;
using Redbridge.IO;

namespace Tests
{
	[TestFixture()]
	public class StreamConversionTests
	{
		[Test]
		public void TestToStreamFromStringMethod()
		{
			string inputString = "My String Test";
			var stream = inputString.ToStream();

			var retrievedString = stream.AsString();
			Assert.AreEqual(inputString, retrievedString, "Unexpected recovered string.");
		}
	}
}
