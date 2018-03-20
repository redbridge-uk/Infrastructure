using System;
using System.Collections;
using System.Configuration;
using System.Linq;

namespace Redbridge.IntegrationTesting
{
	public class ObjectFactory
	{
		private ObjectDefinitionsSection _definitions;

		public ObjectFactory()
		{
			var section = ConfigurationManager.GetSection(ObjectDefinitionsSection.SectionName) as ObjectDefinitionsSection;
			Setup(section ?? new ObjectDefinitionsSection());
		}

		public ObjectFactory(ObjectDefinitionsSection definitions)
		{
			Setup(definitions);
		}

		private void Setup(ObjectDefinitionsSection definitions)
		{
			if (definitions == null) throw new ArgumentNullException(nameof(definitions));
			_definitions = definitions;
		}

		public object GetColumnValue(string name, ObjectDefinition definition)
		{
			object value = null;

			if (definition != null)
				value = GetRandomValue(name, definition.Columns);

			return value ?? (GetRandomValue(name, _definitions.Columns));
		}

		private object GetRandomValue(string name, ColumnDefinitionCollection columnDefinitions)
		{
			var outerColumn = columnDefinitions.Cast<ColumnDefinition>().SingleOrDefault(cd => cd.Name == name);

			return outerColumn?.GetRandomValue();
		}

		public TMessage Create<TMessage>() where TMessage : new()
		{
			return (TMessage)Create(typeof(TMessage));
		}

		private object Create(Type objectType)
		{
			if (objectType.IsInterface)
				return null;
			if (typeof(IEnumerable).IsAssignableFrom(objectType))
				return null;

			var objectDefinition = GetDefinitionForType(objectType);

			var instance = Activator.CreateInstance(objectType);

			foreach (var property in objectType.GetProperties().Where(x => x.SetMethod != null))
			{
				if (property.PropertyType == typeof(string))
				{
					var randomValue = GetColumnValue(property.Name, objectDefinition);
					property.SetValue(instance, randomValue);
				}
				else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
				{
					var randomValue = GetColumnValue(property.Name, objectDefinition);
					property.SetValue(instance, randomValue);
				}
				else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
				{

				}
				else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
				{

				}
				else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
				{

				}
				else
				{
					var complexType = Create(property.PropertyType);
					property.SetValue(instance, complexType);
				}
			}

			return instance;
		}

		private ObjectDefinition GetDefinitionForType(Type objectType)
		{
			var objectDefinitions = _definitions.Types.Cast<ObjectDefinition>();
			var objectDefinition = objectDefinitions.SingleOrDefault(ot => Type.GetType(ot.Type) == objectType);
			return objectDefinition;
		}
	}
}
