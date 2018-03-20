using NUnit.Framework;
using System;
using Redbridge.Forms;

namespace Redbridge.Xamarin.Forms.Tests.Cells
{
    [TestFixture()]
    public class DateEntryCellViewModelTests
    {
        [Test]
        public void CreateDateEntryCellViewModelExpectSuccess()
        {
			var cell = new DateTimeCellViewModel();
			Assert.AreEqual(cell.Accessories, CellIndicators.None);
			Assert.IsNull(cell.Text);
			Assert.IsNull(cell.MaximumDate);
			Assert.IsNull(cell.MinimumDate);
			Assert.IsTrue(cell.AllowCellSelection);
			Assert.IsTrue(cell.IsEnabled);
			Assert.IsNull(cell.CommandParameter);
			Assert.AreEqual(cell.ClearButtonMode, TextClearButtonMode.Never);
            Assert.IsTrue(cell.IsValid);
        }

		[Test]
		public void CreateDateEntryCellViewModelSettingTextSetsNumber()
		{
            var cell = new DateTimeCellViewModel()
            {
                Text = "23"
            };

            Assert.IsFalse(cell.IsValid);
            Assert.IsNull(cell.Value);
		}

        [Test]
        public void CreateDateTimeCellViewModelValidateExpiredDate ()
        {
            var cell = new DateTimeCellViewModel();
            cell.MaximumDate = new DateTime(2018, 01, 01);
            cell.Value = new DateTime(2019, 01, 01);
            Assert.IsFalse(cell.IsValid);
        }
    }
}
