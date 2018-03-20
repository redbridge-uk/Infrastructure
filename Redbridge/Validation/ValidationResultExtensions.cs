using System;
using Redbridge.SDK;

namespace Redbridge.Validation
{
	public static class ValidationResultExtensions
	{
		public static void OnFailThrowValidationException(this ValidationResultCollection result)
		{
			if (result == null) throw new ArgumentNullException(nameof(result));

			if (!result.Success)
			{
				if (string.IsNullOrWhiteSpace(result.Message))
					throw new ValidationResultsException(result);

				throw new ValidationResultsException(result.Message, result);
			}
		}

		public static void OnFailThrowValidationException(this ValidationResult result)
		{
			if (result == null) throw new ArgumentNullException(nameof(result));

			if (!result.Success)
			{
				throw new ValidationException(result.Message);
			}
		}

		public static void OnFail(this ValidationResult result, Action<ValidationResult> action)
		{
			if (!result.Success)
				action(result);
		}
	}
}
