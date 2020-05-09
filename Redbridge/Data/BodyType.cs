using System.Runtime.Serialization;

namespace Redbridge.Data
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
