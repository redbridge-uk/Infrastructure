﻿using System;
using System.Configuration;

namespace Redbridge.IntegrationTesting
{
	public class ObjectDefinitionsSection : ConfigurationSection
	{
		public const string SectionName = "objectDefinitions";

		[ConfigurationProperty("types", IsDefaultCollection = true)]
		[ConfigurationCollection(typeof(ObjectDefinitionCollection), AddItemName = "add", ClearItemsName = "clear",
			RemoveItemName = "remove")]
		public ObjectDefinitionCollection Types => (ObjectDefinitionCollection)base["types"];

        [ConfigurationProperty("columns", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(ColumnDefinitionCollection), AddItemName = "add", ClearItemsName = "clear",
			RemoveItemName = "remove")]
		public ColumnDefinitionCollection Columns => (ColumnDefinitionCollection)base["columns"];
    }
}
