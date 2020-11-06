using NUnit.Framework;
using Redbridge.Web.Messaging;

namespace Redbridge.Tests
{
    [TestFixture]
    public class SlackWebhookRequestTests
    {
        [Test]
        public void Construct_SlackWebhookRequest_ExpectSuccess()
        {
            var webhook = new SlackWebhookRequest();
            Assert.IsNotNull(webhook);
            Assert.AreEqual(HttpVerb.Post, webhook.HttpVerb);
        }

        [Test]
        public void Construct_SlackWebhookRequestWithChannel_ExpectSuccess()
        {
            var webhook = new SlackWebhookRequest("channelUrl/1234");
            Assert.AreEqual("channelUrl/1234", webhook.ChannelUrl);
            Assert.AreEqual(HttpVerb.Post, webhook.HttpVerb);
        }
    }
}