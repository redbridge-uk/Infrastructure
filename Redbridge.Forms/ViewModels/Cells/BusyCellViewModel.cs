using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public class BusyCellViewModel : TableCellViewModel
    {
		public const string CellTypeName = "busy";

        private Color _textColour = Color.Black;
        private Color _indicatorColour = Color.Black;
		private string _text;

		public BusyCellViewModel() 
        {
            Text = "Loading...";
        }

		public BusyCellViewModel(string text)
		{
			Text = text;
		}

		public override string CellType
		{
			get { return CellTypeName; }
		}

		public Color ActivityIndicatorColour
		{
			get { return _indicatorColour; }
			set
			{
				if (_indicatorColour != value)
				{
					_indicatorColour = value;
					OnPropertyChanged("ActivityIndicatorColour");
				}
			}
		}

        public Color TextColour
        {
			get { return _textColour; }
			set
			{
				if (_textColour != value)
				{
					_textColour = value;
					OnPropertyChanged("TextColour");
				}
			}
        }

		public string Text
		{
			get { return _text; }
			set
			{
				if (_text != value)
				{
					_text = value;
					OnPropertyChanged("Text");
				}
			}
		}
    }
}
