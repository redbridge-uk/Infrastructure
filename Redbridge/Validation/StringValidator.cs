using System;
namespace Redbridge.Validation
{
	public class StringValidator : Validator<string>
	{
		public StringValidator() : this(0, Int32.MaxValue) { }
		public StringValidator(int maximumLength) : this(0, maximumLength) { }
		public StringValidator(int? minimumLength, int? maximumLength)
		{
			MinimumLength = minimumLength;
			MaximumLength = maximumLength;
		}

		public int? MinimumLength { get; set; }

		public int? MaximumLength { get; set; }

		protected override ValidationResult OnValidate(string value, string fieldName)
		{
			if (MinimumLength.HasValue && value.Length < MinimumLength.Value)
				return ValidationResult.Fail(fieldName, "The minimum length for field '{0}' is {1}, the supplied string ('{2}') contains only {3} characters", fieldName, MinimumLength, value, value.Length);
			else if (MaximumLength.HasValue && value.Length > MaximumLength)
				return ValidationResult.Fail(fieldName, "The maximum length for field '{0}' is {1}, the supplied string ('{2}') contains {3} characters", fieldName, MaximumLength, value, value.Length);
			else
				return ValidationResult.Pass(fieldName);
		}
	}
}
