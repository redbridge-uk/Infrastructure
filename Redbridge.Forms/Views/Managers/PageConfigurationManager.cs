using System;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Linq;
using Xamarin.Forms;
using Redbridge.Linq;
using System.Diagnostics;

namespace Redbridge.Forms
{
    public class PageConfigurationManager<TViewModel> : IDisposable
        where TViewModel:IPageViewModel
    {
        private CompositeDisposable _compositeDisposables = new CompositeDisposable();
        private readonly ToolbarItemFactory _toolbarItemFactory;
        private readonly Page _page;

        public PageConfigurationManager(Page page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            _page = page;
            _toolbarItemFactory = new ToolbarItemFactory();
        }

        protected Page ParentPage => _page;

        public CompositeDisposable Disposibles
        {
            get { return _compositeDisposables; }
        }

        public virtual void ConfigurePage (TViewModel viewModel)
        {
            Debug.WriteLine($"Configuring page with view model type {viewModel.GetType()}");
            _page.SetBinding(Page.TitleProperty, "Title");
            _page.SetBinding(Page.IconProperty, "Icon");
            _page.SetBinding(VisualElement.IsFocusedProperty, "IsFocused");
            _page.SetBinding(NavigationPage.HasNavigationBarProperty, "ShowNavigationBar");
            _page.SetBinding(NavigationPage.BarTextColorProperty, "NavigationBarTextColour");
            _page.SetBinding(NavigationPage.BarBackgroundColorProperty, "NavigationBarColour");
            _page.SetBinding(NavigationPage.HasBackButtonProperty, "IsBackButtonVisible");
            _page.SetBinding(NavigationPage.BackButtonTitleProperty, "BackButtonText");

            if (viewModel.ShowNavigationBar)
                Debug.WriteLine("Instructing the page to show the navigation bar");
            else 
                Debug.WriteLine("Instructing the page to hide the navigation bar");

            NavigationPage.SetHasNavigationBar(_page, viewModel.ShowNavigationBar);
            OnConfigureToolbar(_page, viewModel.Toolbar);
        }

        private void OnConfigureToolbar(Page page, ToolbarViewModel toolbar)
        {
            Debug.WriteLine($"Configuring toolbar on page {page.GetType()}");
            page.ToolbarItems.Clear();
            var items = toolbar.ToolbarItems.Select(tb => _toolbarItemFactory.CreateToolbarItem(tb));
            items.ForEach(i => page.ToolbarItems.Add(i));
        }

        public void Dispose()
        {
            Debug.WriteLine("Disposing page manager");
            _compositeDisposables.Dispose();
        }
    }
    
}
