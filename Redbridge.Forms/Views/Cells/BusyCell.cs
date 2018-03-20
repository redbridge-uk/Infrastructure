using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class BusyCell : ViewCell, IView 
    {
		public static readonly BindableProperty TextProperty = 	BindableProperty.Create("Text", typeof(string), typeof(BusyCell), "Loading...");
        public static readonly BindableProperty TextColourProperty = BindableProperty.Create("TextColour", typeof(Color), typeof(BusyCell), Color.Black);

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public Color TextColour
		{
			get { return (Color)GetValue(TextColourProperty); }
			set { SetValue(TextColourProperty, value); }
		}
    }
}
