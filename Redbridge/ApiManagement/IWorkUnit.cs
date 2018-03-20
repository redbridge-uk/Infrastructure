using System;
using System.Threading.Tasks;

namespace Redbridge.ApiManagement
{
	public interface IWorkUnit : IDisposable
	{
		Task<int> SaveChangesAsync();
	}
}
