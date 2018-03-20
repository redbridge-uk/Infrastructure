using System;
namespace Redbridge.SDK
{
	public static class UriExtensions
	{
		public static Uri AddQueryParameter(this Uri uri, string name, string value)
		{
			if (uri == null) throw new ArgumentNullException(nameof(uri));
			var query = HttpUtility.ParseQueryString(string.Empty);
			query.Add(name, value);
			var queryString = query.ToString();
			var builder = new UriBuilder(uri) { Query = queryString };
			return builder.Uri;
		}
	}
}
