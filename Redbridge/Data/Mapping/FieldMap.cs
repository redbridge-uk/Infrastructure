using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Redbridge.Data.Mapping
{
	public interface IFieldMap<in TRecord>
	{
		string FieldName { get; }
		object Read(TRecord record);
		void Write(TRecord record, object fieldValue);
	}

	public class FieldMap<TRecord, TSourceFieldType> : FieldMap<TRecord, TSourceFieldType, TSourceFieldType>
	{
		public FieldMap(string fieldName, Expression<Func<TRecord, TSourceFieldType>> func)
			: base(fieldName, func, (f) => (TSourceFieldType)f, null, null) { }

		public FieldMap(string fieldName, 
                           Expression<Func<TRecord, TSourceFieldType>> func, 
                           Expression<Func<object, TSourceFieldType>> readConvertFunc,
                           Expression<Func<TRecord, TSourceFieldType, 
                           TSourceFieldType>> convertFunc = null)
			: base(fieldName, func, readConvertFunc, convertFunc, null) { }

		public FieldMap(string fieldName, 
                           Expression<Func<TRecord, TSourceFieldType>> func, 
                           Expression<Func<object, TSourceFieldType>> readConvertFunc,
                           Expression<Func<TRecord, TSourceFieldType, TSourceFieldType>> convertFunc = null, 
                           Expression<Func<TSourceFieldType, TSourceFieldType>> convertBackFunc = null)
			: base(fieldName, func, readConvertFunc, convertFunc, convertBackFunc) { }

		protected override TryGetResult<TSourceFieldType> TryConvertBack(TSourceFieldType value)
		{
			var result = OnConvert(value);
			return TryGetResult.FromResult(result);
		}
	}

	public class FieldMap<TRecord, TSourceFieldType, TDestinationFieldType> : IFieldMap<TRecord>
	{
		private readonly Expression<Func<TRecord, TSourceFieldType>> _func;
		private readonly Expression<Func<TRecord, TSourceFieldType, TDestinationFieldType>> _convertFunc;
		private readonly Expression<Func<TDestinationFieldType, TSourceFieldType>> _convertBackFunc;
        private readonly Expression<Func<object, TDestinationFieldType>> _readConvertFunc;
		private readonly Dictionary<Type, Func<object, object>> _sourcetypeConverters = new Dictionary<Type, Func<object, object>>();
		private readonly MethodInfo _propertySetter;

		public FieldMap(string fieldName, Expression<Func<TRecord, TSourceFieldType>> func,
                           Expression<Func<object, TDestinationFieldType>> readConvertFunc,
							Expression<Func<TRecord, TSourceFieldType, TDestinationFieldType>> convertFunc = null,
							Expression<Func<TDestinationFieldType, TSourceFieldType>> convertBackFunc = null)
		{
            FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
			_func = func;
			_convertFunc = convertFunc;
			_convertBackFunc = convertBackFunc;
            _readConvertFunc = readConvertFunc;
			if (_func != null)
			{
				Expression body = func;
				body = ((LambdaExpression)body).Body;
				PropertyInfo propertyInfo;
				switch (body.NodeType)
				{
					case ExpressionType.MemberAccess:
						propertyInfo = (PropertyInfo)((MemberExpression)body).Member;
						break;
					default:
						throw new InvalidOperationException();
				}
                _propertySetter = propertyInfo.SetMethod;
			}

			LoadTypeConverters();
		}

		private void LoadTypeConverters()
		{
			_sourcetypeConverters.Add(typeof(bool), (f) => Convert.ToBoolean(f));
			_sourcetypeConverters.Add(typeof(bool?), (f) => Convert.ToBoolean(f));
			_sourcetypeConverters.Add(typeof(string), Convert.ToString);
			_sourcetypeConverters.Add(typeof(int), (f) => Convert.ToInt32(f));
			_sourcetypeConverters.Add(typeof(int?), (f) => Convert.ToInt32(f));
			_sourcetypeConverters.Add(typeof(decimal), (f) => Convert.ToDecimal(f));
			_sourcetypeConverters.Add(typeof(decimal?), (f) => Convert.ToDecimal(f));
			_sourcetypeConverters.Add(typeof(long), (f) => Convert.ToInt64(f));
			_sourcetypeConverters.Add(typeof(long?), (f) => Convert.ToInt64(f));
			_sourcetypeConverters.Add(typeof(DateTime), (f) => Convert.ToDateTime(f));
			_sourcetypeConverters.Add(typeof(DateTime?), (f) => Convert.ToDateTime(f));
			_sourcetypeConverters.Add(typeof(Guid), (f) => f as Guid?);
		}

		protected virtual bool RequiresValidation(object value)
		{
			return true;
		}

		protected virtual TDestinationFieldType OnConvert(object value)
        {
            return _readConvertFunc.Compile()(value);
        }

		public TDestinationFieldType Read(TRecord record)
		{
			var destinationType = OnRead(record);
			return OnConvertReadValue(record, destinationType);
		}

		protected virtual TDestinationFieldType OnConvertReadValue(TRecord record, TDestinationFieldType destinationType)
		{
			return destinationType;
		}

		protected virtual TDestinationFieldType OnRead(TRecord record)
		{
            var columnValue = _func.Compile()(record);
			var convertedValue = _convertFunc != null ? _convertFunc.Compile()(record, columnValue) : OnConvert(columnValue);
			return convertedValue;
		}

		public TryGetResult<TDestinationFieldType> TryConvert(object value)
		{
			TDestinationFieldType mappedValue;
			try
			{
				mappedValue = OnConvert(value);
			}
			catch (FormatException)
			{
				throw new RedbridgeException($"Failed to read value '{value}'. The value isn't in the format expected.");
			}

            return mappedValue.Equals(null) ? TryGetResult.Fail<TDestinationFieldType>() : TryGetResult.FromResult(mappedValue);
        }

		protected virtual TryGetResult<TSourceFieldType> TryConvertBack(TDestinationFieldType value)
		{
			if (_convertBackFunc != null)
			{
				return TryGetResult.FromResult(_convertBackFunc.Compile()(value));
			}

            if ( value == null )
				return TryGetResult.Fail<TSourceFieldType>();

			var targetType = typeof(TSourceFieldType);
			// See if we have a converter that can take our type and return the TValue...
            if (_sourcetypeConverters.TryGetValue(targetType, out var converter))
			{
				var otherConvertedValue = converter(value);
				return TryGetResult.FromResult((TSourceFieldType)otherConvertedValue);
			}

			return TryGetResult.Fail<TSourceFieldType>();
		}
		public void Write(TRecord newRecord, object value)
		{
			var converted = OnConvert(value);
			Write(newRecord, converted);
		}

		public virtual void Write(TRecord newRecord, TDestinationFieldType value)
		{
			var mappedValue = TryConvertBack(value);
			if (mappedValue.Success) _propertySetter.Invoke(newRecord, new object[] { mappedValue.Item });
		}

        public string FieldName { get; }

        object IFieldMap<TRecord>.Read(TRecord record)
        {
            return Read(record);
        }
    }
}
