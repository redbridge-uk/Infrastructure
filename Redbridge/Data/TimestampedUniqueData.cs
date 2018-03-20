using System;
namespace Redbridge.SDK
{
public abstract class TimestampedUniqueData<TKey> : UniqueData<TKey>, ITimestamped
	where TKey : IEquatable<TKey>
{
	protected TimestampedUniqueData()
	{
		Created = DateTime.UtcNow;
		Updated = DateTime.UtcNow;
	}

	protected TimestampedUniqueData(TKey key) : base(key)
	{
		Created = DateTime.UtcNow;
		Updated = DateTime.UtcNow;
	}

	public DateTime Created
	{
		get;
		set;
	}

	public DateTime Updated
	{
		get;
		set;
	}

	public DateTime? Deleted
	{
		get;
		set;
	} }
}
