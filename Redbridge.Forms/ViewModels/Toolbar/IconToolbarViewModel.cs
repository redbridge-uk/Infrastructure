using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class IconItemToolbarViewModel : ToolbarItemViewModel
	{
		public IconItemToolbarViewModel() { }

		public IconItemToolbarViewModel(FileImageSource icon)
		{
			Icon = icon;
		}
	}
}
