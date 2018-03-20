//using System;
//using System.Globalization;
//using System.Linq;
//using System.Linq.Expressions;
//using Redbridge.Validation;

//namespace Redbridge.Data.Mapping
//{
//	public class BooleanFieldDefinition<TRecord> : FieldMap<TRecord, bool, StringRestrictionValidator>
//	{
//		private static readonly string[] TrueStrings = { "Y", "Yes", "True" };

//		public BooleanFieldDefinition(string fieldName) : base(fieldName, false)
//		{
//			TrueExportValue = bool.TrueString;
//			FalseExportValue = bool.FalseString;
//		}


//		public BooleanFieldDefinition(string fieldName, Expression<Func<TRecord, bool>> func) : base(fieldName, false, func)
//		{
//			TrueExportValue = bool.TrueString;
//			FalseExportValue = bool.FalseString;
//		}

//		protected override StringRestrictionValidator OnCreateValidator()
//		{
//			return new StringRestrictionValidator("Y", "N", "Yes", "No", "True", "False");
//		}

//		protected override bool OnConvert(object value)
//		{
//			var boolString = value?.ToString();

//			if (string.IsNullOrWhiteSpace(boolString))
//				return false;

//			return TrueStrings.Any(ts => ts.Equals(boolString, StringComparison.OrdinalIgnoreCase));
//		}

//		public string TrueExportValue { get; set; }
//		public string FalseExportValue { get; set; }

//		protected override string OnExport(bool value)
//		{
//			return value ? TrueExportValue : FalseExportValue;
//		}
//	}

//	public class NullableBooleanFieldDefinition<TRecord> : FieldMap<TRecord, bool?, StringRestrictionValidator>
//	{
//		private static readonly string[] TrueStrings = { "Y", "Yes", "True" };
//		private static readonly string[] FalseStrings = { "N", "No", "False" };

//		public NullableBooleanFieldDefinition(string fieldName) : base(fieldName, isMandatory: false)
//		{
//			TrueExportValue = bool.TrueString;
//			FalseExportValue = bool.FalseString;
//			NullExportValue = string.Empty;
//		}
//		public NullableBooleanFieldDefinition(string fieldName, Expression<Func<TRecord, bool?>> func) : base(fieldName, false, func)
//		{
//			TrueExportValue = bool.TrueString;
//			FalseExportValue = bool.FalseString;
//			NullExportValue = string.Empty;
//		}

//		protected override StringRestrictionValidator OnCreateValidator()
//		{
//			var validator = new StringRestrictionValidator("Y", "N", "Yes", "No", "True", "False")
//			{
//				AllowEmptyStrings = true,
//				AllowNullValues = !IsMandatory
//			};
//			return validator;
//		}

//		protected override bool? OnConvert(object value)
//		{
//			var boolString = value != null && !string.IsNullOrWhiteSpace(value.ToString()) ? value.ToString() : null;

//			if (string.IsNullOrWhiteSpace(boolString))
//				return null;

//			var isTrueString = TrueStrings.Any(ts => ts.Equals(boolString, StringComparison.OrdinalIgnoreCase));
//			if (isTrueString)
//				return true;

//			var isFalseString = FalseStrings.Any(ts => ts.Equals(boolString, StringComparison.OrdinalIgnoreCase));
//			if (isFalseString)
//				return false;

//			return null;
//		}

//		public string NullExportValue { get; set; }
//		public string TrueExportValue { get; set; }
//		public string FalseExportValue { get; set; }

//		protected override string OnExport(bool? value)
//		{
//			if (value.HasValue)
//				return value.Value ? TrueExportValue : FalseExportValue;

//			return NullExportValue;
//		}
//	}
//}
