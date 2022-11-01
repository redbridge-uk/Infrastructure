using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Redbridge.Notifications;
using Redbridge.Web.Messaging;

namespace Redbridge.Configuration
{
public class NotifierConfigurationElement : ConfigurationElement
{
	public NotifierConfigurationElement(string name)
	{
		Name = name;
	}

	public NotifierConfigurationElement() { }

	[ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
	public string Name
	{
		get => (string)this["name"];
        set => this["name"] = value;
    }

	[ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false, IsKey = false)]
	public bool Enabled
	{
		get => (bool)this["enabled"];
        set => this["enabled"] = value;
    }

	[ConfigurationProperty("filters", IsDefaultCollection = true)]
	[ConfigurationCollection(typeof(NotificationFilterCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
	public NotificationFilterCollection Filters
	{
		get
		{
			var filterCollection = (NotificationFilterCollection)base["filters"];
			return filterCollection;
		}
		set
		{
			var filterCollection = value;
			base["filters"] = filterCollection;
		}
	}

	public Task NotifyAsync(NotificationMessage message, IHttpClientFactory clientFactory)
    {
        if (Filters != null && Filters.Count > 0)
		{
			if (Filters.IsIncluded(message))
				return OnNotifyAsync(message, clientFactory);

			return Task.FromResult(false);
		}

        return OnNotifyAsync(message, clientFactory);
    }

	protected virtual Task OnNotifyAsync(NotificationMessage message, IHttpClientFactory clientFactory)
	{
		return Task.FromResult(true);
	}

	public void ReadSettings(XmlReader reader)
	{
		Name = reader.GetAttribute("name");
		Enabled = bool.Parse(reader.GetAttribute("enabled") ?? "true");

		OnReadSettings(reader);

		if (reader.ReadToDescendant("filters"))
		{
			Filters.ReadSettings(reader);
		}

		reader.ReadEndElement();
	}

	protected virtual void OnReadSettings(XmlReader reader) { } }
}
