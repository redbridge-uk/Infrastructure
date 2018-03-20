using System;
using System.Runtime.Serialization;

namespace Redbridge
{
	[DataContract]
	public enum BodyType
	{
		[EnumMember]
		PlainText = 0,
		[EnumMember]
		Html = 1,
	}
}
