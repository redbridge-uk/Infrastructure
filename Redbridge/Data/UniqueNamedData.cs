using System;
using System.Runtime.Serialization;
using Redbridge.SDK;

namespace Redbridge.Data
{
	[DataContract]
	public abstract class UniqueNamedData<TKey, TEntity> : UniqueData<TKey>, INamed
		where TKey : IEquatable<TKey>
		where TEntity : IUnique<TKey>, INamed
	{
		protected UniqueNamedData() { }

		protected UniqueNamedData(TKey id, string name) : base(id)
		{
			Name = name;
		}

		protected UniqueNamedData(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			Id = entity.Id;
			Name = entity.Name;
		}

		[DataMember]
		public string Name
		{
			get;
			set;
		}
	}

	[DataContract]
	public abstract class UniqueNamedData<TKey> : UniqueData<TKey>, INamed
		where TKey : IEquatable<TKey>
	{
		protected UniqueNamedData() { }

		protected UniqueNamedData(TKey id, string name) : base(id)
		{
			Name = name;
		}

		[DataMember]
		public string Name
		{
			get;
			set;
		}
	}
}
