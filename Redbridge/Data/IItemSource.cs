using System;
using System.Threading.Tasks;

namespace Redbridge.SDK
{
	public interface IItemSource<T>
	{
		T[] GetAll();
	}

	public interface IFilteredItemSource<T>
	{
		T[] GetFiltered(Func<T, bool> filter);
	}
}
