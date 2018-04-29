using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public interface IHardwareNavigationAware
    {
        event EventHandler<Page> BackButtonPressed;
    }
}
