using System.Threading.Tasks;

namespace Redbridge.ApiManagement
{

	public interface IApiContextAuthorizer<TContext>
	{
		Task AuthorizeAsync(IApiCall api, TContext context);
	}
}
