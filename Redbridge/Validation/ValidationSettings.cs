namespace Redbridge.Validation
{
	public class ValidationSettings
	{
		public static ValidationSettings Default => new ValidationSettings() { ThrowExceptionsForNonValidationTypes = false };

		public bool ThrowExceptionsForNonValidationTypes
		{
			get;
			set;
		}
	}
}
