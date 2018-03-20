using System;
using Xamarin.Forms;

namespace Redbridge.Forms.ViewModels.Cells
{
    public class CircledTextCellViewModel : TableCellViewModel
    {
        private string _circledText;
        private Color _circledTextColour;
        private Color _circledTextBackgroundColour; 
		public const string CellTypeName = "circledtext";
		private string _text;
		private string _detail;

		public CircledTextCellViewModel() { }

		public CircledTextCellViewModel(string text)
		{
			Text = text;
		}

		public CircledTextCellViewModel(string text, CellIndicators indicators = CellIndicators.None) : base(indicators)
        {
			Text = text;
		}

		public CircledTextCellViewModel(string text, string detail = "", CellIndicators indicators = CellIndicators.None) : base(indicators)
        {
			Text = text;
			Detail = detail;
		}

		public Color CircledTextColour
		{
			get { return _circledTextColour; }
			set
			{
				if (_circledTextColour != value)
				{
					_circledTextColour = value;
					OnPropertyChanged("CircledTextColour");
				}
			}
		}

		public Color CircleBackgroundColour
		{
			get { return _circledTextBackgroundColour; }
			set
			{
				if (_circledTextBackgroundColour != value)
				{
					_circledTextBackgroundColour = value;
					OnPropertyChanged("CircleBackgroundColour");
				}
			}
		}

		public string CircledText
        {
            get { return _circledText; }
            set
            {
                if ( _circledText != value )
                {
                    _circledText = value;
                    OnPropertyChanged("CircledText");
                }
            }
        }

		public override string CellType
		{
			get { return CellTypeName; }
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

		public string Detail
		{
			get { return _detail; }
			set
			{
				if (_detail != value)
				{
					_detail = value;
					OnPropertyChanged("Detail");
				}
			}
		}
    }
}
