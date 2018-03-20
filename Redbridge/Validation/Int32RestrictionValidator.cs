using System;
using System.Linq;

namespace Redbridge.Validation
{
	public class Int32RestrictionValidator : ValueRestrictionValidator<int>
	{
		protected override ValidationResult OnValidate(object value, string fieldName)
		{
			var intValue = Convert.ToInt32(value);
			var isValid = PermittedValues.Contains(intValue);

			if (isValid)
				return ValidationResult.Pass(fieldName);

			var validationMessage = string.Format("The value {0} is not valid for the field {1} as it does not conform to one of the following restriction values {2}", intValue, fieldName, string.Join(",", PermittedValues));
			return ValidationResult.Fail(fieldName, validationMessage);
		}
	}
}
