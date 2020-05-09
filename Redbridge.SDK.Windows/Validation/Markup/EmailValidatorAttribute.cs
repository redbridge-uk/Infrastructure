using System;

namespace Redbridge.Windows.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class EmailValidatorAttribute : PropertyValidatorAttribute
	{
		public EmailValidatorAttribute()
			: base(new EmailValidator())
		{
		}
	}
}
