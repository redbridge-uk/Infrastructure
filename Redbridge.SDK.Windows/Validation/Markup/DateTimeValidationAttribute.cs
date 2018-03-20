using System;
namespace Redbridge.SDK.Windows
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class DateTimeValidatorAttribute : PropertyValidatorAttribute
	{
		public DateTimeValidatorAttribute() : base(new DateTimeValidator()) { }
	}
}
