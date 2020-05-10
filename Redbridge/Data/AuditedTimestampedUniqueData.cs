using System;
using System.Runtime.Serialization;

namespace Redbridge.Data
{
    [DataContract]
    public abstract class AuditedTimestampedUniqueData<TKey, TUserKey> : TimestampedUniqueData<TKey>, IAudited<TUserKey>
        where TKey : IEquatable<TKey>
        where TUserKey : struct
    {
        protected AuditedTimestampedUniqueData(TKey id) : base(id) {}

        [DataMember] public TUserKey CreatedBy { get; set; }

        [DataMember] public TUserKey UpdatedBy { get; set; }

        [DataMember] public TUserKey? DeletedBy { get; set; }
    }

}
