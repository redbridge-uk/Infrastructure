using System;

namespace Redbridge.Data
{
	public abstract class DescribedUniqueData<TKey> : UniqueData<TKey>, IDescribed
		where TKey : IEquatable<TKey>
	{
		protected DescribedUniqueData(TKey id, string description)
			: base(id)
		{
			Description = description;
		}

		public string Description
		{
			get;
			set;
		}
	}
}
