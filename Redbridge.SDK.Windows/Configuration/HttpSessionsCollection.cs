using System;
using System.Configuration;

namespace Redbridge.Configuration
{
	public class HttpSessionsCollection : ConfigurationElementCollection
	{
		public SessionManagerSection this[int index]
		{
			get { return (SessionManagerSection)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public void Add(SessionManagerSection serviceConfig)
		{
			BaseAdd(serviceConfig);
		}

		public void Clear()
		{
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new SessionManagerSection();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SessionManagerSection)element).Name;
		}

		public void Remove(SessionManagerSection serviceConfig)
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
