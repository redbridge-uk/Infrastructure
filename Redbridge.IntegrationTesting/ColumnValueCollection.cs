using System;
using System.Configuration;

namespace Redbridge.IntegrationTesting
{
	public class ColumnValueCollection : ConfigurationElementCollection
	{
		public ColumnValue this[int index]
		{
			get { return (ColumnValue)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public void Add(ColumnValue serviceConfig)
		{
			BaseAdd(serviceConfig);
		}

		public void Clear()
		{
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ColumnValue();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ColumnValue)element).Value;
		}

		public void Remove(ColumnValue serviceConfig)
		{
			BaseRemove(serviceConfig.Value);
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
