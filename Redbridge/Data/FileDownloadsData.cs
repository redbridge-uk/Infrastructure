﻿using System;

namespace Redbridge.Data
{
	public class FileDownloadsData : IDisposable
	{
		public string FileName { get; set; }

		public FileDownloadData[] Files { get; set; }
		public void Dispose()
		{
			foreach (var file in Files)
			{
				file.Dispose();
			}
		}
	}
}
