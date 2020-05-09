using System.IO;

namespace Redbridge.Validation
{
	public class FileExistsValidator : Validator<string>
	{
		protected override ValidationResult OnValidate(string value, string fieldName)
		{
			if (File.Exists(value))
				return ValidationResult.Pass(fieldName, $"The file '{value}' exists.");

			return ValidationResult.Fail(fieldName, $"Unable to locate or access file '{value}'.");
		}
	}
}
