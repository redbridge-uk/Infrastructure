using System;
using System.Runtime.Serialization;

namespace Redbridge.Data
{
	[DataContract]
	public abstract class NamedUnique<TEntity, TKey> : UniqueData<TKey>, INamed
	   where TEntity : INamed, IUnique<TKey>
	   where TKey : IEquatable<TKey>
	{
		protected NamedUnique(TEntity entity) : base(entity.Id)
		{
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
	public abstract class NamedUniqueData<TKey> : UniqueData<TKey>, INamed
		where TKey : IEquatable<TKey>
	{
		protected NamedUniqueData()
		{ }

		protected NamedUniqueData(TKey id, string name)
			: base(id)
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
