using System;
namespace Redbridge.Validation.Markup
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class DirectoryExistsValidatorAttribute : PropertyValidatorAttribute
	{
		public DirectoryExistsValidatorAttribute() : base(new DirectoryExistsValidator())
		{
		}
	}
}
