using System;
using System.Runtime.Serialization;

namespace Redbridge.Data
{
	[DataContract]
	public abstract class AuditedTimestampedNamedDescribedUniqueData<TKey, TUserKey> : TimestampedNamedDescribedUniqueData<TKey>, IAudited<TUserKey>
		where TKey : IEquatable<TKey> where TUserKey : struct
	{

		protected AuditedTimestampedNamedDescribedUniqueData(TKey id, string name)
			: this(id, name, string.Empty) { }

		protected AuditedTimestampedNamedDescribedUniqueData(TKey id, string name, string description)
			: base(id, name, description) { }

		[DataMember]
		public TUserKey CreatedBy
		{
			get;
			set;
		}

		[DataMember]
		public TUserKey UpdatedBy
		{
			get;
			set;
		}

		[DataMember]
		public TUserKey? DeletedBy { get; set; }
	}
}


