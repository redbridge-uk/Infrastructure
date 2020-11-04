using Redbridge.Data;

namespace Redbridge.Web.Messaging
{
	public abstract class JsonWebPageRequest<TResult, TInput1> : JsonWebRequestFunc<PagedResultSet<TResult>, TInput1>
	{
		protected JsonWebPageRequest(string requestUri, HttpVerb httpVerb) : base(requestUri, httpVerb)
		{
			Page = 1;
			Size = 20;
			Filter = string.Empty;
			Sort = string.Empty;
		}

		protected override string AppendQuery(string baseUri)
		{
			var baseString = base.AppendQuery(baseUri);
			var pageRequest = PageRequestParser.ParseUrlRequest(Page, Size, Sort, Filter);
			return $"{baseString}?{pageRequest.ToQueryString()}";
		}

		public int Page { get; set; }

		public int Size { get; set; }

		public string Filter { get; set; }

		public string Sort { get; set; }
	}


	public abstract class JsonWebPageRequest<TResult> : JsonWebRequestFunc<PagedResultSet<TResult>>
	{
		protected JsonWebPageRequest(string requestUri, HttpVerb httpVerb) 
            : base(requestUri, httpVerb)
		{
			Page = 1;
			Size = 20;
			Filter = string.Empty;
			Sort = string.Empty;
		}

		protected override string AppendQuery(string baseUri)
		{
			var baseString = base.AppendQuery(baseUri);
			var pageRequest = PageRequestParser.ParseUrlRequest(Page, Size, Sort, Filter);
			return $"{baseString}?{pageRequest.ToQueryString()}";
		}

		public int Page { get; set; }

		public int Size { get; set; }

		public string Filter { get; set; }

		public string Sort { get; set; }
	}
}
