using System.Configuration;

namespace Redbridge.Windows.Configuration
{
	public class ServiceSection : ConfigurationSection
	{
		public const string SectionName = "serviceSection";

		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("services", IsDefaultCollection = true)]
		[ConfigurationCollection(typeof(ServiceCollection), AddItemName = "add", ClearItemsName = "clear",
			RemoveItemName = "remove")]
		public ServiceCollection Services => (ServiceCollection)base["services"];
	}
}
