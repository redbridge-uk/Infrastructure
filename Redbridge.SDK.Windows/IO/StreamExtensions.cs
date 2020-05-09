using System.IO;

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
	}
}
