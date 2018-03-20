using Redbridge.Diagnostics;

namespace Redbridge.Forms
{
    public abstract class MvvmNavigationApp<TStartViewModel> : MvvmApp<NavigationControllerViewModel<TStartViewModel>>
    where TStartViewModel : INavigationPageModel
    {
        public MvvmNavigationApp(ILogger logger, IViewFactory viewFactory) : base(logger, viewFactory) { }
    }
}
