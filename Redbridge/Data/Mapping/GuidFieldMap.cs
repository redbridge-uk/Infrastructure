//using System;
//using System.Linq.Expressions;
//using Redbridge.Validation;

//namespace Redbridge.Data.Mapping
//{
//	public class GuidFieldDefinition<TRecord> : FieldMap<TRecord, Guid, GuidValidator>
//	{
//		public GuidFieldDefinition(string fieldName, bool isMandatory = false) : base(fieldName, isMandatory: isMandatory) { }
//		public GuidFieldDefinition(string fieldName, Expression<Func<TRecord, Guid>> func, bool isMandatory = false) : base(fieldName, isMandatory, func) { }

//		protected override Guid OnConvert(object value)
//		{
//			return value != null ? Guid.Parse(value.ToString()) : Guid.Empty;
//		}
//	}
//}
