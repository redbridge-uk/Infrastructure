using System;
using System.Runtime.Serialization;

namespace Redbridge.Data
{
	[DataContract]
	public abstract class NamedDescribedUniqueData<TKey> : NamedUniqueData<TKey>, IDescribed
		where TKey : IEquatable<TKey>
	{
		protected NamedDescribedUniqueData() { }

		protected NamedDescribedUniqueData(TKey id, string name) : this(id, name, string.Empty) {}

		protected NamedDescribedUniqueData(TKey id, string name, string description) : base(id, name)
		{
			Description = description;
		}

		[DataMember]
		public string Description
		{
			get;
			set;
		}
	}
}
