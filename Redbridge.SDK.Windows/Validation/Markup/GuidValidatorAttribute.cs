using System;

namespace Redbridge.Windows.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class GuidValidatorAttribute : PropertyValidatorAttribute
	{
	}
}
