using System.Runtime.Serialization;

namespace Redbridge.Notifications
{
[DataContract]
public enum NotificationTargetType
{
	[EnumMember]
	User = 0,
	[EnumMember]
	Group, }

}
