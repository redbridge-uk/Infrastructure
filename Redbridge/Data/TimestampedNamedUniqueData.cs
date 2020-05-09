using System;
using System.Runtime.Serialization;

namespace Redbridge.Data
{
	[DataContract]
	public abstract class TimestampedNamedUniqueData<TKey>
		: NamedUniqueData<TKey>, ITimestamped
		where TKey : IEquatable<TKey>
	{
		protected TimestampedNamedUniqueData(TKey id, string name) : base(id, name)
		{
			Created = DateTime.UtcNow;
			Updated = DateTime.UtcNow;
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
		public DateTime? Deleted { get; set; }
	}
}
