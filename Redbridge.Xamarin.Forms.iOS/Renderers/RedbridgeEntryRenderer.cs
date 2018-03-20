using System;
using Redbridge.Forms;
using Redbridge.Forms.Controls;
using Redbridge.Xamarin.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RedbridgeEntry), typeof(RedbridgeEntryRenderer))]


namespace Redbridge.Xamarin.Forms.iOS.Renderers
{
    public class RedbridgeEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var redbridgeEntry = e.NewElement as RedbridgeEntry;
                if (redbridgeEntry != null)
                {
                    // do whatever you want to the UITextField here!
                    Control.BorderStyle = redbridgeEntry.BorderVisible ? UITextBorderStyle.Line : UITextBorderStyle.None;

                    var mapper = new DefaultIconMapper();
                    var code = mapper.MapResource(redbridgeEntry.PlaceholderIcon, Color.Black, IconSize.Small);
                    UIImageView iconImage;
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        iconImage = new UIImageView(UIImage.FromFile(code));
                        Control.LeftView = iconImage;
                        Control.LeftViewMode = UITextFieldViewMode.Always;
                    }
                }
            }
        }
    }
}
