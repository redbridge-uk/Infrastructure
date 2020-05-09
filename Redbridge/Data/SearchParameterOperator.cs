using System.Runtime.Serialization;

namespace Redbridge.Data
{

	[DataContract]
	public enum SearchParameterOperator
	{
		[EnumMember]
		Or = 0,
	}
	
}
