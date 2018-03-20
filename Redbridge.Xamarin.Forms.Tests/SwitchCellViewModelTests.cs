using NUnit.Framework;
using Redbridge.Forms;

namespace Redbridge.Forms.Tests
{
	[TestFixture()]
	public class SwitchCellViewModelTests
	{
		public class BooleanData
		{
			public bool SomeValue { get; set; }
		}

		[Test()]
		public void SetDataValueFromSuppliedDataObject()
		{
			var data = new BooleanData() { SomeValue = false };
			var switchCell = new SwitchCellViewModel<BooleanData>(data, (s) => s.SomeValue);
			Assert.AreEqual(false, switchCell.Value);
		}

		[Test()]
		public void SetDataValueOnSaveTestExpectDataUpdated()
		{
			var data = new BooleanData() { SomeValue = false };
			var switchCell = new SwitchCellViewModel<BooleanData>(data, (s) => s.SomeValue);
			switchCell.Toggle();
			var result = switchCell.TrySave();
			Assert.AreEqual(true, result.Success);
			Assert.AreEqual(true, data.SomeValue);
		}
	}
}
