//using Redbridge.Validation;

//namespace Redbridge.Data.Mapping
//{
//	public class Int64FieldMap<TRecord> : FieldMap<TRecord, long, Int64Validator>
//	{
//		public Int64FieldMap(string fieldName, bool isMandatory = false) : base(fieldName, isMandatory: isMandatory) { }

//		protected override long OnConvert(object value)
//		{
//			var integerString = value?.ToString() ?? string.Empty;

//			if (long.TryParse(integerString, out long result))
//			{
//				return result;
//			}

//			return 0;
//		}
//	}

//	public class NullableInt64FieldMap<TRecord> : FieldMap<TRecord, long?, Int64Validator>
//	{
//		public NullableInt64FieldMap(string columnName) : base(columnName, isMandatory: false) { }

//		protected override long? OnConvert(object value)
//		{
//			var longString = value?.ToString();

//			long longResult;

//			if (long.TryParse(longString, out longResult))
//				return longResult;

//			return null;
//		}

//		public long? MinimumValue
//		{
//			get { return Validator.Minimum; }
//			set { Validator.Minimum = value; }
//		}
//		public long? MaximumValue
//		{
//			get { return Validator.Maximum; }
//			set { Validator.Maximum = value; }
//		}
//	}
//}
