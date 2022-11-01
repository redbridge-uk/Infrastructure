using System;

namespace Redbridge.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class GuidValidatorAttribute : PropertyValidatorAttribute
	{
	}
}
