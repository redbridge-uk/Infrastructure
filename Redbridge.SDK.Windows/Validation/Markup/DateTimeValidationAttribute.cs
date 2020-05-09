using System;
using Redbridge.Validation;

namespace Redbridge.Windows.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class DateTimeValidatorAttribute : PropertyValidatorAttribute
	{
		public DateTimeValidatorAttribute() : base(new DateTimeValidator()) { }
	}
}
