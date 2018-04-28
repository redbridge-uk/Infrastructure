using Moq;
using NUnit.Framework;
using Redbridge.SDK;
using Redbridge.Xamarin.Forms.Tests.Mocks;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace Redbridge.Forms.Tests
{
    public class ViewModelOne: NavigationPageViewModel
    {
        public bool OnDisposeCalled { get; set; }

        public ViewModelOne(ISchedulerService scheduler, INavigationService navService) :
            base(navService, scheduler)
        {
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            OnDisposeCalled = true;
        }
    }

    [TestFixture()]
	public class ViewModelTests
	{
		[Test()]
		public void GivenNavigationPageViewModel_WhenDispose_OnDisposeInSubClassIsInvoked()
		{
            var mockNavigationService = new Mock<INavigationService>();
			var model = new ViewModelOne(new MockSchedulerService(), mockNavigationService.Object);
            var modelCastAsInterface = (IPageViewModel)model;
            modelCastAsInterface.Dispose();
            Assert.IsTrue(model.OnDisposeCalled);
        }

        [Test()]
        public void GivenModalPageControllerViewModel_WhenDispose_OnDisposeInWrappedPageIsInvoked()
        {
            var mockNavigationService = new Mock<INavigationService>();
            var model = new ViewModelOne(new MockSchedulerService(), mockNavigationService.Object);
            var modalViewModel = new ModalPageControllerViewModel(model);
            modalViewModel.Dispose();
            Assert.IsTrue(model.OnDisposeCalled);
        }
    }
}
