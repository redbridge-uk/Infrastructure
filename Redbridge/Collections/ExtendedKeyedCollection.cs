using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Redbridge.Collections
{
	public abstract class ExtendedKeyedCollection<TKey, TItem> : KeyedCollection<TKey, TItem>
	{
		public bool TryGetValue(TKey key, out TItem item)
		{
			if (Contains(key))
			{
				item = this[key];
				return true;
			}
			item = default(TItem);
			return false;
		}

		public void AddRange(IEnumerable<TItem> collection)
		{
			if (collection != null)
			{
				foreach (TItem item in collection)
				{
					Add(item);
				}
			}
		}

		public void Update(TItem item)
		{
			TKey key = GetKeyForItem(item);
			TItem oldItem;

			if (TryGetValue(key, out oldItem))
			{
				int index = Items.IndexOf(oldItem);
				SetItem(index, item);
			}
			else
				throw new KeyNotFoundException("The key for the specified item could not be found in the collection");
		}
	}
}
