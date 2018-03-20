using System;
using System.Linq;

namespace Redbridge.Validation
{
	public class StringRestrictionValidator : ValueRestrictionValidator<string>
	{
		public StringRestrictionValidator()
		{
			IsCaseSensitive = false;
		}

		public StringRestrictionValidator(params string[] restrictions) : base(restrictions)
		{
			IsCaseSensitive = false;
		}
		protected override ValidationResult OnValidate(object value, string fieldName)
		{
			var stringValue = value.ToString();

			if (stringValue.Equals(string.Empty) && AllowEmptyStrings)
				return ValidationResult.Pass(fieldName);

			var isValid = !IsCaseSensitive ? PermittedValues.Any(r => r.Equals(stringValue, StringComparison.OrdinalIgnoreCase)) : PermittedValues.Any(r => r.Equals(stringValue));

			if (isValid)
				return ValidationResult.Pass(fieldName);

			var validationMessage = string.Format("The value '{0}' is not valid for the field '{1}' as it does not conform to one of the following restriction values {2}", stringValue, fieldName, string.Join(",", PermittedValues));
			return ValidationResult.Fail(fieldName, validationMessage);
		}

		public bool AllowEmptyStrings { get; set; }

		public bool IsCaseSensitive { get; set; }
	}
}
