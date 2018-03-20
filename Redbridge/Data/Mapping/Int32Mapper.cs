//using System;
//using System.Linq.Expressions;
//using Redbridge.Validation;

//namespace Redbridge.Data.Mapping
//{
//	public class Int32FieldMap<TRecord> : FieldMap<TRecord, int, Int32Validator>
//	{
//		public Int32FieldMap(string fieldName, Expression<Func<TRecord, int>> func, bool isMandatory = false) : base(fieldName, isMandatory, func) { }

//		public Int32FieldMap(string fieldName, bool isMandatory = false) : base(fieldName, isMandatory) { }

//		public int? Minimum
//		{
//			get { return Validator.Minimum; }
//			set { Validator.Minimum = value; }
//		}

//		public int? Maximum
//		{
//			get { return Validator.Maximum; }
//			set { Validator.Maximum = value; }
//		}

//		protected override int OnConvert(object value)
//		{
//			var integerString = value?.ToString() ?? string.Empty;
//			int result;

//			if (int.TryParse(integerString, out result))
//			{
//				return result;
//			}

//			return 0;
//		}
//	}

//	public class NullableInt32FieldMap<TRecord> : FieldMap<TRecord, int?, Int32Validator>
//	{
//		public NullableInt32FieldMap(string columnName) : base(columnName, isMandatory: false) { }

//		protected override int? OnConvert(object value)
//		{
//			var integerString = value?.ToString();

//			int integerResult;

//			if (int.TryParse(integerString, out integerResult))
//				return integerResult;

//			return null;
//		}

//		public int? MinimumValue
//		{
//			get { return Validator.Minimum; }
//			set { Validator.Minimum = value; }
//		}
//		public int? MaximumValue
//		{
//			get { return Validator.Maximum; }
//			set { Validator.Maximum = value; }
//		}
//	}
//}
