using System;
namespace Redbridge.Validation
{
	public interface IObjectValidator<in TObject> : IObjectValidator
		where TObject : class
	{
		ValidationResultCollection Validate(TObject item);
	}

	public interface IObjectValidator
	{
		ValidationResultCollection Validate(object item);
	}
}
