using Redbridge.Forms;
using UIKit;

namespace Redbridge.Xamarin.Forms.iOS
{
	public static class ClearButtonModeConverter
	{
		public static UITextFieldViewMode ToiOSClearButtonMode(this TextClearButtonMode mode)
		{
			switch (mode)
			{
				case TextClearButtonMode.Always:
					return UITextFieldViewMode.Always;

				case TextClearButtonMode.WhilstEditing:
					return UITextFieldViewMode.WhileEditing;
				
				case TextClearButtonMode.UnlessEditing:
					return UITextFieldViewMode.UnlessEditing;

				default:
					return UITextFieldViewMode.Never;
			}
		}
	}
}
