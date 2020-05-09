using System;
using Redbridge.Validation;

namespace Redbridge.Windows.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class Int32ValidatorAttribute : PropertyValidatorAttribute
	{
		public Int32ValidatorAttribute() : this(int.MinValue, int.MaxValue) { }

		public Int32ValidatorAttribute(int maximum) : this(Int32.MinValue, maximum) { }

		public Int32ValidatorAttribute(int minimum, int maximum) : base(new Int32Validator(minimum, maximum)) { }

		private Int32Validator Int32Validator => (Int32Validator)Validator;

		public int? Minimum
		{
			get { return Int32Validator.Minimum; }
			set { Int32Validator.Minimum = value; }
		}

		public int? Maximum
		{
			get { return Int32Validator.Maximum; }
			set { Int32Validator.Maximum = value; }
		}
	}
}
