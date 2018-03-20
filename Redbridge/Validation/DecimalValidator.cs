using System;
using Redbridge.SDK;

namespace Redbridge.Validation
{
	public class DecimalValidator : Validator<decimal>
	{
		public DecimalValidator() : this(decimal.MinValue, decimal.MaxValue) { }

		public DecimalValidator(decimal maximum) : this(decimal.MinValue, maximum) { }

		public DecimalValidator(decimal minimum, decimal maximum)
		{
			AllowZero = true;
			Minimum = minimum;
			Maximum = maximum;
		}

		public DecimalValidator(decimal? minimum, decimal? maximum)
		{
			AllowZero = true;
			Minimum = minimum;
			Maximum = maximum;
		}

		protected override decimal OnConvert(object value)
		{
			var decimalString = value.ToString();
			decimal decimalValue;

			if (decimal.TryParse(decimalString, out decimalValue))
			{
				return decimalValue;
			}

			throw new ValidationException(string.Format("The value {0} cannot be converted to a decimal value. Validation fails.", decimalString));
		}

		public bool AllowZero { get; set; }

		public decimal? Minimum { get; set; }

		public decimal? Maximum { get; set; }

		public bool AllowNegative { get; set; }

		protected override ValidationResult OnValidate(decimal value, string fieldName)
		{
			if (!AllowNegative && value < decimal.Zero)
				return ValidationResult.Fail(fieldName, "Negative values are not permitted for the field {0}", fieldName);

			if (Minimum.HasValue && value < Minimum)
				return ValidationResult.Fail(fieldName, "The minimum value for this decimal is {0}, the supplied decimal value is {1}", Minimum, value);

			if (Maximum.HasValue && value > Maximum)
				return ValidationResult.Fail(fieldName, "The maximum value for this decimal is {0}, the supplied decimal value is {1}", Maximum, value);

			if (value == decimal.Zero && !AllowZero)
				return ValidationResult.Fail(fieldName, "Zero is not supported for the decimal.");

			return ValidationResult.Pass(fieldName);
		}
	}
}
