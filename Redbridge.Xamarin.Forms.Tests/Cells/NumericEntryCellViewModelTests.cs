using NUnit.Framework;
using System;
using Redbridge.Forms;

namespace Redbridge.Xamarin.Forms.Tests.Cells
{
    [TestFixture()]
    public class NumericEntryCellViewModelTests
    {
        [Test]
        public void CreateNumericEntryCellViewModelExpectSuccess()
        {
			var cell = new NumericEntryCellViewModel();
			Assert.AreEqual(cell.Accessories, CellIndicators.None);
			Assert.IsNull(cell.Text);
			Assert.IsNull(cell.MaximumValue);
			Assert.IsNull(cell.MinimumValue);
			Assert.IsFalse(cell.AllowCellSelection);
			Assert.IsTrue(cell.IsEnabled);
			Assert.IsNull(cell.CommandParameter);
			Assert.AreEqual(cell.ClearButtonMode, TextClearButtonMode.WhilstEditing);
            Assert.IsTrue(cell.IsValid);
            Assert.IsFalse(cell.IsSigned);
        }

		[Test]
		public void CreateNumericEntryCellViewModelSettingTextInvalidLeavesValueNullAndInvalid()
		{
			var cell = new NumericEntryCellViewModel()
			{
				Text = "ballbags"
			};

			Assert.IsNull(cell.Value);
            Assert.IsFalse(cell.IsValid);
		}

		[Test]
		public void CreateNumericEntryCellViewModelSettingTextSetsNumber()
		{
            var cell = new NumericEntryCellViewModel()
            {
                Text = "23"
            };

            Assert.AreEqual(23M, cell.Value);
		}

        [Test]
        public void CreateNumericEntryCellViewModelValidateUnsignedInteger ()
        {
            var cell = new NumericEntryCellViewModel();
            cell.MaximumValue = 20;
            cell.Value = 23;
            Assert.IsFalse(cell.IsValid);
        }
    }
}
