//using System;
//using System.Linq.Expressions;
//using Redbridge.Validation;

//namespace Redbridge.Data.Mapping
//{
//	public class DecimalFieldMap<TRecord> : FieldMap<TRecord, decimal, DecimalValidator>
//	{
//		public DecimalFieldMap(string fieldName, bool isMandatory = false) : base(fieldName, isMandatory: isMandatory) { }
//		public DecimalFieldMap(string fieldName, Expression<Func<TRecord, decimal>> func, bool isMandatory = false) : base(fieldName, isMandatory, func) { }

//		public string ExportFormat { get; set; }

//		protected override string OnExport(decimal value)
//		{
//			if (string.IsNullOrWhiteSpace(ExportFormat))
//				return base.OnExport(value);

//			return value.ToString(ExportFormat);
//		}

//		protected override decimal OnConvert(object value)
//		{
//			var decimalString = value?.ToString() ?? string.Empty;

//			decimal decimalValue;
//			if (decimal.TryParse(decimalString, out decimalValue))
//				return decimalValue;

//			return decimal.Zero;
//		}
//	}

//	public class NullableDecimalFieldDefinition<TRecord> : FieldMap<TRecord, decimal?, DecimalValidator>
//	{
//		public NullableDecimalFieldDefinition(string fieldName) : base(fieldName, false)
//		{
//			AllowZero = true;
//		}

//		public NullableDecimalFieldDefinition(string fieldName, Expression<Func<TRecord, decimal?>> func) : base(fieldName, false, func)
//		{
//			AllowZero = true;
//		}

//		public bool AllowZero
//		{
//			get { return Validator.AllowZero; }
//			set { Validator.AllowZero = value; }
//		}

//		public bool AllowNegativeValues
//		{
//			get { return Validator.AllowNegative; }
//			set { Validator.AllowNegative = value; }
//		}

//		public decimal? Minimum
//		{
//			get { return Validator.Minimum; }
//			set { Validator.Minimum = value; }
//		}

//		public decimal? Maximum
//		{
//			get { return Validator.Maximum; }
//			set { Validator.Maximum = value; }
//		}

//		public string ExportFormat { get; set; }

//		protected override string OnExport(decimal? value)
//		{
//			if (!value.HasValue) return string.Empty;

//			if (string.IsNullOrWhiteSpace(ExportFormat))
//				return base.OnExport(value);

//			return value.Value.ToString(ExportFormat);
//		}

//		protected override decimal? OnConvert(object value)
//		{
//			var decimalString = value != null && value != DBNull.Value && !string.IsNullOrWhiteSpace(value.ToString()) ? value.ToString() : null;

//			if (decimalString != null)
//				return decimal.Parse(decimalString);

//			return new decimal?();
//		}
//	}
//}
