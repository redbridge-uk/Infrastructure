using System.Runtime.Serialization;

namespace Redbridge.Data
{
	[DataContract]
	public enum SortDirection
	{
		[EnumMember]
		Ascending = 0,
		[EnumMember]
		Descending = 1,
	}
	
}
