using System;
using System.Text.RegularExpressions;

namespace Redbridge.Validation
{
	public class StringPatternValidator : Validator<string>
	{
		public StringPatternValidator() { }

		public StringPatternValidator(string expression)
		{
			Pattern = expression;
		}

		public string Pattern { get; set; }

		protected override string OnConvert(object value)
		{
			return value.ToString();
		}

		protected override ValidationResult OnValidate(string value, string fieldName)
		{
			var matches = Regex.Match(value, Pattern);

			if (!matches.Success)
				return ValidationResult.Fail(fieldName, "The required pattern for field '{0}' is {1}", fieldName, Pattern);

			return ValidationResult.Pass(fieldName);
		}
	}
}
