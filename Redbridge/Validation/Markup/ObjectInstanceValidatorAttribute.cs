using System;

namespace Redbridge.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class ObjectInstanceValidatorAttribute : PropertyValidatorAttribute
	{
		public ObjectInstanceValidatorAttribute(bool allowNulls = false)
			: base(new ObjectInstanceValidator<object>(allowNulls))
		{
			this.IsRequired = !allowNulls;
		}
	}
}
