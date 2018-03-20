using System;
using System.ComponentModel;

namespace Redbridge.Forms
{
	public interface IViewModel : INotifyPropertyChanged
	{
		Guid InstanceId { get; }
		void OnDisappearing();
		void OnAppearing();
    }
}
