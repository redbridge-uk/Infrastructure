using System;
using NUnit.Framework;

namespace Redbridge.Console.Test
{
	[TestFixture]
	public class StringParameterAttributeTest
	{
		[TestCase]
		public void ConstructStringParameterAttribute()
		{
			StringParameterAttribute attribute = new StringParameterAttribute("NoLog");
			Assert.IsFalse(attribute.AllowEmptyValues, "Allow empty values is not supported.");
		}

		[TestCase]
		public void ConstructStringParameterAttribute_BlankName_ExpectException()
		{
			try
			{
				var attribute = new StringParameterAttribute(string.Empty);
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
