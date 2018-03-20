using System.Collections.Generic;
using System.Linq;
using Redbridge.Forms;
using Redbridge.Xamarin.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(RedbridgeNavigationPageRenderer))]

namespace Redbridge.Xamarin.Forms.iOS.Renderers
{

    public class RedbridgeNavigationPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);


            var contentPage = this.Element as ContentPage;
            if (contentPage == null) return;
            var pageModel = contentPage.BindingContext as IPageViewModel;

            var itemsInfo = contentPage.ToolbarItems;
            if (NavigationController == null) return;

            var navigationItem = this.NavigationController.TopViewController.NavigationItem;
            var leftNativeButtons = (navigationItem.LeftBarButtonItems ?? new UIBarButtonItem[] { }).ToList();
            var rightNativeButtons = (navigationItem.RightBarButtonItems ?? new UIBarButtonItem[] { }).ToList();
            var rightButtonArray = rightNativeButtons.ToArray();

            rightButtonArray.ForEach(nativeItem =>
            {
                var info = GetButtonInfo(itemsInfo, nativeItem.Title);

                if (info == null || info.Priority != 0)
                {
                    if (info.Priority == 1)nativeItem.Style = UIBarButtonItemStyle.Done;
                    return;
                }

                rightNativeButtons.Remove(nativeItem);
                leftNativeButtons.Add(nativeItem);
            });

            navigationItem.RightBarButtonItems = rightNativeButtons.ToArray();
            navigationItem.LeftBarButtonItems = leftNativeButtons.ToArray();

            // Uncomment the next line for the whole thing to blur and stop working.... sigh
            // NavigationController.ToolbarHidden = false;

            //var refreshButton = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (s, e) =>{});
            //var pauseButton = new UIBarButtonItem(UIBarButtonSystemItem.Pause, (s, e) =>{});
            //var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) { Width = 50 };
            //SetToolbarItems(new UIBarButtonItem[] {refreshButton, spacer, pauseButton}, false);
        }

        private ToolbarItem GetButtonInfo(IList<ToolbarItem> items, string name)
        {
            if (string.IsNullOrEmpty(name) || items == null)
                return null;

            return items.ToList().FirstOrDefault(itemData => name.Equals(itemData.Text));
        }
    }
}
