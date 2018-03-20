using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Redbridge.Text
{
	public class GZipStringCompressor : IStringCompressor
	{
		string IStringCompressor.Compress(string input)
		{
			return Compress(input);
		}

		string IStringCompressor.Decompress(string input)
		{
			return Decompress(input);
		}

		public static string Compress(string input)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(input);
			var memoryStream = new MemoryStream();

			using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
			{
				gZipStream.Write(buffer, 0, buffer.Length);
			}

			memoryStream.Position = 0;

			var compressedData = new byte[memoryStream.Length];
			memoryStream.Read(compressedData, 0, compressedData.Length);

			var gZipBuffer = new byte[compressedData.Length + 4];
			Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
			Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
			return Convert.ToBase64String(gZipBuffer);
		}

		public static string Decompress(string input)
		{
			if (!string.IsNullOrEmpty(input))
			{
				byte[] gZipBuffer = Convert.FromBase64String(input);

				using (var memoryStream = new MemoryStream())
				{
					int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
					memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

					var buffer = new byte[dataLength];

					memoryStream.Position = 0;
					using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
					{
						gZipStream.Read(buffer, 0, buffer.Length);
					}

					return Encoding.UTF8.GetString(buffer);
				}
			}
			else
			{
				return input;
			}
		}
	}
}
