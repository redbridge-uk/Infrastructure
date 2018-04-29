using Moq;
using NUnit.Framework;
using Redbridge.Diagnostics;
using Redbridge.Forms;
using Redbridge.Forms.Navigation;
using Redbridge.Forms.Tests;
using Redbridge.Xamarin.Forms.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Redbridge.Xamarin.Forms.Tests
{
    [TestFixture()]
    public class NavigationServiceTests
    {
		[Test()]
        public async Task GivenViewAwareOfHardwareBackButton_WhenHardwareBackButtonPressed_NavigationServiceRemovesView()
        {
            var mockApp = new Mock<Application>();
            var mockMainPage = new Mock<Page>();
            var mockCurrentPageService = new Mock<ICurrentPageService>();
            var mockNavigation = new Mock<INavigation>();
            var mockViewModelFactory = new Mock<IViewModelFactory>();
            var mockViewFactory = new Mock<IViewFactory>();
            var mockLogger = new Mock<ILogger>();
            var mockScheduler = new MockSchedulerService();

            mockCurrentPageService.Setup((app) => app.Current).Returns(mockMainPage.Object);
            mockCurrentPageService.Setup((app) => app.Navigation).Returns(mockNavigation.Object);

            var mockPage = new Mock<Page>();
            var mockHardwareButtoneEnabledPage = mockPage.As<IHardwareNavigationAware>();

            mockViewFactory.Setup((factory) => factory.CreatePage(It.IsAny<object>(), false))
                           .Returns(mockPage.Object);

            var navigationService = new NavigationService(mockViewModelFactory.Object, mockViewFactory.Object, mockLogger.Object, mockCurrentPageService.Object);
            var mockViewModel = new Mock<IPageViewModel>();

            // Navigation to page
            await navigationService.PushAsync(mockViewModel.Object);

            // Verify page is pushed
            mockNavigation.Verify((nav) => nav.PushAsync(mockPage.Object), Times.Once);

            // Simulate that the hardware back button was called
            mockHardwareButtoneEnabledPage.Raise((vm) => vm.BackButtonPressed += null,
                                                 EventArgs.Empty,
                                                 mockPage.Object);

            // Verify page is popped and vm disposed
            mockNavigation.Verify((nav) => nav.PopAsync(), Times.Once);
            mockViewModel.Verify((vm) => vm.Dispose(), Times.Once);
        }

        [Test()]
        public async Task GivenModalViewAwareOfHardwareBackButton_WhenHardwareBackButtonPressed_NavigationServiceRemovesView()
        {
            var mockApp = new Mock<Application>();
            var mockMainPage = new Mock<Page>();
            var mockCurrentPageService = new Mock<ICurrentPageService>();
            var mockNavigation = new Mock<INavigation>();
            var mockViewModelFactory = new Mock<IViewModelFactory>();
            var mockViewFactory = new Mock<IViewFactory>();
            var mockLogger = new Mock<ILogger>();
            var mockScheduler = new MockSchedulerService();

            mockCurrentPageService.Setup((app) => app.Current).Returns(mockMainPage.Object);
            mockCurrentPageService.Setup((app) => app.Navigation).Returns(mockNavigation.Object);

            var mockPage = new Mock<Page>();
            var mockHardwareButtonEnabledPage = mockPage.As<IHardwareNavigationAware>();

            mockViewFactory.Setup((factory) => factory.CreatePage(It.IsAny<object>(), true))
                           .Returns(mockPage.Object);

            var navigationService = new NavigationService(mockViewModelFactory.Object, mockViewFactory.Object, mockLogger.Object, mockCurrentPageService.Object);
            var mockViewModel = new Mock<INavigationPageModel>();

            // Navigation to page
            await navigationService.PushModalAsync(mockViewModel.Object);

            // Verify page is pushed
            mockNavigation.Verify((nav) => nav.PushModalAsync(mockPage.Object), Times.Once);

            // Simulate that the hardware back button was called
            mockHardwareButtonEnabledPage.Raise((vm) => vm.BackButtonPressed += null,
                                                 EventArgs.Empty,
                                                 mockPage.Object);

            // Verify page is popped and vm disposed
            mockNavigation.Verify((nav) => nav.PopModalAsync(), Times.Once);
            mockViewModel.Verify((vm) => vm.Dispose(), Times.Once);
        }

        [Test()]
        public async Task GivenViewModel_WhenViewModelDisallowsBackNavigation_PageIsNotNavigatedAwayFrom()
        {
            var mockApp = new Mock<Application>();
            var mockMainPage = new Mock<Page>();
            var mockCurrentPageService = new Mock<ICurrentPageService>();
            var mockNavigation = new Mock<INavigation>();
            var mockViewModelFactory = new Mock<IViewModelFactory>();
            var mockViewFactory = new Mock<IViewFactory>();
            var mockLogger = new Mock<ILogger>();
            var mockScheduler = new MockSchedulerService();
            var mockViewModel = new Mock<IPageViewModel>();
            var mockPage = new Mock<Page>();

            mockCurrentPageService.Setup((app) => app.Current).Returns(mockMainPage.Object);
            mockCurrentPageService.Setup((app) => app.Navigation).Returns(mockNavigation.Object);
            mockViewModel.Setup((vm) => vm.NavigateBack()).Returns(Task.FromResult(false));
            mockNavigation.Setup((nav) => nav.NavigationStack).Returns(new List<Page>(new Page[] { mockPage.Object }));

            mockViewFactory.Setup((factory) => factory.CreatePage(It.IsAny<object>(), false))
                           .Returns(mockPage.Object);

            var navigationService = new NavigationService(mockViewModelFactory.Object, mockViewFactory.Object, mockLogger.Object, mockCurrentPageService.Object);

            // Navigation to page
            await navigationService.PushAsync(mockViewModel.Object);

            // Verify page is pushed
            mockNavigation.Verify((nav) => nav.PushAsync(mockPage.Object), Times.Once);

            // Try to navigate away from page
            await navigationService.PopAsync();

            // Verify page is popped and vm disposed
            mockNavigation.Verify((nav) => nav.PopAsync(), Times.Never);
            mockViewModel.Verify((vm) => vm.Dispose(), Times.Never);
        }

        [Test()]
        public async Task GivenViewModel_WhenViewModelAllowsBackNavigation_PageIsNavigatedAwayFrom()
        {
            var mockApp = new Mock<Application>();
            var mockMainPage = new Mock<Page>();
            var mockCurrentPageService = new Mock<ICurrentPageService>();
            var mockNavigation = new Mock<INavigation>();
            var mockViewModelFactory = new Mock<IViewModelFactory>();
            var mockViewFactory = new Mock<IViewFactory>();
            var mockLogger = new Mock<ILogger>();
            var mockScheduler = new MockSchedulerService();
            var mockViewModel = new Mock<IPageViewModel>();
            var mockPage = new Mock<Page>();

            mockCurrentPageService.Setup((app) => app.Current).Returns(mockMainPage.Object);
            mockCurrentPageService.Setup((app) => app.Navigation).Returns(mockNavigation.Object);
            mockViewModel.Setup((vm) => vm.NavigateBack()).Returns(Task.FromResult(true));
            mockNavigation.Setup((nav) => nav.NavigationStack).Returns(new List<Page>(new Page[] { mockPage.Object }));

            mockViewFactory.Setup((factory) => factory.CreatePage(It.IsAny<object>(), false))
                           .Returns(mockPage.Object);

            var navigationService = new NavigationService(mockViewModelFactory.Object, mockViewFactory.Object, mockLogger.Object, mockCurrentPageService.Object);

            // Navigation to page
            await navigationService.PushAsync(mockViewModel.Object);

            // Verify page is pushed
            mockNavigation.Verify((nav) => nav.PushAsync(mockPage.Object), Times.Once);

            // Try to navigate away from page
            await navigationService.PopAsync();

            // Verify page is popped and vm disposed
            mockNavigation.Verify((nav) => nav.PopAsync(), Times.Once);
            mockViewModel.Verify((vm) => vm.Dispose(), Times.Once);
        }

    }
}
