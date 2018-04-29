using NUnit.Framework;
using Redbridge.Forms;
using Moq;
using Redbridge.Xamarin.Forms.Tests.Mocks;

namespace Redbridge.Xamarin.Forms.Tests
{
    [TestFixture()]
    public class EditableTableViewModelTests
    {
        [Test()]
        public void CreateEditableTableViewModelExpectNotInEditMode()
        {
            var navService = new Mock<INavigationService>();
            var scheduleService = new MockSchedulerService();
            var alerts = new Mock<IAlertController>();
            var model = new EditableTableViewModel(alerts.Object, navService.Object, scheduleService);
            Assert.IsFalse(model.EditMode);
        }
    }
}
