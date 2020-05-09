using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Ionic.Zip;
using Redbridge.Data;

namespace Redbridge.Services.WebApi.Results
{
	public static class FileDownloadDataExtensions
	{
		public static HttpResponseMessage AsFileResult(this FileDownloadData downloadData)
		{
			var result = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StreamContent(downloadData.FileStream)
			};

			result.Content.Headers.ContentType = new MediaTypeHeaderValue(downloadData.ContentType);
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = downloadData.FileName
			};

			return result;
		}

		public static HttpResponseMessage AsZippedFileResult(this FileDownloadsData downloadData)
		{
			using (var zipFile = new ZipFile(downloadData.FileName))
			{
				foreach (var download in downloadData.Files)
				{
					zipFile.AddEntry(download.FileName, download.FileStream);
				}
				return ZipContentResult(zipFile, downloadData.FileName);
			}
		}

		private static HttpResponseMessage ZipContentResult(ZipFile zipFile, string filename)
		{
			var pushStreamContent = new PushStreamContent((stream, content, context) =>
			{
				zipFile.Save(stream);
				stream.Close(); // After save we close the stream to signal that we are done writing.
			}, "application/zip");

			var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = pushStreamContent };
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
			response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = filename
			};
			return response;
		}
	}
}
