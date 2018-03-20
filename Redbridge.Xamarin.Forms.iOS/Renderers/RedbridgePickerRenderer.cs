using Redbridge.Forms.Controls;
using Redbridge.Xamarin.Forms.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RedbridgeDatePicker), typeof(RedbridgePickerRenderer))]

namespace Redbridge.Xamarin.Forms.iOS
{
	public class RedbridgePickerRenderer : DatePickerRenderer
	{
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.TextAlignment = UITextAlignment.Center;
			}
        }
	}
}
