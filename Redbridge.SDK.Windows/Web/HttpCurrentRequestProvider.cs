using System.Web;

namespace Redbridge.Windows.Web
{
	public class HttpCurrentRequestProvider : IHttpRequestContextProvider
	{
		public HttpRequest Current => HttpContext.Current.Request;
	}
}
