using System.Runtime.Serialization;

namespace Redbridge.Data
{
	[DataContract]
	public class PageSort
	{

		public PageSort() { }

		public PageSort(int priority, SortDirection sortDirection, string propertyName)
		{
			Priority = priority;
			SortDirection = sortDirection;
			Property = propertyName;
		}

		[DataMember]
		public int Priority
		{
			get;
			set;
		}

		[DataMember]
		public SortDirection SortDirection
		{
			get;
			set;
		}

		[DataMember]
		public string Property
		{
			get;
			set;
		}

		public string Symbol => SortDirection == SortDirection.Ascending ? "+" : "-";
	}
	
}
