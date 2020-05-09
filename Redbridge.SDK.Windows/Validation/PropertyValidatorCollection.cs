using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Redbridge.SDK;
using Redbridge.Validation;
using Redbridge.Windows.Validation.Markup;

namespace Redbridge.Windows.Validation
{
	public static class TypeExtensions
	{
		public static PropertyValidatorCollection CreatePropertyValidator(this Type type)
		{
			return PropertyValidatorCollection.CollectFrom(type);
		}
	}

	public class PropertyValidatorCollection
	{
		private readonly Dictionary<string, List<PropertyValidatorAttribute>> _validatorAttributes = new Dictionary<string, List<PropertyValidatorAttribute>>();

		public ValidationResultCollection Validate(object item)
		{
			if (item == null) throw new ArgumentNullException(nameof(item));
			var results = new ValidationResultCollection();

			foreach (var attributeSet in _validatorAttributes.Values)
			{
				foreach (var attribute in attributeSet)
				{
					if (attribute.HasCondition)
					{
						var conditionalProperty = item.GetType().GetProperty(attribute.Condition);
						if (conditionalProperty != null)
						{
							var conditionalValue = conditionalProperty.GetValue(item, null);
							if (conditionalValue is bool)
							{
								if ((bool)conditionalValue)
								{
									results.AddResult(attribute.Validate(item));
								}
							}
							else
								throw new ValidationException(string.Format("Unable to evaluate conditional property '{0}' from type '{1}' as the return type is not boolean.", attribute.HasCondition, item.GetType().Name));
						}
						else
							throw new ValidationException(string.Format("Unable to read conditional property '{0}' from type '{1}'", attribute.HasCondition, item.GetType().Name));
					}
					else
						results.AddResult(attribute.Validate(item));
				}
			}

			return results;
		}

		public IEnumerable<PropertyValidationResult> ValidatePropertyValue(string propertyName, object instance)
		{
			List<PropertyValidatorAttribute> validators;

			if (_validatorAttributes.TryGetValue(propertyName, out validators))
			{
				foreach (var attribute in validators)
					yield return attribute.Validate(instance);
			}
			else
				yield return PropertyValidationResult.Pass(null);
		}

		private void AddPropertyValidator(PropertyValidatorAttribute validator)
		{
			if (validator == null)
				throw new ArgumentNullException(nameof(validator), "The supplied attribute is null.");

			List<PropertyValidatorAttribute> validators;

			if (!_validatorAttributes.TryGetValue(validator.Property.Name, out validators))
			{
				validators = new List<PropertyValidatorAttribute>();
				_validatorAttributes.Add(validator.Property.Name, validators);
			}

			validators.Add(validator);
		}

		public static PropertyValidatorCollection CollectFrom(Type itemType)
		{
			PropertyValidatorCollection collection;
			TryCollectValidationDetails(itemType, out collection);
			return collection;
		}

		public static bool TryCollectValidationDetails<T>(out PropertyValidatorCollection typeAttributeCollection)
		{
			return TryCollectValidationDetails(typeof(T), out typeAttributeCollection);
		}

		public static bool TryCollectValidationDetails(Type itemType, out PropertyValidatorCollection typeAttributeCollection)
		{
			if (itemType == null)
				throw new ArgumentNullException(nameof(itemType));

			typeAttributeCollection = null;

			// Collect the validation interfaces from the type.
			var validationInterfaces = itemType.GetInterfaces()
											   .Where(t => t.GetCustomAttributes(typeof(ValidationContractAttribute), true).Count() == 1)
											   .Union(new[] { itemType });

			foreach (Type validationType in validationInterfaces)
			{
				PropertyInfo[] properties = validationType.GetProperties();

				// Walk through the properties for the object.
				foreach (PropertyInfo property in properties)
				{
					var attributes = property.GetCustomAttributes(typeof(PropertyValidatorAttribute), true).Cast<PropertyValidatorAttribute>();

					// If the property has an attribute associated then 
					foreach (PropertyValidatorAttribute attribute in attributes)
					{
						// Associate the attribute with the property info.
						attribute.Configure(property);

						if (typeAttributeCollection == null)
							typeAttributeCollection = new PropertyValidatorCollection();

						typeAttributeCollection.AddPropertyValidator(attribute);
					}
				}
			}

			return typeAttributeCollection != null;
		}
	}
}
