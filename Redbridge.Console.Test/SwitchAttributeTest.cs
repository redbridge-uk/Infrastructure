﻿using NUnit.Framework;

namespace Redbridge.Console.Test
{

	[TestFixture]
	public class SwitchAttributeTest
	{
		[TestCase]
		public void ConstructSwitchAttribute_ExpectSuccess()
		{
			var attribute = new SwitchAttribute("NoLog");
			Assert.IsFalse(attribute.AllowEmptyValues, "Allow empty values is not supported.");
            Assert.IsFalse(attribute.DefaultValue);
		}


        [TestCase]
		public void ConstructSwitchAttribute_BlankName_ExpectException()
        {
            Assert.Throws<CommandLineParseException>(() => new SwitchAttribute(string.Empty));
		}
	}
}
