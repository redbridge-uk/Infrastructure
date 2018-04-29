using Xamarin.Forms;

namespace Redbridge.Forms.Navigation
{
    public class XamarinAppCurrentPageService : ICurrentPageService
    {
        public Page Current
        {
            get 
            { 
                var page = Application.Current.MainPage;

                if (page is TabbedPage tabController)
                    return tabController.CurrentPage;

                return page;
            }
        }

        public INavigation Navigation
        {
            get { return Current.Navigation; }
        }
    }
}
