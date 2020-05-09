using System;
using System.Runtime.Serialization;

namespace Redbridge.Data
{
[DataContract]
public abstract class AuditedTimestampedNamedUniqueData<TKey, TUserKey> : TimestampedNamedUniqueData<TKey>, IAudited<TUserKey>
	where TKey : IEquatable<TKey> where TUserKey : struct
{
	protected AuditedTimestampedNamedUniqueData(TKey id, string name)
		: base(id, name) { }

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
	public TUserKey? DeletedBy
	{
		get;
		set;
	} }

}
