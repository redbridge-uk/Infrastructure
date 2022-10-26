using System;
using System.IO;
using Newtonsoft.Json;

namespace Redbridge.IO
{
	public static class StreamExtensions
	{
        public static void WriteFile(this Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

        public static void SaveAs(this Stream stream, string filePath)
        {
            var bytes = stream.ToByteArray();
            File.WriteAllBytes(filePath, bytes);
        }

		public static MemoryStream ToMemoryStream(this Stream source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			var memoryStream = new MemoryStream();
			if (source.CanSeek && source.Position > 0)
			{
				source.Position = 0;
			}
			source.CopyTo(memoryStream);
			memoryStream.Position = 0;
			return memoryStream;
		}

		public static string AsString(this Stream input)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));

			var targetStream = input.ToMemoryStream();

			using (var reader = new StreamReader(targetStream))
			{
				string streamText = reader.ReadToEnd();
				return streamText;
			}
		}

		public static byte[] ToByteArray(this Stream input)
		{
			if (input == null)
				throw new ArgumentNullException(nameof(input));

			input.Position = 0;
			var buffer = new byte[16 * 1024];

			using (var ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		public static object DeserializeJson(this Stream stream)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			object result;

			using (var reader = new StreamReader(stream))
			{
				var serializer = new JsonSerializer();
				using (var jsonReader = new JsonTextReader(reader))
				{
					result = serializer.Deserialize(jsonReader);
					jsonReader.Close();
				}
			}

			return result;
		}
	}
}
