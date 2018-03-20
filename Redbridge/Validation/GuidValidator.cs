using System;

namespace Redbridge.Validation
{
	public class GuidValidator : Validator<Guid>
	{
		public bool AllowEmptyGuid { get; set; }

		public GuidValidator()
			: this(true)
		{
		}

		public GuidValidator(bool allowEmptyGuid)
		{
			AllowEmptyGuid = allowEmptyGuid;
		}

		protected override Guid OnConvert(object value)
		{
			Guid result;

			if (Guid.TryParse(value.ToString(), out result))
				return result;

			return Guid.Empty;
		}

		protected override ValidationResult OnValidate(Guid value, string fieldName)
		{
			if (AllowEmptyGuid == false && value == Guid.Empty)
				return ValidationResult.Fail(fieldName, "Please supply a non-empty guid");

			return ValidationResult.Pass(fieldName);
		}
	}
}
