using Redbridge.Notifications;

namespace Redbridge.Web.Messaging
{

	public class SlackWebhookRequest : JsonWebRequestAction<SlackMessagePayloadData>
	{
		public SlackWebhookRequest(IHttpClientFactory clientFactory) : base("https://hooks.slack.com/services/{channelurl}", HttpVerb.Post, clientFactory)
		{
			AddParameter("channelurl");
		}

		public SlackWebhookRequest(string channelUrl, IHttpClientFactory clientFactory) : base("https://hooks.slack.com/services/{channelurl}", HttpVerb.Post, clientFactory)
		{
			AddParameter("channelurl");
			ChannelUrl = channelUrl;
		}

		public string ChannelUrl
		{
			get => (string)Parameters["channelurl"];
            set => Parameters["channelurl"] = value;
        }

		public override bool RequiresSignature => false;
	}
}
