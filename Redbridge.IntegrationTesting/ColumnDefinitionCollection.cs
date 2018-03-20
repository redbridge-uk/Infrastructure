using System;
using System.Configuration;

namespace Redbridge.IntegrationTesting
{
	public class ColumnDefinitionCollection : ConfigurationElementCollection
	{
		public ColumnDefinition this[int index]
		{
			get { return (ColumnDefinition)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public void Add(ColumnDefinition serviceConfig)
		{
			BaseAdd(serviceConfig);
		}

		public void Clear()
		{
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ColumnDefinition();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ColumnDefinition)element).Name;
		}

		public void Remove(ColumnDefinition serviceConfig)
		{
			BaseRemove(serviceConfig.Name);
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
