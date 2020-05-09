using System;

namespace Redbridge.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class DateTimeValidatorAttribute : PropertyValidatorAttribute
	{
		public DateTimeValidatorAttribute() : base(new DateTimeValidator()) { }
	}
}
