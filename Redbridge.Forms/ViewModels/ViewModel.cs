using System;
using System.ComponentModel;

namespace Redbridge.Forms
{
	public abstract class ViewModel : INotifyPropertyChanged, IViewModel
	{
		private readonly Guid _id = Guid.NewGuid();

		public Guid InstanceId => _id;

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnAppearing()
		{
			OnViewAppearing();
		}

		protected virtual void OnViewAppearing() {}

		public void OnDisappearing()
		{
			OnViewDisappearing();
		}

		protected virtual void OnViewDisappearing() { }

		public void OnPropertyChanged(string propertyName)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}
