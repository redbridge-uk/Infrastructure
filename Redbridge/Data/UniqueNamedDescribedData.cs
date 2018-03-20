using System;
using System.Runtime.Serialization;
using Redbridge.SDK;

namespace Redbridge.Data
{
	[DataContract]
	public abstract class UniqueNamedDescribedData<TKey, TEntity> : UniqueNamedData<TKey, TEntity>, IDescribed
		where TKey : IEquatable<TKey>
		where TEntity : IUnique<TKey>, INamed, IDescribed
	{
		protected UniqueNamedDescribedData() { }

		protected UniqueNamedDescribedData(TEntity entity) : base(entity)
		{
			Description = entity.Description;
		}

		[DataMember]
		public string Description
		{
			get;
			set;
		}
	}

	[DataContract]
	public abstract class UniqueNamedDescribedData<TKey> : UniqueNamedData<TKey>, IDescribed
		where TKey : IEquatable<TKey>
	{
		[DataMember]
		public string Description
		{
			get;
			set;
		}
	}
}
