using System;
using System.Collections.ObjectModel;

namespace Redbridge.Forms
{
    public abstract class TabbedNavigationControllerViewModel : NavigationControllerViewModel, ITabbedNavigationControllerViewModel
    {
        readonly IViewModelFactory _factory;
        private Lazy<ObservableCollection<INavigationPageModel>> _lazyPages;

        public TabbedNavigationControllerViewModel(IViewModelFactory factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            _factory = factory;
            _lazyPages = new Lazy<ObservableCollection<INavigationPageModel>>(() => CreatePages(_factory));
        }

        protected abstract ObservableCollection<INavigationPageModel> CreatePages(IViewModelFactory factory);

        public void SetPage(INavigationPageModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            this.CurrentPage = model;
        }

        public virtual ObservableCollection<INavigationPageModel> Pages => _lazyPages.Value;
    }
}
