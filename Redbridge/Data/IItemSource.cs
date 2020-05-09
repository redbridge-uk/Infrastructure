using System;

namespace Redbridge.Data
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
