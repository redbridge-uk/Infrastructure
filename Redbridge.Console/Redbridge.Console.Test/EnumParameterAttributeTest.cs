using System;
using NUnit.Framework;

namespace Redbridge.Console.Test
{
	[TestFixture]
	public class EnumParameterAttributeTest
	{
		[TestCase]
		public void ConstructEnumParameterAttribute_CheckEmptyValues()
		{
			var attribute = new EnumParameterAttribute("Port");
			Assert.IsFalse(attribute.AllowEmptyValues, "Allow empty values is not supported.");
		}

		[TestCase]
		public void ConstructEnumParameterAttribute_BlankName_ExpectException()
		{
			var attribute = new EnumParameterAttribute(string.Empty);
			Assert.IsNotNull(attribute);
		}

		[TestCase]
		public void ConstructIntegerParameterAttribute_CheckName()
		{
			var attribute = new EnumParameterAttribute("Port");
			Assert.AreEqual("Port", attribute.ParameterName, "Incorrect parameter name.");
		}
	}
}
