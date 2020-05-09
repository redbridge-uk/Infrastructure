using System.Threading.Tasks;

namespace Redbridge.ApiManagement
{
	public interface IApiContextProvider<TContext>
	{
		Task<TContext> GetCurrentAsync();
	}

	
}
