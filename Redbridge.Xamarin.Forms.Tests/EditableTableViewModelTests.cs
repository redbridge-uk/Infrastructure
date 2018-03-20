using NUnit.Framework;
using System;
using Redbridge.Forms;
using Moq;
using Redbridge.SDK;

namespace Redbridge.Xamarin.Forms.Tests
{
    [TestFixture()]
    public class EditableTableViewModelTests
    {
        [Test()]
        public void CreateEditableTableViewModelExpectNotInEditMode()
        {
            var navService = new Mock<INavigationService>();
            var scheduleService = new Mock<ISchedulerService>();
            var alerts = new Mock<IAlertController>();
            var model = new EditableTableViewModel(alerts.Object, navService.Object, scheduleService.Object);
            Assert.IsFalse(model.EditMode);
        }
    }
}
