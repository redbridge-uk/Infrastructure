using System;
using System.Linq;
using UIKit;

namespace Redbridge.Xamarin.Forms.iOS
{
	public static class RootViewControllerFinder
	{
		public static UIViewController GetCurrent()
		{
			return UIApplication.SharedApplication.Windows.OrderByDescending(w => w.WindowLevel).First().RootViewController;
		}
	}
}
