using System;
using System.IO;

namespace Redbridge.SDK.Data
{
	public class FileDownloadData : IDisposable
	{
		public Stream FileStream { get; set; }

		public string FileName { get; set; }

		public string ContentType { get; set; }

		public void Dispose()
		{
			FileStream?.Dispose();
		}
	}
}
