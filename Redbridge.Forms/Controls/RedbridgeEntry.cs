using Xamarin.Forms;

namespace Redbridge.Forms.Controls
{
    public class RedbridgeEntry : Entry
    {
        public static readonly BindableProperty BorderVisibleProperty = BindableProperty.Create("BorderVisible", typeof(bool), typeof(RedbridgeEntry), true);
        public static readonly BindableProperty PlaceholderIconProperty = BindableProperty.Create("PlaceholderIcon", typeof(Icon), typeof(RedbridgeEntry), Icon.None);

        public bool BorderVisible
        {
            get { return (bool)GetValue(BorderVisibleProperty); }
            set { SetValue(BorderVisibleProperty, value); }
        }

        public Icon PlaceholderIcon
        {
            get { return (Icon)GetValue(PlaceholderIconProperty); }
            set { SetValue(PlaceholderIconProperty, value); }
        }
    }
}
