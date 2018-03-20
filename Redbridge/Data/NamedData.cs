using System;
using System.Runtime.Serialization;

namespace Redbridge.SDK
{
	[DataContract]
	public abstract class NamedData<T> : NamedData
		where T :class, INamed
	{
		protected NamedData() { }

		protected NamedData(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			Name = item.Name;
		}
	}

	[DataContract]
	public abstract class NamedData : INamed
	{
		protected NamedData() { }

		protected NamedData(string name)
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
