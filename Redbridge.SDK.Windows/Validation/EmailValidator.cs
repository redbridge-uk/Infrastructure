using System;
using System.Net.Mail;
using Redbridge.Validation;

namespace Redbridge.Validation
{
	public class EmailValidator : Validator<string>
	{
		protected override ValidationResult OnValidate(string value, string fieldName)
		{
			try
			{
#pragma warning disable RECS0026 // Possible unassigned object created by 'new'
				new MailAddress(value);
#pragma warning restore RECS0026 // Possible unassigned object created by 'new'
				return ValidationResult.Pass(fieldName);
			}
			catch (ArgumentNullException ex)
			{
				return ValidationResult.Fail(fieldName, ex.Message, value);
			}
			catch (ArgumentException ex)
			{
				return ValidationResult.Fail(fieldName, ex.Message, value);
			}
			catch (FormatException ex)
			{
				return ValidationResult.Fail(fieldName, ex.Message, value);
			}
		}
	}
}
