using System.Threading.Tasks;
using Redbridge.ApiManagement;

namespace Redbridge.SDK
{

	public interface IApiContextAuthorizer<TContext>
	{
		Task AuthorizeAsync(IApiCall api, TContext context);
	}
}
