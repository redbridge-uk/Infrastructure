using System;
namespace Redbridge.Validation
{
	public class ValidationResult
	{
		public ValidationResult() : this(true) { }

		public ValidationResult(bool result)
		{
			Success = result;
		}

		public ValidationResult(bool result, string message, params object[] arguments) : this(result)
		{
			if (!string.IsNullOrEmpty(message))
				Message = string.Format(message, arguments);
		}

		public static ValidationResult Pass()
		{
			return new ValidationResult(true);
		}

		public static ValidationResult Pass(string fieldName, string message = null)
		{
			return new ValidationResult(true, message) { PropertyName = fieldName };
		}

		public static ValidationResult Fail(string fieldName, string message, params object[] arguments)
		{
			return new ValidationResult(false, message, arguments) { PropertyName = fieldName };
		}

		public static ValidationResult Fail(Exception exception)
		{
			return new ValidationResult(false, exception.Message);
		}

		public string Message
		{
			get;
			set;
		}

		public string PropertyName
		{
			get;
			set;
		}

		public bool Success
		{
			get;
			protected set;
		}
	}
}
