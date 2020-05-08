using NUnit.Framework;
using System;
using Redbridge.Text;

namespace Tests
{
    [TestFixture()]
    public class GZipStringCompressorTestClass
    {
		[Test]
		public void CreateStringCompressorExpectSuccess()
		{
			new GZipStringCompressor();
		}

		[Test]
		public void StringCompressorCompressExpectSuccessAndResult()
		{
			IStringCompressor compressor = new GZipStringCompressor();
			var result = compressor.Compress("abc");
			Assert.IsNotNull(result);
		}

		[Test]
		public void StringCompressorCompressDecompressExpectInputAsResult()
		{
			IStringCompressor compressor = new GZipStringCompressor();
			var result = compressor.Compress("abc");
			var decompressed = compressor.Decompress(result);
			Assert.AreEqual("abc", decompressed);
		}

		[Test]
		public void StringCompressorDecompressEmptyStringExpectEmptyResult()
		{
			IStringCompressor compressor = new GZipStringCompressor();
			var decompressed = compressor.Decompress(string.Empty);
			Assert.AreEqual(string.Empty, decompressed);
		}

		[Test]
		public void StringCompressorCompressExpectSmallerStringThanInputForCompression()
		{
			var inputString = "This is my story, the story of a software developer. We have just got back from Morocco on a brief break. Once upon a time we began a project of what appeared to be small scale which evolved into a monster project. We are very proud of the project and wish to do well in our lives.";
			IStringCompressor compressor = new GZipStringCompressor();
			var result = compressor.Compress(inputString);
			Assert.IsTrue(inputString.Length > result.Length);
		}

		[Test]
		public void StringCompressorCompressExpectCanDecompressCompressedStringBack()
		{
			var compressedString = "GQEAAB+LCAAAAAAABAA1j1tuAyEMRbdyF1DNTqr+VOq3BzyBBDAyHkbZfU2iSJbl97n+TXnArT4xTPT5BUv8DiEHCEMOu0gZkScX6awb/hiJJuN+DsNNDDuFBw6Vim9RCUEgzXd3zXy4Z3ps+GmBcfZXw3JlXIydb7TyrnLnYIt4JTJQ7+zMCBOfwahUCkag4lsphwSeUqb3c/MJQpU2jPVz56VwaZ7sb3jxjOv0+uxDohZx5ZEWIYprcUBukFNR8uSx/QN2EfrIGQEAAA==";
			var expectedString = "This is my story, the story of a software developer. We have just got back from Morocco on a brief break. Once upon a time we began a project of what appeared to be small scale which evolved into a monster project. We are very proud of the project and wish to do well in our lives.";
			IStringCompressor compressor = new GZipStringCompressor();
			var result = compressor.Decompress(compressedString);
			Assert.AreEqual(result, expectedString);
		}
    }
}
