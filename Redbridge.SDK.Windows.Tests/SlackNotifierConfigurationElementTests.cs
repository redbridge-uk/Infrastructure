using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Redbridge.Configuration;
using Redbridge.Notifications;
using Redbridge.Web.Messaging;

namespace Redbridge.Windows.Tests
{
    [TestFixture]
    public class SlackNotifierConfigurationElementTests
    {
        public class TestNotificationMessage : NotificationMessage
        {
            public TestNotificationMessage()
            {
            }
        }

        [Test]
        public void Construct_SlackNotifierConfigurationElementDefaultConstructor_ExpectSuccess()
        {
            var element = new SlackNotifierConfigurationElement();
            Assert.IsTrue(element.Enabled);
        }

        [Test]
        public async Task SlackNotifierConfigurationElementDefaultConstructor_ExpectSuccess()
        {
            var element = new SlackNotifierConfigurationElement {WebhookUrl = "http://localhost:1234/channel"};
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(f => f.Create()).Returns(new HttpClient());
            await element.NotifyAsync(new TestNotificationMessage(), factory.Object);
        }
    }
}
