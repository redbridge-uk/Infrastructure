using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class CircledTextCell : ViewCell, IView
	{
        public static readonly BindableProperty AccessoriesProperty = BindableProperty.Create("Accessories", typeof(CellIndicators), typeof(CircledTextCell), CellIndicators.None);      
		public static readonly BindableProperty CircledTextProperty = BindableProperty.Create("CircledText", typeof(string), typeof(CircledTextCell));
		public static readonly BindableProperty CircledTextColourProperty = BindableProperty.Create("CircledTextColour", typeof(Color), typeof(CircledTextCell), Color.Black);
        public static readonly BindableProperty CircleBackgroundColourProperty = BindableProperty.Create("CircleBackgroundColour", typeof(Color), typeof(CircledTextCell), Color.White);
		public static readonly BindableProperty TextProperty = 	BindableProperty.Create("Text", typeof(string), typeof(CircledTextCell));
        public static readonly BindableProperty TextColourProperty = BindableProperty.Create("TextColour", typeof(Color), typeof(CircledTextCell), Color.Black);
		public static readonly BindableProperty DetailProperty = BindableProperty.Create("Detail", typeof(string), typeof(CircledTextCell));
		public static readonly BindableProperty DetailColourProperty = BindableProperty.Create("DetailColour", typeof(Color), typeof(CircledTextCell), Color.Black);

        public CellIndicators Accessories
        {
            get { return (CellIndicators)GetValue(AccessoriesProperty); }
            set { SetValue(AccessoriesProperty, value); }
        }

		public Color CircleBackgroundColour
		{
			get { return (Color)GetValue(CircleBackgroundColourProperty); }
			set { SetValue(CircleBackgroundColourProperty, value); }
		}

		public string CircledText
		{
			get { return (string)GetValue(CircledTextProperty); }
			set { SetValue(CircledTextProperty, value); }
		}

		public Color CircledTextColour
		{
			get { return (Color)GetValue(CircledTextColourProperty); }
			set { SetValue(CircledTextColourProperty, value); }
		}

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

		public string Detail
		{
			get { return (string)GetValue(DetailProperty); }
			set { SetValue(DetailProperty, value); }
		}

		public Color DetailColour
		{
			get { return (Color)GetValue(DetailColourProperty); }
			set { SetValue(DetailColourProperty, value); }
		}
    }
}
