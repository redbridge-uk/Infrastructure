using System.Configuration;
using System.Threading.Tasks;
using System.Xml;
using Redbridge.Data;
using Redbridge.Notifications;
using Redbridge.Web.Messaging;

namespace Redbridge.Configuration
{
public class SlackNotifierConfigurationElement : NotifierConfigurationElement
{
	public SlackNotifierConfigurationElement() { }
	public SlackNotifierConfigurationElement(string name) : base(name) { }
	public static string ElementName => "slack";

	public static NotifierConfigurationElement FromXmlReader(XmlReader reader)
	{
		var notifier = new SlackNotifierConfigurationElement();
		notifier.ReadSettings(reader);
		return notifier;
	}

	[ConfigurationProperty("webhookUrl", DefaultValue = "", IsRequired = true, IsKey = false)]
	public string WebhookUrl
	{
		get => (string)this["webhookUrl"];
        set => this["webhookUrl"] = value;
    }

	protected override void OnReadSettings(XmlReader reader)
	{
		base.OnReadSettings(reader);
		WebhookUrl = reader.GetAttribute("webhookUrl");
	}

	protected override async Task OnNotifyAsync(NotificationMessage message, IHttpClientFactory clientFactory)
	{
		var request = new SlackWebhookRequest(WebhookUrl, clientFactory);
		var emojiMetaData = message.Metadata["slack-emoji"];
		var payload = new SlackMessagePayloadData()
		{
			Text = message.CreateMessage(BodyType.PlainText)
		};

		if (emojiMetaData != null)
			payload.IconEmoji = emojiMetaData.Value.ToString();

		await request.ExecuteAsync(payload);
	} }
}
