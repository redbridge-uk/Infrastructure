using System;
using Redbridge.Validation;

namespace Redbridge.Windows.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class StringValidatorAttribute : PropertyValidatorAttribute
	{
		public StringValidatorAttribute() : this(0, int.MaxValue) { }

		public StringValidatorAttribute(int maximumLength) : this(0, maximumLength) { }

		public StringValidatorAttribute(int minimumLength, int maximumLength) : base(new StringValidator(minimumLength, maximumLength)) { }

		private StringValidator StringValidator => (StringValidator)Validator;

		public int? MinimumLength
		{
			get { return StringValidator.MinimumLength; }
			set { StringValidator.MinimumLength = value; }
		}

		public int? MaximumLength
		{
			get { return StringValidator.MaximumLength; }
			set { StringValidator.MaximumLength = value; }
		}
	}
}
