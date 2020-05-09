using System.Web;

namespace Redbridge.Windows.Web
{
	public interface IHttpRequestContextProvider
	{
		HttpRequest Current { get; }
	}
}
