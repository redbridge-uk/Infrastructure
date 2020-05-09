using System.Configuration;

namespace Redbridge.Configuration
{
	public class ServiceCollection : ConfigurationElementCollection
	{
		public ServiceSection this[int index]
		{
			get { return (ServiceSection)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public void Add(ServiceSection serviceConfig)
		{
			BaseAdd(serviceConfig);
		}

		public void Clear()
		{
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ServiceSection();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ServiceSection)element).Name;
		}

		public void Remove(ServiceSection serviceConfig)
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
