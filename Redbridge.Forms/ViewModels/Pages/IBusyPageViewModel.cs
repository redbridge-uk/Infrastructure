using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface IBusyPageViewModel : IPageViewModel
	{
		IObservable<bool> Busy { get; }
		bool IsBusy { get; set; }
		Color BusyIndicatorColour { get; } 
	}
}
