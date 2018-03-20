//using System;
//using System.Globalization;
//using System.Linq.Expressions;
//using Redbridge.Validation;

//namespace Redbridge.Data.Mapping
//{
//	public class DateTimeFieldDefinition<TRecord> : FieldMap<TRecord, DateTime, StringPatternValidator>
//	{
//		private readonly CultureInfo _cultureInfo;

//		public DateTimeFieldDefinition(string fieldName, CultureInfo cultureInfo = null, bool isMandatory = false)
//			: base(fieldName, isMandatory: isMandatory)
//		{
//			_cultureInfo = cultureInfo;
//		}

//		public DateTimeFieldDefinition(string fieldName, Expression<Func<TRecord, DateTime>> func, CultureInfo cultureInfo = null, bool isMandatory = false)
//			: base(fieldName, isMandatory: isMandatory, func: func)
//		{
//			_cultureInfo = cultureInfo;
//		}

//		protected override bool RequiresValidation(object value)
//		{
//			return !(value is DateTime);
//		}

//		protected override DateTime OnConvert(object value)
//		{
//			if (value == null) return new DateTime();
//			if (value is DateTime) return (DateTime)value;

//			var dateTimeString = value.ToString();
//			DateTime dateTime;
//			if (_cultureInfo != null)
//				return DateTime.TryParse(dateTimeString, _cultureInfo, DateTimeStyles.None, out dateTime) ? dateTime : DateTime.MinValue;
//			else
//				return DateTime.TryParse(dateTimeString, out dateTime) ? dateTime : DateTime.MinValue;
//		}

//		public string Format { get; set; }

//		protected override string OnExport(DateTime value)
//		{
//			if (string.IsNullOrWhiteSpace(Format))
//				return value.ToString(CultureInfo.InvariantCulture);

//			return value.ToString(Format, CultureInfo.InvariantCulture);
//		}
//	}

//	public class NullableDateTimeFieldDefinition<TRecord> : FieldMap<TRecord, DateTime?, DateStringValidator>
//	{
//		private readonly CultureInfo _cultureInfo;

//		public NullableDateTimeFieldDefinition(string fieldName, CultureInfo cultureInfo = null, bool isMandatory = false)
//			: base(fieldName, isMandatory: isMandatory)
//		{
//			_cultureInfo = cultureInfo;
//		}
//		public NullableDateTimeFieldDefinition(string fieldName, Expression<Func<TRecord, DateTime?>> func, CultureInfo cultureInfo = null, bool isMandatory = false)
//			: base(fieldName, isMandatory, func)
//		{
//			_cultureInfo = cultureInfo;
//		}

//		protected override bool RequiresValidation(object value)
//		{
//			return !(value is DateTime);
//		}

//		protected override DateTime? OnConvert(object value)
//		{
//			if (value == null)
//				return new DateTime?();

//			if (value is DateTime)
//				return (DateTime)value;

//			var dateTimeString = value.ToString();
//			DateTime dateTime;
//			if (_cultureInfo != null)
//				return DateTime.TryParse(dateTimeString, _cultureInfo, DateTimeStyles.None, out dateTime) ? dateTime : new DateTime?();
//			else
//				return DateTime.TryParse(dateTimeString, out dateTime) ? dateTime : new DateTime?();
//		}

//		public string Format { get; set; }

//		protected override string OnExport(DateTime? value)
//		{
//			if (value.HasValue)
//			{
//				if (string.IsNullOrWhiteSpace(Format))
//					return value.Value.ToString(_cultureInfo ?? CultureInfo.InvariantCulture);

//				return value.Value.ToString(Format, _cultureInfo ?? CultureInfo.InvariantCulture);
//			}

//			return string.Empty;
//		}

//		protected override DateStringValidator OnCreateValidator()
//		{
//			return new DateStringValidator();
//		}
//	}
//}
