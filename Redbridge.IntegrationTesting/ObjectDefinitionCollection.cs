using System.Configuration;

namespace Redbridge.IntegrationTesting
{
	public class ObjectDefinitionCollection : ConfigurationElementCollection
	{
		public ObjectDefinition this[int index]
		{
			get => (ObjectDefinition)BaseGet(index);
            set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public void Add(ObjectDefinition objectDefinition)
		{
			BaseAdd(objectDefinition);
		}

		public void Clear()
		{
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ObjectDefinition();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ObjectDefinition)element).Type;
		}

		public void Remove(ObjectDefinition serviceConfig)
		{
			BaseRemove(serviceConfig.Type);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}

		public void Remove(string name)
		{
			BaseRemove(name);
		}
	}
}
