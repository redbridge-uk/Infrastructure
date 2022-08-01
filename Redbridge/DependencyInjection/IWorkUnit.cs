using System;
using System.Threading.Tasks;

namespace Redbridge.DependencyInjection
{
	public interface IWorkUnit : IDisposable
	{
		Task<int> SaveChangesAsync();
	}
}
