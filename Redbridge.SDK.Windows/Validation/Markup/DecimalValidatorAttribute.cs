using System;
namespace Redbridge.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class DecimalValidatorAttribute : PropertyValidatorAttribute
	{

		public DecimalValidatorAttribute() : this(decimal.MinValue, decimal.MaxValue) { }

		public DecimalValidatorAttribute(decimal maximum) : this(decimal.MinValue, maximum) { }

		public DecimalValidatorAttribute(decimal minimum, decimal maximum) : base(new DecimalValidator(minimum, maximum)) { }

		private DecimalValidator DecimalValidator => (DecimalValidator)Validator;

		public bool AllowZero
		{
			get { return DecimalValidator.AllowZero; }
			set { DecimalValidator.AllowZero = value; }
		}

		public decimal? Minimum
		{
			get { return DecimalValidator.Minimum; }
			set { DecimalValidator.Minimum = value; }
		}

		public decimal? Maximum
		{
			get { return DecimalValidator.Maximum; }
			set { DecimalValidator.Maximum = value; }
		}
	}
}
