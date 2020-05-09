using System.Runtime.Serialization;

namespace Redbridge.Notifications
{
[DataContract]
public class SlackMessagePayloadData
{
	public SlackMessagePayloadData()
	{
		UnfurlLinks = false;
		UnfurlMedia = false;
	}

	[DataMember(Name = "text")]
	public string Text { get; set; }

	[DataMember(Name = "username")]
	public string Username { get; set; }

	[DataMember(Name = "icon_emoji")]
	public string IconEmoji { get; set; }

	[DataMember(Name = "icon_url")]
	public string IconUrl { get; set; }

	[DataMember(Name = "unfurl_links")]
	public bool UnfurlLinks { get; set; }

	[DataMember(Name = "unfurl_media")]
	public bool UnfurlMedia { get; set; } }
}
