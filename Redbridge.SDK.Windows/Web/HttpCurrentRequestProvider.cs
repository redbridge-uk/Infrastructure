using System.Web;

namespace Redbridge.Web
{
	public class HttpCurrentRequestProvider : IHttpRequestContextProvider
	{
		public HttpRequest Current => HttpContext.Current.Request;
	}
}
