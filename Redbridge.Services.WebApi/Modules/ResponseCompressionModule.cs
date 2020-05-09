using System;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace Redbridge.Services.WebApi.Modules
{
	public class ResponseCompressionModule : IHttpModule
	{
		private const string AcceptedEncoding = "Accept-Encoding";
		private const string ContentEncoding = "Content-Encoding";
		private const string Deflate = "deflate";
		private const string GZip = "gzip";

		public void Dispose() { }

		public void Init(HttpApplication app)
		{
			app.PreRequestHandlerExecute += Compress;
		}

		private void Compress(object sender, EventArgs e)
		{
			var app = (HttpApplication)sender;
			var request = app.Request;
			var response = app.Response;

			if (response.StatusCode < 400 &&
				request.Headers.AllKeys.Contains(AcceptedEncoding) &&
				!string.IsNullOrEmpty(request.Headers[AcceptedEncoding]))
			{
				var acceptEncoding = request.Headers[AcceptedEncoding];
				acceptEncoding = acceptEncoding.ToLower(CultureInfo.InvariantCulture);

				if (acceptEncoding.Contains(GZip))
				{
					response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
					response.AddHeader(ContentEncoding, GZip);
				}
				else if (acceptEncoding.Contains(Deflate))
				{
					response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
					response.AddHeader(ContentEncoding, Deflate);
				}
			}
		}
	}
}
