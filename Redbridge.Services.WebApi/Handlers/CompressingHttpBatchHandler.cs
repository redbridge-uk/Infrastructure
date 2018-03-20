using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Batch;

namespace Redbridge.Services.WebApi.Handlers
{
	public class CompressingHttpBatchHandler : DefaultHttpBatchHandler
	{
		public CompressingHttpBatchHandler(HttpServer httpServer) : base(httpServer) { }

		public override Task<HttpResponseMessage> ProcessBatchAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return base.ProcessBatchAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>((responseToCompleteTask) =>
			{
				var response = responseToCompleteTask.Result;

				if (response.RequestMessage.Headers.AcceptEncoding != null && response.RequestMessage.Headers.AcceptEncoding.Any() )
				{
                    // IIS only supports gzip at the moment so this avoids the Safari issue of the 'Br' compression type.
					var encodingType = response.RequestMessage.Headers.AcceptEncoding.FirstOrDefault(s => s.Value == "gzip" || s.Value == "deflate").Value;
                    if ( encodingType != null )
					    response.Content = new CompressedContent(response.Content, encodingType);
				}

				return response;
			},
				TaskContinuationOptions.OnlyOnRanToCompletion);
		}

		public class CompressedContent : HttpContent
		{
			private readonly HttpContent _originalContent;
			private readonly string _encodingType;

			public CompressedContent(HttpContent content, string encodingType)
			{
				if (content == null) throw new ArgumentNullException(nameof(content));
				if (encodingType == null) throw new ArgumentNullException(nameof(encodingType));

				_originalContent = content;
				_encodingType = encodingType.ToLowerInvariant();

				if (_encodingType != "gzip" && _encodingType != "deflate")
				{
					throw new InvalidOperationException($"Encoding '{_encodingType}' is not supported. Only supports gzip or deflate encoding.");
				}

				foreach (var header in _originalContent.Headers)
				{
					Headers.TryAddWithoutValidation(header.Key, header.Value);
				}

				Headers.ContentEncoding.Add(encodingType);
			}

			protected override bool TryComputeLength(out long length)
			{
				length = -1;
				return false;
			}

			protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
			{
				Stream compressedStream = null;

				switch (_encodingType)
				{
					case "gzip":
						compressedStream = new GZipStream(stream, CompressionMode.Compress, leaveOpen: true);
						break;
					case "deflate":
						compressedStream = new DeflateStream(stream, CompressionMode.Compress, leaveOpen: true);
						break;
				}

				return _originalContent.CopyToAsync(compressedStream).ContinueWith(tsk =>
				{
					compressedStream?.Dispose();
				});
			}
		}
	}
}
