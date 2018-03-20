using System;
using Redbridge.SDK;

namespace Redbridge.Validation
{
	public class Int32Validator : Validator<int>
	{
		public Int32Validator() : this(Int32.MinValue, Int32.MaxValue) { }

		public Int32Validator(int maximum) : this(Int32.MinValue, maximum) { }

		public Int32Validator(int minimum, int maximum)
		{
			Minimum = minimum;
			Maximum = maximum;
		}

		public int? Minimum { get; set; }

		public int? Maximum { get; set; }

		protected override int OnConvert(object value)
		{
			var valueString = value.ToString();
			int intValue;
			if (!int.TryParse(valueString, out intValue))
			{
				throw new ValidationException($"Unable to convert the value {valueString} into an integer. Validation fails.");
			}
			return intValue;
		}

		protected override ValidationResult OnValidate(int value, string fieldName)
		{
			if (Minimum.HasValue && value < Minimum)
				return ValidationResult.Fail(fieldName, "The minimum value for this integer is {0}, the supplied integer value is {1}", Minimum, value);

			if (Maximum.HasValue && value > Maximum)
				return ValidationResult.Fail(fieldName, "The maximum value for this integer is {0}, the supplied integer value is {1}", Maximum, value);

			return ValidationResult.Pass(fieldName);
		}
	}
}
