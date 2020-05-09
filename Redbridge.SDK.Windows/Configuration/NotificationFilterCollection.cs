using System;
using System.Configuration;
using System.Linq;
using System.Xml;
using Redbridge.Notifications;

namespace Redbridge.Configuration
{
	public class NotificationFilterCollection : ConfigurationElementCollection
	{
		public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMap;

		protected override ConfigurationElement CreateNewElement()
		{
			return new NotificationFilterConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((NotificationFilterConfigurationElement)element).Event;
		}

		public NotificationFilterConfigurationElement this[int index]
		{
			get
			{
				return (NotificationFilterConfigurationElement)BaseGet(index);
			}
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		new public NotificationFilterConfigurationElement this[string name] => (NotificationFilterConfigurationElement)BaseGet(name);


		public int IndexOf(NotificationFilterConfigurationElement notifier)
		{
			return BaseIndexOf(notifier);
		}

		public void Add(NotificationFilterConfigurationElement notifier)
		{
			BaseAdd(notifier);

			// Your custom code goes here.

		}

		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);

			// Your custom code goes here.

		}

		public void Remove(NotificationFilterConfigurationElement notifier)
		{
			if (BaseIndexOf(notifier) >= 0)
			{
				BaseRemove(notifier.Event);
				// Your custom code goes here.
				System.Console.WriteLine("NotiferFilterCollection: {0}", "Removed collection element!");
			}
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);

			// Your custom code goes here.

		}

		public void Remove(string name)
		{
			BaseRemove(name);

			// Your custom code goes here.

		}

		public void Clear()
		{
			BaseClear();

			// Your custom code goes here.
			System.Console.WriteLine("NotificationFilterCollection: {0}", "Removed entire collection!");
		}

		public void ReadSettings(XmlReader reader)
		{
			if (reader.ReadToDescendant("add"))
			{
				Add(new NotificationFilterConfigurationElement(reader.GetAttribute("event")));
			}

			while (reader.ReadToNextSibling("add"))
			{
				Add(new NotificationFilterConfigurationElement(reader.GetAttribute("event")));
			}

			reader.ReadEndElement();
		}

		public bool IsIncluded(NotificationMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));
			return this.Cast<NotificationFilterConfigurationElement>().Any(filter => filter.Includes(message));
		}
	}
}
