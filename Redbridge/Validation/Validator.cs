using System;

namespace Redbridge.Validation
{
	public abstract class Validator : IValidator
	{
		protected Validator() : this(false) { }

		protected Validator(bool allowNulls)
		{
			AllowNullValues = allowNulls;
		}

		public ValidationResult Validate(object value)
		{
			return Validate(value, string.Empty);
		}

		public ValidationResult Validate(object value, string fieldName)
		{
			if (!AllowNullValues && value == null)
				return ValidationResult.Fail(fieldName, string.Format("The supplied value for field '{0}' is null, and null values are not permitted.", fieldName));

			return value != null ? OnValidate(value, fieldName) : ValidationResult.Pass();
		}

		public bool AllowNullValues
		{
			get;
			set;
		}

		protected abstract ValidationResult OnValidate(object value, string fieldName);
	}

	public abstract class Validator<T> : Validator
	{
		protected Validator() { }

		protected Validator(bool allowNulls) : base(allowNulls) { }

		protected override ValidationResult OnValidate(object value, string fieldName)
		{
            try
            {
                var castValue = OnConvert(value);
                return OnValidate(castValue, fieldName);
            }
            catch (Exception)
            {
                return ValidationResult.Fail($"Invalid value '{value}' or unexpected content.", fieldName);
            }
		}

		protected virtual T OnConvert(object value)
		{
			return (T)value;
		}

		protected abstract ValidationResult OnValidate(T value, string fieldName);
	}
}
