using System;
using System.Runtime.Serialization;

namespace Redbridge.Data
{
	[DataContract]
	public class UniqueData<T> : IUnique<T> where T : IEquatable<T>
	{
		public UniqueData() { }
		public UniqueData(T id) 
		{
			Id = id;
		}

		[DataMember]
		public T Id { get; set; }
	}
}
