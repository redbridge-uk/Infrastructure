using System.Runtime.Serialization;

namespace Redbridge
{
	public enum Gender
	{
		[EnumMember]
		Unspecified = 0,
		[EnumMember]
		Male = 1,
		[EnumMember]
		Female = 2,
	}
}
