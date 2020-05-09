using Redbridge.Notifications;

namespace Redbridge.Web.Messaging
{

	public class SlackWebhookRequest : JsonWebRequestAction<SlackMessagePayloadData>
	{
		public SlackWebhookRequest() : base("https://hooks.slack.com/services/{channelurl}", HttpVerb.Post)
		{
			AddParameter("channelurl");
		}

		public SlackWebhookRequest(string channelUrl) : base("https://hooks.slack.com/services/{channelurl}", HttpVerb.Post)
		{
			AddParameter("channelurl");
			ChannelUrl = channelUrl;
		}

		public string ChannelUrl
		{
			get { return (string)Parameters["channelurl"]; }
			set { Parameters["channelurl"] = value; }
		}

		public override bool RequiresSignature => false;
	}
}
