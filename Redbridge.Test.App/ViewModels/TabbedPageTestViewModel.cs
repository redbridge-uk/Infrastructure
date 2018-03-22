using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Redbridge.Forms;

namespace TesterApp.ViewModels
{
    public class TabbedPageTestViewModel : TabbedNavigationControllerViewModel
    {
        public TabbedPageTestViewModel(IViewModelFactory factory) : base(factory) { }

        protected override ObservableCollection<INavigationPageModel> CreatePages(IViewModelFactory factory)
        {
            var page1 = factory.CreateModel<SingleSectionTableViewModel>();
            var page2 = factory.CreateModel<SingleSectionTableViewModel>();
            var page3 = factory.CreateModel<SingleSectionTableViewModel>();
            return new ObservableCollection<INavigationPageModel>(new[] { page1, page2, page3 });
        }
    }
}
