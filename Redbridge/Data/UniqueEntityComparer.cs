using System;
using System.Collections.Generic;

namespace Redbridge.SDK.Data
{
	public class UniqueEntityComparer<TEntity, TKey> : IEqualityComparer<TEntity>
		where TEntity : IUnique<TKey>
		where TKey : IEquatable<TKey>
	{
		public bool Equals(TEntity x, TEntity y)
		{
			return x.Id.Equals(y.Id);
		}

		public int GetHashCode(TEntity obj)
		{
			return obj.Id.GetHashCode();
		}
	}
}
