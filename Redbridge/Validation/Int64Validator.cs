using System;
namespace Redbridge.Validation
{
	public class Int64Validator : Validator<long>
	{
		public Int64Validator() : this(Int64.MinValue, Int64.MaxValue) { }

		public Int64Validator(long maximum) : this(Int64.MinValue, maximum) { }

		public Int64Validator(long minimum, long maximum)
		{
			Minimum = minimum;
			Maximum = maximum;
		}

		public long? Minimum { get; set; }

		public long? Maximum { get; set; }

		protected override ValidationResult OnValidate(long value, string fieldName)
		{
			if (Minimum.HasValue &&  value < Minimum)
				return ValidationResult.Fail(fieldName, "The minimum value for this long is {0}, the supplied long value is {1}", Minimum, value);
			else if (Maximum.HasValue && value > Maximum)
				return ValidationResult.Fail(fieldName, "The maximum value for this long is {0}, the supplied long value is {1}", Maximum, value);
			else
				return ValidationResult.Pass(fieldName);
		}
	}
}
