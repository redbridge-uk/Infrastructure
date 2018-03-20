using System;
using System.Runtime.Serialization;

namespace Redbridge.SDK
{
[DataContract]
public enum NotificationTargetType
{
	[EnumMember]
	User = 0,
	[EnumMember]
	Group, }

}
