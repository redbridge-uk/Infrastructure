using System.Configuration;

namespace Redbridge.IntegrationTesting
{
	public class ColumnValue : ConfigurationSection
	{
		[ConfigurationProperty("value", IsRequired = true)]
		public string Value
		{
			get => (string)this["value"];
            set => this["value"] = value;
        }
	}
}
