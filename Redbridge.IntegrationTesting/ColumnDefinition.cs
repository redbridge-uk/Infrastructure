using System;
using System.Configuration;

namespace Redbridge.IntegrationTesting
{
	public class ColumnDefinition : ConfigurationSection
	{
		private readonly Random _randomizer;

		public ColumnDefinition()
		{
			_randomizer = new Random(DateTime.UtcNow.Millisecond);
		}

		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("type", IsRequired = false)]
		public Type Type
		{
			get { return (Type)this["type"]; }
			set { this["type"] = value; }
		}

		[ConfigurationProperty("values", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(ColumnDefinitionCollection), AddItemName = "add", ClearItemsName = "clear",
			RemoveItemName = "remove")]
		public ColumnValueCollection Values => (ColumnValueCollection)base["values"];

		public object GetRandomValue()
		{
			var totalItems = Values.Count;
			if (totalItems <= 0) return null;
			var randomIndex = _randomizer.Next(0, totalItems);
			var columnRandomValue = Values[randomIndex].Value;

			// TODO: establish property typing from the column type.
			return columnRandomValue;
		}
	}
}
