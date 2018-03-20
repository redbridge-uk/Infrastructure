using System;
using System.Collections.Generic;
using Redbridge.SDK;
namespace Redbridge.Validation
{
	public class ObjectValidator<T> : IObjectValidator<T>
		where T : class
	{
		private readonly PropertyValidatorCollection _propertyValidators;

		public ObjectValidator()
		{
			var itemType = typeof(T);
			_propertyValidators = PropertyValidatorCollection.CollectFrom(itemType);
		}

		public ValidationResultCollection Validate(T item)
		{
			if (_propertyValidators != null)
			{
				return _propertyValidators.Validate(item);
			}
			else
				return new ValidationResultCollection();
		}

		ValidationResultCollection IObjectValidator.Validate(object obj)
		{
			T item = obj as T;
			return Validate(item);
		}
	}

	public class ObjectValidator : IObjectValidator
	{
		private static readonly Dictionary<Type, PropertyValidatorCollection> KnownTypes = new Dictionary<Type, PropertyValidatorCollection>();

		public ObjectValidator() : this(ValidationSettings.Default) { }

		private ObjectValidator(ValidationSettings settings)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			Settings = settings;
		}

		public ValidationSettings Settings
		{
			get;
			private set;
		}

		public ValidationResultCollection Validate(object item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item), "The object being validated is not permitted to be null.");

			Type itemType = item.GetType();
			PropertyValidatorCollection validatorCollection;

			// If we have not previously validated this particular type of object...
			if (!KnownTypes.TryGetValue(itemType, out validatorCollection))
			{
				// Collect necessary details in order to validate it...
				if (PropertyValidatorCollection.TryCollectValidationDetails(itemType, out validatorCollection))
				{
					// And store it for next time round
					KnownTypes.Add(itemType, validatorCollection);
				}
				else
				{
					// If throw on no validation then throw an exception.
					if (Settings.ThrowExceptionsForNonValidationTypes)
						throw new ValidationException(string.Format("The object of type {0} has no known validation requirements.", itemType));
					else
						return ValidationResultCollection.Empty;
				}
			}

			return validatorCollection.Validate(item);
		}
	}
}
