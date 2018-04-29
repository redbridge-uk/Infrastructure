using Xamarin.Forms;

namespace Redbridge.Forms.Navigation
{
    public interface ICurrentPageService
    {
        Page GetCurrent();

        INavigation GetNavigation();
    }
}
