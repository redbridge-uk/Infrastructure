using System.Configuration;

namespace Redbridge.Configuration
{
	public class HttpSessionsSection : ConfigurationSection
	{
		public const string SectionName = "httpSessionsSection";

		[ConfigurationProperty("defaultSession", IsRequired = true)]
		public string DefaultSessionName
		{
			get { return (string)this["defaultSession"]; }
			set { this["defaultSession"] = value; }
		}

		[ConfigurationProperty("sessions", IsDefaultCollection = true)]
		[ConfigurationCollection(typeof(HttpSessionsCollection), AddItemName = "add", ClearItemsName = "clear",
			RemoveItemName = "remove")]
		public HttpSessionsCollection Sessions => (HttpSessionsCollection)base["sessions"];
	}
}
