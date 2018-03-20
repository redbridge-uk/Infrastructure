using System;
using System.Runtime.Serialization;

namespace Redbridge.SDK
{
	public class SortRequest
	{

		public SortRequest() { }

		public SortRequest(string property, SortDirection sort = SortDirection.Ascending)
		{
			Property = property;
			Direction = sort;
		}

		[DataMember]
		public string Property
		{
			get;
			set;
		}

		[DataMember]
		public SortDirection Direction
		{
			get;
			set;
		}
	}
}
