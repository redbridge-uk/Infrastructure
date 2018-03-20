using System;
using System.Runtime.Serialization;

namespace Redbridge.SDK
{
	[DataContract]
	public abstract class TimestampedNamedDescribedUniqueData<TKey> : NamedDescribedUniqueData<TKey>, ITimestamped
		where TKey : IEquatable<TKey>
	{

		protected TimestampedNamedDescribedUniqueData(TKey id, string name) : this(id, name, string.Empty) { }

		protected TimestampedNamedDescribedUniqueData(TKey id, string name, string description) : base(id, name, description)
		{
			Created = DateTime.UtcNow;
			Updated = DateTime.UtcNow;
		}

		protected TimestampedNamedDescribedUniqueData(IUnique<TKey> source)
		{
			Id = source.Id;
		}

		[DataMember]
		public DateTime Created
		{
			get;
			set;
		}

		[DataMember]
		public DateTime Updated
		{
			get;
			set;
		}

		[DataMember]
		public DateTime? Deleted
		{
			get;
			set;
		}
	}
}
