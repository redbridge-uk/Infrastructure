using System;
using System.Configuration;
using Redbridge.Notifications;

namespace Redbridge.Configuration
{
	public class NotificationFilterConfigurationElement : ConfigurationElement
	{
		public NotificationFilterConfigurationElement(string name)
		{
			Event = name;
		}

		public NotificationFilterConfigurationElement() { }

		[ConfigurationProperty("event", DefaultValue = "", IsRequired = true, IsKey = true)]
		public string Event
		{
			get => (string)this["event"];
            set => this["event"] = value;
        }

		public bool Includes(NotificationMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));
			return Event.Equals(message.NotificationId, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
