using System.Configuration;
using System.Xml;

namespace Redbridge.Configuration
{
public class NotifierCollection : ConfigurationElementCollection
{

	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMap;

	protected override ConfigurationElement CreateNewElement()
	{
		return new NotifierConfigurationElement();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((NotifierConfigurationElement)element).Name;
	}

	public NotifierConfigurationElement this[int index]
	{
		get
		{
			return (NotifierConfigurationElement)BaseGet(index);
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

	protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
	{
        if (elementName == EmailNotifierConfigurationElement.ElementName)
		{
			Add(EmailNotifierConfigurationElement.FromXmlReader(reader));
			return true;
		}

		return base.OnDeserializeUnrecognizedElement(elementName, reader);
	}

	public new NotifierConfigurationElement this[string name] => (NotifierConfigurationElement)BaseGet(name);


	public int IndexOf(NotifierConfigurationElement notifier)
	{
		return BaseIndexOf(notifier);
	}

	public void Add(NotifierConfigurationElement notifier)
	{
		BaseAdd(notifier);

		// Your custom code goes here.

	}

	protected override void BaseAdd(ConfigurationElement element)
	{
		BaseAdd(element, false);

		// Your custom code goes here.

	}

	public void Remove(NotifierConfigurationElement notifier)
	{
		if (BaseIndexOf(notifier) >= 0)
		{
			BaseRemove(notifier.Name);
			// Your custom code goes here.
			System.Console.WriteLine("NotiferCollection: {0}", "Removed collection element!");
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
		System.Console.WriteLine("NotiferCollection: {0}", "Removed entire collection!");
	}
 }
}
