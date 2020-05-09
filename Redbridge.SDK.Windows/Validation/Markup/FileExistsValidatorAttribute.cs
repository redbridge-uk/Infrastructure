using System;

namespace Redbridge.Windows.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class FileExistsValidatorAttribute : PropertyValidatorAttribute
	{
		public FileExistsValidatorAttribute() : base(new FileExistsValidator()) { }
	}
}
