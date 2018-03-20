using System.Linq;
using NUnit.Framework;
using Redbridge.SDK;

namespace Redbridge.Forms.Tests
{
	public enum TestEnum
	{
		One = 0,
		Two = 1,
		[EnumDescription("Tester")]
		Three = 2,
	}

	[TestFixture()]
	public class ActionSheetOptionTests
	{
		[Test()]
		public void CreateActionSheetOptionFromEnumValue()
		{
			var option = ActionSheetOption.FromOption(TestEnum.One);
			Assert.AreEqual(option.Value, TestEnum.One);
			Assert.AreEqual("One", option.Title);
		}

		[Test()]
		public void CreateActionSheetOptionFromEnumValueWithDescription()
		{
			var option = ActionSheetOption.FromOption(TestEnum.Three);
			Assert.AreEqual(option.Value, TestEnum.Three);
			Assert.AreEqual("Tester", option.Title);
		}

		[Test()]
		public void CreateActionSheetOptionsFromEnumValue()
		{
			var option = ActionSheetViewModel.FromEnum<TestEnum>();
			Assert.IsNotNull(option);
			Assert.AreEqual(3, option.Options.Count());
		}
	}
}
