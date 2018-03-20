using System.Runtime.Serialization;

namespace Redbridge.SDK
{
	[DataContract]
	public enum WhereOperation
	{
		[EnumMember]
		Equal = 0,

		[EnumMember]
		NotEqual = 1,

		[EnumMember]
		Contains = 2,

		[EnumMember]
		StartsWith = 3,

		[EnumMember]
		EndsWith = 4,
	}
}
