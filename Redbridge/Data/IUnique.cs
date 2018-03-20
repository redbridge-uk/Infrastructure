using System;
namespace Redbridge.SDK
{
    public interface IUnique<out TKey> 
        where TKey : IEquatable<TKey>
	{
		TKey Id { get; }
	}
}
