using System;
using System.Globalization;
using Redbridge.SDK;

namespace Redbridge.Validation
{
    public class DateTimeValidator : Validator<DateTime>
    {
        public DateTimeValidator () {}

        public DateTimeValidator(DateTime? minimum, DateTime? maximum) 
        {
            MinimumDate = minimum;
            MaximumDate = maximum;
        }

        protected override ValidationResult OnValidate(DateTime value, string fieldName)
        {
            if (MinimumDate.HasValue && value < MinimumDate.Value)
                return ValidationResult.Fail(fieldName, $"The date supplied '{value}' must be on or after {MinimumDate.Value}.");

			if (MaximumDate.HasValue && value > MaximumDate.Value)
				return ValidationResult.Fail(fieldName, $"The date supplied '{value}' must be on or before {MaximumDate.Value}.");

			return ValidationResult.Pass();
        }

        protected override DateTime OnConvert(object value)
        {
            if (value is DateTime)
                return (DateTime)value;

            DateTime dateTime;

            if (DateTime.TryParse(value.ToString(), out dateTime))
                return dateTime;

            throw new ValidationException("Unable to convert the value supplied to a valid date time.");
        }

        public DateTime? MaximumDate { get; set; }
        public DateTime? MinimumDate { get; set; }
    }

	public class DateStringValidator : Validator<string>
	{
		protected override string OnConvert(object value)
		{
			return value.ToString();
		}

		protected override ValidationResult OnValidate(string value, string fieldName)
		{
			DateTime parsedDate;
			if (!DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
				return ValidationResult.Fail(fieldName, "The field '{0}' with value '{1}' does not parse as a valid date", fieldName, value);

			return ValidationResult.Pass(fieldName);
		}
	}
}
