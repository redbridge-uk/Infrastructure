using System;
using System.Configuration;

namespace Redbridge.IntegrationTesting
{
	public class ObjectDefinition : ConfigurationSection
	{
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get { return (string)base["type"]; }
			set { this["type"] = value; }
		}

		[ConfigurationProperty("columns", IsDefaultCollection = true)]
		[ConfigurationCollection(typeof(ColumnDefinitionCollection), AddItemName = "add", ClearItemsName = "clear",
			RemoveItemName = "remove")]
		public ColumnDefinitionCollection Columns
		{
			get
			{
				return (ColumnDefinitionCollection)base["columns"];
			}
		}
	}
}
