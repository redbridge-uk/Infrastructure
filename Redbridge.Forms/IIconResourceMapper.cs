using Xamarin.Forms;

namespace Redbridge.Forms
{

    public interface IIconResourceMapper : IValueConverter
    {
        string MapResource(Icon icon, Color colour, IconSize size = IconSize.Small);
    }
}
