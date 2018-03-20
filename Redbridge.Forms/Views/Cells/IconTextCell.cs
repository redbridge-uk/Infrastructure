using Redbridge.Forms.ViewModels;
using Redbridge.Forms.ViewModels.Cells;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class IconTextCell : ViewCell, IView
    {
        public static readonly BindableProperty CellHeightProperty = BindableProperty.Create("CellHeight", typeof(TableCellHeight), typeof(IconTextCell), TableCellHeight.Default); 
        public static readonly BindableProperty SelectionStyleProperty = BindableProperty.Create("SelectionStyle", typeof(TableCellSelectionStyle), typeof(IconTextCell), TableCellSelectionStyle.Default);  
        public static readonly BindableProperty AccessoriesProperty = BindableProperty.Create("Accessories", typeof(CellIndicators), typeof(IconTextCell), CellIndicators.None);  
        public static readonly BindableProperty DisplayModeProperty = BindableProperty.Create("DisplayMode", typeof(IconCellViewMode), typeof(IconTextCell), IconCellViewMode.TitleDetail);  
		public static readonly BindableProperty IconCodeProperty = BindableProperty.Create("IconCode", typeof(string), typeof(IconTextCell));
		public static readonly BindableProperty IconColourProperty = BindableProperty.Create("IconColour", typeof(Color), typeof(IconTextCell), Color.Black);
		public static readonly BindableProperty TextProperty = 	BindableProperty.Create("Text", typeof(string), typeof(IconTextCell));
        public static readonly BindableProperty TextColourProperty = BindableProperty.Create("TextColour", typeof(Color), typeof(IconTextCell), Color.Black);
		public static readonly BindableProperty DetailProperty = BindableProperty.Create("Detail", typeof(string), typeof(IconTextCell));
		public static readonly BindableProperty DetailColourProperty = BindableProperty.Create("DetailColour", typeof(Color), typeof(IconTextCell), Color.Black);
        public static readonly BindableProperty BackgroundColourProperty = BindableProperty.Create("BackgroundColour", typeof(Color), typeof(IconTextCell), Color.Transparent);

        public IconCellViewMode DisplayMode
        {
            get { return (IconCellViewMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }

        public CellIndicators Accessories
        {
            get { return (CellIndicators)GetValue(AccessoriesProperty); }
            set { SetValue(AccessoriesProperty, value); }
        }

		public string IconCode
		{
			get { return (string)GetValue(IconCodeProperty); }
			set { SetValue(IconCodeProperty, value); }
		}

		public Color IconColour
		{
			get { return (Color)GetValue(IconColourProperty); }
			set { SetValue(IconColourProperty, value); }
		}

        public Color BackgroundColour
        {
            get { return (Color)GetValue(BackgroundColourProperty); }
            set { SetValue(BackgroundColourProperty, value); }
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

        public TableCellSelectionStyle SelectionStyle 
        {
            get { return (TableCellSelectionStyle)GetValue(SelectionStyleProperty); }
            set { SetValue(SelectionStyleProperty, value); }
        }

        public TableCellHeight CellHeight
        {
            get { return (TableCellHeight)GetValue(CellHeightProperty); }
            set { SetValue(CellHeightProperty, value); }
        }
    }
}
