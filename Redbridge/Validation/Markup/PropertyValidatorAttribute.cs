using System;
using System.Reflection;
using Redbridge.Exceptions;

namespace Redbridge.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property)]
	public abstract class PropertyValidatorAttribute : Attribute
	{
		protected PropertyValidatorAttribute() { }

		protected PropertyValidatorAttribute(IValidator validator)
		{
			if (validator == null) throw new ArgumentNullException(nameof(validator), "The validator instance is not permitted to be null.");
			Validator = validator;
			IsRequired = false;
		}

		public bool IsRequired
		{
			get { return !Validator.AllowNullValues; }
			set { Validator.AllowNullValues = !value; }
		}

		public string ErrorText
		{
			get;
			set;
		}

		public bool HasCondition => !string.IsNullOrWhiteSpace(Condition);

		public string Condition
		{
			get;
			set;
		}

		public string ErrorResource
		{
			get;
			set;
		}

		protected IValidator Validator
		{
			get;
		}

		public PropertyInfo Property
		{
			get;
			private set;
		}

		internal void Configure(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
				throw new ValidationException("The property info instance is not permitted to be null.");

			Property = propertyInfo;
			OnConfigure(propertyInfo);
		}

		protected virtual void OnConfigure(PropertyInfo propertyInfo) { }

		public virtual PropertyValidationResult Validate(object item)
		{
			// At this point the property could actually be from an interface type.
			object fieldValue = Property.GetValue(item, null);

			if (string.IsNullOrWhiteSpace(ErrorText))
				return new PropertyValidationResult(Property, Validator.Validate(fieldValue, Property.Name));
			else
			{
				var result = Validator.Validate(fieldValue, Property.Name);
				return new PropertyValidationResult(Property, new ValidationResult(result.Success, ErrorText) { PropertyName = Property.Name });
			}
		}
	}
}
