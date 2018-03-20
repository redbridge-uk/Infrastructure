using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Xml;
using Redbridge.SDK;

namespace Redbridge.Configuration
{
public class TrelloNotifierConfigurationElement : NotifierConfigurationElement
{
	public TrelloNotifierConfigurationElement() { }
	public TrelloNotifierConfigurationElement(string name) : base(name) { }
	public static string ElementName => "trello";

	public static NotifierConfigurationElement FromXmlReader(XmlReader reader)
	{
		var notifier = new SlackNotifierConfigurationElement();
		notifier.ReadSettings(reader);
		return notifier;
	}

	[ConfigurationProperty("webhookUrl", DefaultValue = "", IsRequired = true, IsKey = false)]
	public string WebhookUrl
	{
		get { return (string)this["webhookUrl"]; }
		set { this["webhookUrl"] = value; }
	}

	protected override void OnReadSettings(XmlReader reader)
	{
		base.OnReadSettings(reader);
		WebhookUrl = reader.GetAttribute("webhookUrl");
	}

	protected override async Task OnNotifyAsync(NotificationMessage message)
	{
		var request = new SlackWebhookRequest(WebhookUrl);
		var emojiMetaData = message.Metadata["slack-emoji"];
		var payload = new SlackMessagePayloadData()
		{
			Text = message.CreateMessage()
		};

		if (emojiMetaData != null)
			payload.IconEmoji = emojiMetaData.Value.ToString();

		await request.ExecuteAsync(payload);
	}
}
}
