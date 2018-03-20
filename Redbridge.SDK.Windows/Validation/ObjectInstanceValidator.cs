using System;
using Redbridge.Validation;

namespace Redbridge.Validation
{
	public class ObjectInstanceValidator<T> : Validator<T>
	{
		public ObjectInstanceValidator(bool allowNull = false)
		{
			AllowNullValues = allowNull;
		}

		protected override ValidationResult OnValidate(T value, string fieldName)
		{
			if (value == null)
			{
				if (!AllowNullValues)
				{
					return ValidationResult.Fail(fieldName, "Please supply a value.");
				}
			}

			return ValidationResult.Pass();
		}
	}
}
