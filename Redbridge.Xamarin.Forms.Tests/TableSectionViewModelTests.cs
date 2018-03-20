using NUnit.Framework;
using System;
using Redbridge.Forms;

namespace Redbridge.Xamarin.Forms.Tests
{
    [TestFixture()]
    public class TableSectionViewModelTests
    {
        [Test()]
        public void CreateTableSectionCheckDefaults()
        {
            var section = new TableSectionViewModel();
            Assert.IsNotNull(section);
            Assert.IsFalse(section.EditMode);
        }

		[Test()]
		public void CreateTableSectionSetEditModeCheckCellsFollow()
		{
			var section = new TableSectionViewModel();
			var cell = section.AddTextCell("Test");
            Assert.IsFalse(cell.EditMode);
            section.BeginEdit();
            Assert.IsTrue(cell.EditMode);
		}
    }
}
