using System;
using System.Reactive.Subjects;
using Redbridge.SDK;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class BusyOperation : IDisposable
	{
		readonly IBusyPageViewModel _parentViewModel;

		public BusyOperation(IBusyPageViewModel parentViewModel)
		{
			if (parentViewModel == null)throw new ArgumentNullException(nameof(parentViewModel));
			_parentViewModel = parentViewModel;
			_parentViewModel.IsBusy = true;
		}

		public void Dispose()
		{
			_parentViewModel.IsBusy = false;
		}
	}
	
}
