using System.Diagnostics;
using System.Reflection;

namespace Redbridge.Validation
{
	[DebuggerDisplay("Property Validation Result: {Property} {Success} ({Message})")]
	public class PropertyValidationResult : ValidationResult
	{
		public PropertyValidationResult(PropertyInfo property, ValidationResult result) : base(result.Success, result.Message)
		{
			Property = property;
		}

		public static PropertyValidationResult Pass(PropertyInfo property)
		{
			return new PropertyValidationResult(property, ValidationResult.Pass());
		}

		public PropertyInfo Property
		{
			get;
		}

		public override string ToString()
		{
			return Message;
		}
	}
}
