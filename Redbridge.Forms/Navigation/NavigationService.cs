using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redbridge.Diagnostics;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class NavigationService: INavigationService
	{
		protected readonly IViewFactory _viewFactory;
        protected readonly ILogger _logger;
        protected readonly IViewModelFactory _viewModelFactory;
        protected List<Page> _modalStack = new List<Page>();
        protected Dictionary<Page, IPageViewModel> _pageViewModelMap = new Dictionary<Page, IPageViewModel>();

		public NavigationService (IViewModelFactory viewModelFactory, IViewFactory viewFactory, ILogger logger)
		{
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
		}

		public Page CurrentPage
		{
			get
			{
				if (_modalStack.Any())
				{
					return _modalStack.Last();
				}

                if (Application.Current.MainPage is TabbedPage tabController)
                {
                    return tabController.CurrentPage;
                }

                if (Application.Current.MainPage is NavigationPage navigationController)
                {
                    return navigationController;
                }

                return Application.Current.MainPage;
			}
		}

		protected INavigation Navigation
		{
			get
			{
				var page = CurrentPage;
				return page?.Navigation;
			}
		}

		public async Task PopAsync()
        {
            var page = Navigation.NavigationStack.Last();
            var vm = _pageViewModelMap[page];
            if (await vm.NavigateBack())
            {
                await Navigation.PopAsync();
                vm.Dispose();
                _pageViewModelMap.Remove(page);
            }
		}

		public async Task PopModalAsync()
		{
            var page = Navigation.ModalStack.Last();
            var vm = _pageViewModelMap[page];
            if (await vm.NavigateBack())
            {
                await Navigation.PopModalAsync();
                vm.Dispose();
                _modalStack.Remove(page);
                _pageViewModelMap.Remove(page);
            }
		}

		public async Task PushAsync<T>() where T : IPageViewModel
		{
			var model = _viewModelFactory.CreateModel<T>();
			var view = _viewFactory.CreatePage(model);
            _pageViewModelMap.Add(view, model);
			await Navigation.PushAsync(view);
		}

		public async Task PushAsync(IPageViewModel model)
		{
			_logger.WriteInfo($"Pushing navigation page {model.Title} onto stack...");
			if (model == null) throw new ArgumentNullException(nameof(model));
			var view = _viewFactory.CreatePage(model);
            _pageViewModelMap.Add(view, model);
			await Navigation.PushAsync(view);
		}

		public async Task PushModalAsync<T>() where T : INavigationPageModel
		{
			var navigationmodel = _viewModelFactory.CreateModel<ModalPageControllerViewModel<T>>();
			var page = _viewFactory.CreatePage(navigationmodel, true);
			await Navigation.PushModalAsync(page);
			_modalStack.Add(page);
            _pageViewModelMap.Add(page, navigationmodel);
		}

		public async Task PushModalAsync(INavigationPageModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			var navigationmodel = new ModalPageControllerViewModel(model);
			var page = _viewFactory.CreatePage(navigationmodel, true);
			await Navigation.PushModalAsync(page);
			_modalStack.Add(page);
            _pageViewModelMap.Add(page, model);
		}
	}
}
