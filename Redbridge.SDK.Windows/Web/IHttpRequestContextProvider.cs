using System.Web;

namespace Redbridge.Web
{
	public interface IHttpRequestContextProvider
	{
		HttpRequest Current { get; }
	}
}
