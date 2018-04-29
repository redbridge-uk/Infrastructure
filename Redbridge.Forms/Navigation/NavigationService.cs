using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redbridge.Diagnostics;
using Redbridge.Forms.Navigation;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public class NavigationService: INavigationService
	{
        private readonly ICurrentPageService _currentPageService;
        protected readonly IViewFactory _viewFactory;
        protected readonly ILogger _logger;
        protected readonly IViewModelFactory _viewModelFactory;
        protected List<Page> _modalStack = new List<Page>();
        protected Dictionary<Page, IPageViewModel> _pageViewModelMap = new Dictionary<Page, IPageViewModel>();

		public NavigationService (IViewModelFactory viewModelFactory,
                                  IViewFactory viewFactory,
                                  ILogger logger,
                                  ICurrentPageService currentPageService)
		{
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
            _currentPageService = currentPageService ?? throw new ArgumentNullException(nameof(currentPageService));
        }

		public Page CurrentPage
		{
			get
			{
				if (_modalStack.Any())
					return _modalStack.Last();

                return _currentPageService.GetCurrent();
			}
		}

		protected INavigation Navigation
		{
			get
			{
                return _currentPageService.GetNavigation();
            }
		}

		public async Task PopAsync()
        {
            var page = Navigation.NavigationStack.Last();
            var vm = _pageViewModelMap[page];
            if (await vm.NavigateBack())
                await CleanupPage(page);
        }

		public async Task PopModalAsync()
		{
            var page = Navigation.ModalStack.Last();
            var vm = _pageViewModelMap[page];
            if (await vm.NavigateBack())
               await CleanupPage(page);
		}

		public async Task PushAsync<T>() where T : IPageViewModel
		{
			var model = _viewModelFactory.CreateModel<T>();
			var view = _viewFactory.CreatePage(model);
            await InitialisePage(view, model);
        }

        public async Task PushAsync(IPageViewModel model)
		{
			_logger.WriteInfo($"Pushing navigation page {model.Title} onto stack...");
			if (model == null) throw new ArgumentNullException(nameof(model));
			var view = _viewFactory.CreatePage(model);
            await InitialisePage(view, model);
        }

        public async Task PushModalAsync<T>() where T : INavigationPageModel
		{
			var navigationmodel = _viewModelFactory.CreateModel<ModalPageControllerViewModel<T>>();
			var page = _viewFactory.CreatePage(navigationmodel, true);
			await Navigation.PushModalAsync(page);
            await InitialiseModalPage(page, navigationmodel);
		}

		public async Task PushModalAsync(INavigationPageModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			var navigationmodel = new ModalPageControllerViewModel(model);
			var page = _viewFactory.CreatePage(navigationmodel, true);
            await InitialiseModalPage(page, model);
        }

        protected async Task InitialisePage(Page page, IPageViewModel model)
        {
            if (page is IHardwareNavigationAware view)
                view.OnHardwareBackButtonPressed += ViewOnBackButtonPressed;

            await Navigation.PushAsync(page);
            _pageViewModelMap.Add(page, model);
        }

        protected async Task InitialiseModalPage(Page page, IPageViewModel model)
        {
            if (page is IHardwareNavigationAware view)
                view.OnHardwareBackButtonPressed += ViewOnBackButtonPressed;

            await Navigation.PushModalAsync(page);
            _modalStack.Add(page);
            _pageViewModelMap.Add(page, model);
        }

        protected async Task CleanupPage(Page page)
        {
            if (page is IHardwareNavigationAware view)
                view.OnHardwareBackButtonPressed -= ViewOnBackButtonPressed;

            if (_pageViewModelMap.TryGetValue(page, out var viewModel))
            {
                viewModel.Dispose();
                _pageViewModelMap.Remove(page);
                if (_modalStack.Contains(page))
                {
                    await Navigation.PopModalAsync();
                    _modalStack.Remove(page);
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
        }

        private async void ViewOnBackButtonPressed(object sender, Page page)
        {
            await CleanupPage(page);
        }
    }
}
