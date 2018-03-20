using System.IO;
using Redbridge.Validation;

namespace Redbridge.Validation
{
	public class DirectoryExistsValidator : Validator<string>
	{
		protected override ValidationResult OnValidate(string value, string fieldName)
		{
			if (Directory.Exists(value))
				return ValidationResult.Pass($"The directory '{value}' exists.");
			else
				return ValidationResult.Fail(fieldName, $"Unable to locate or access directory '{value}'.");
		}
	}
}
