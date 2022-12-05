using System;
using System.Threading.Tasks;

namespace Redbridge.DependencyInjection
{
	public interface IWorkUnit
	{
		Task<int> SaveChangesAsync();
	}
}
