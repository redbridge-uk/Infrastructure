using System;
using System.Linq.Expressions;
using System.Reflection;
using Redbridge.SDK;
using Redbridge.Validation;

namespace Redbridge.Forms
{

	public class PropertyValueManager<TTarget, TPropertyType>
	where TTarget: class
	{
		readonly Expression<Func<TTarget, TPropertyType>> _propertySetter;
		readonly TTarget _data;

		public PropertyValueManager(TTarget data, Expression<Func<TTarget, TPropertyType>> propertySetter)
		{
			if (propertySetter == null) throw new ArgumentNullException(nameof(propertySetter));
			if (data == null) throw new ArgumentNullException(nameof(data));
			_data = data;
			_propertySetter = propertySetter;
		}

		public TPropertyType GetValue()
		{
			Expression body = _propertySetter;
			body = ((LambdaExpression)body).Body;
			PropertyInfo propertyInfo;
			switch (body.NodeType)
			{
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
					var ue = body as UnaryExpression;
					var memberExpression = ((ue != null) ? ue.Operand : null) as MemberExpression;
					propertyInfo = (PropertyInfo)(memberExpression).Member;
					break;
				case ExpressionType.MemberAccess:
					propertyInfo = (PropertyInfo)((MemberExpression)body).Member;
					break;
				default:
					throw new InvalidOperationException();
			}

			if (propertyInfo.CanRead)
			{
				var propertyValue = propertyInfo.GetValue(_data);
				return (TPropertyType)propertyValue;
			}

			throw new NotSupportedException("Unable to get the value on the property as it is not readable.");
		}

		public ValidationResultCollection TrySave(TPropertyType value)
		{
			Expression body = _propertySetter;
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
			if (propertyInfo.CanWrite)
			{
				propertyInfo.SetValue(_data, value);
			}
			else
			{
				throw new NotSupportedException("Unable to set the value on the property as it is read-only.");
			}

			return new ValidationResultCollection();
		}
	}
	
}
