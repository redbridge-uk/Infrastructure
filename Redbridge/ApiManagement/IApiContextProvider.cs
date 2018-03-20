using System.Threading.Tasks;

namespace Redbridge.SDK
{
	public interface IApiContextProvider<TContext>
	{
		Task<TContext> GetCurrentAsync();
	}

	
}
