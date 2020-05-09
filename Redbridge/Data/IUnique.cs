using System;

namespace Redbridge.Data
{
    public interface IUnique<out TKey> 
        where TKey : IEquatable<TKey>
	{
		TKey Id { get; }
	}
}
