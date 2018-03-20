using System;
using NUnit.Framework;

namespace Redbridge.Console.Test
{
	[TestFixture]
	public class IntegerParameterAttributeTest
	{
		[TestCase]
		public void ConstructIntegerParameterAttribute()
		{
			IntegerParameterAttribute attribute = new IntegerParameterAttribute("Port");
			Assert.IsFalse(attribute.AllowEmptyValues, "Allow empty values is not supported.");
		}

		[TestCase]
		public void ConstructIntegerParameterAttribute_BlankName_ExpectException()
		{
			try
			{ 
				var attribute = new IntegerParameterAttribute(string.Empty); 
				Assert.IsNotNull(attribute);
			}
			catch (CommandLineParseException)
			{

			}
			catch (Exception)
			{
				Assert.Fail("Expected specific type.");
			}
		}
	}
}
