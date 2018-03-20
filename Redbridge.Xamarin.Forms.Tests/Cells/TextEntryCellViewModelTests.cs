using NUnit.Framework;
using System;
using Redbridge.Forms;

namespace Redbridge.Xamarin.Forms.Tests.Cells
{
    [TestFixture()]
    public class TextEntryCellViewModelTests
    {
        [Test()]
        public void CreateTextEntryCellViewModelExpectSuccess()
        {
            var cell = new TextEntryCellViewModel();
            Assert.AreEqual(cell.Accessories, CellIndicators.None);
            Assert.IsNull(cell.Text);
            Assert.IsNull(cell.MaximumLength);
            Assert.IsNull(cell.MinimumLength);
            Assert.IsFalse(cell.AllowCellSelection);
            Assert.IsFalse(cell.IsSecure);
            Assert.IsTrue(cell.IsEnabled);
            Assert.IsNull(cell.CommandParameter);
            Assert.IsTrue(cell.IsValid);
            Assert.AreEqual(cell.ClearButtonMode, TextClearButtonMode.WhilstEditing);
        }

		[Test()]
		public void CreateTextEntryCellViewModelExpectValidationError()
		{
			var cell = new TextEntryCellViewModel();
            cell.MaximumLength = 10;
            cell.Text = "This text is too long.";
            Assert.IsFalse(cell.IsValid);
		}
    }
}
