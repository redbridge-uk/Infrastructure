using System;
using System.Runtime.Serialization;

namespace Redbridge.SDK
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
