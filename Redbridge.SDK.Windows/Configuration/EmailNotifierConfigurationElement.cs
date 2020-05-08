using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Xml;
using Redbridge.SDK;

namespace Redbridge.Windows.Configuration
{
    public class EmailNotifierConfigurationElement : NotifierConfigurationElement
    {
        public EmailNotifierConfigurationElement() { }
        public EmailNotifierConfigurationElement(string name) : base(name) { }
        public static string ElementName => "email";

        public static EmailNotifierConfigurationElement FromXmlReader(XmlReader reader)
        {
            var notifier = new EmailNotifierConfigurationElement();
            notifier.ReadSettings(reader);
            return notifier;
        }

        [ConfigurationProperty("sender", DefaultValue = "", IsRequired = true, IsKey = false)]
        public string Sender
        {
            get { return (string)this["sender"]; }
            set { this["sender"] = value; }
        }

        [ConfigurationProperty("senderDisplayName", DefaultValue = "Easilog Service", IsRequired = true, IsKey = false)]
        public string SenderDisplayName
        {
            get { return (string)this["senderDisplayName"]; }
            set { this["senderDisplayName"] = value; }
        }

        [ConfigurationProperty("target", DefaultValue = "", IsRequired = true, IsKey = false)]
        public string Target
        {
            get { return (string)this["target"]; }
            set { this["target"] = value; }
        }

        protected override void OnReadSettings(XmlReader reader)
        {
            base.OnReadSettings(reader);
            Sender = reader.GetAttribute("sender");
            Target = reader.GetAttribute("target");
            SenderDisplayName = reader.GetAttribute("senderDisplayName");
        }

        protected override async Task OnNotifyAsync(NotificationMessage message)
        {
            var smptClient = new SmtpClient();
            var senderAddress = new MailAddress(Sender, SenderDisplayName);
            var mailMessage = new MailMessage()
            {
                Sender = senderAddress,
                From = senderAddress,
                Subject = message.Subject ?? "No subject",
                Body = message.SupportsHtmlBody ? message.CreateMessage() : message.CreateMessage(BodyType.PlainText),
                IsBodyHtml = message.SupportsHtmlBody,
            };

            mailMessage.To.Add(new MailAddress(Target));
            await smptClient.SendMailAsync(mailMessage);
        }
    }
}
