using System;
namespace Redbridge.Validation
{
	public interface IValidator<in TValue> : IValidator
	{
		ValidationResult ValidateValue(TValue value, string fieldName);
	}

	public interface IValidator
	{
		ValidationResult Validate(object value, string fieldName);

		bool AllowNullValues { get; set; }
	}
}
