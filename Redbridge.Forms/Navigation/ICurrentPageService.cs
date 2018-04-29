using Xamarin.Forms;

namespace Redbridge.Forms.Navigation
{
    public interface ICurrentPageService
    {
        Page Current { get; }

        INavigation Navigation { get; }
    }
}
