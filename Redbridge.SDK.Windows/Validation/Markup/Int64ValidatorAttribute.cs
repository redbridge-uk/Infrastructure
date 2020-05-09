using System;
using Redbridge.Validation;

namespace Redbridge.Windows.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class Int64ValidatorAttribute : PropertyValidatorAttribute
	{
		public Int64ValidatorAttribute() : this(long.MinValue, long.MaxValue) { }

		public Int64ValidatorAttribute(long maximum) : this(Int64.MinValue, maximum) { }

		public Int64ValidatorAttribute(long minimum, long maximum) : base(new Int64Validator(minimum, maximum)) { }

		private Int64Validator Int64Validator
		{
			get { return (Int64Validator)Validator; }
		}

		public long? Minimum
		{
			get { return Int64Validator.Minimum; }
			set { Int64Validator.Minimum = value; }
		}

		public long? Maximum
		{
			get { return Int64Validator.Maximum; }
			set { Int64Validator.Maximum = value; }
		}
	}
}
