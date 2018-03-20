using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	// Placeholder, custom renderers required at each level.
	public class DatePickerCell : ViewCell, IView 
    {
        public static readonly BindableProperty DateProperty = BindableProperty.Create("Date", typeof(DateTime), typeof(DatePickerCell), DateTime.Now, BindingMode.TwoWay, null, OnPropertyChanged);

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            return;
        }

        public DateTime Date
		{
			get { return (DateTime)GetValue(DateProperty); }
			set 
            {
                SetValue(DateProperty, value); 
            }
		}
    }
}
