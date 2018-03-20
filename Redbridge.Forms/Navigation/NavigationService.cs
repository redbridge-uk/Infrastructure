using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redbridge.Diagnostics;
using Redbridge.SDK;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class NavigationService : INavigationService
	{
		private readonly IViewFactory _viewFactory;
		private readonly ILogger _logger;
		private readonly IViewModelFactory _viewModelFactory;
		private List<Page> _modalStack = new List<Page>();
        private Dictionary<Page, IPageViewModel> _pageViewModelMap = new Dictionary<Page, IPageViewModel>();

		public NavigationService (IViewModelFactory viewModelFactory, IViewFactory viewFactory, ILogger logger)
		{
			if (viewModelFactory == null) throw new ArgumentNullException(nameof(viewModelFactory));
			if (logger == null) throw new ArgumentNullException(nameof(logger));
			if (viewFactory == null) throw new ArgumentNullException(nameof(viewFactory));
			_viewFactory = viewFactory;
			_logger = logger;
			_viewModelFactory = viewModelFactory;
		}

		public Page CurrentPage
		{
			get
			{
				if (_modalStack.Any())
				{
					return _modalStack.Last();
				}

				var tabController = Application.Current.MainPage as TabbedPage;

				if (tabController != null)
				{
					return tabController.CurrentPage;
				}

				var navigationController = Application.Current.MainPage as NavigationPage;

				if (navigationController != null)
				{
					return navigationController;
				}

				return Application.Current.MainPage;
			}
		}

		private INavigation Navigation
		{
			get
			{
				var page = CurrentPage;
				return page.Navigation;
			}
		}

		public async Task PopAsync()
		{
			var page = await Navigation.PopAsync();
            _pageViewModelMap[page].NavigateBack();
            _pageViewModelMap.Remove(page);
		}

		public async Task PopModalAsync()
		{
			var page = await Navigation.PopModalAsync();
            _pageViewModelMap[page].NavigateBack();
			_modalStack.Remove(page);
            _pageViewModelMap.Remove(page);
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
