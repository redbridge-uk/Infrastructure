using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class ImageCellViewModel : TextCellViewModel
	{
		private string _imageSource;
		public new const string CellTypeName = "image";

		public ImageCellViewModel() { }

		public ImageCellViewModel(string text, string imageSource, CellIndicators indicators = CellIndicators.None) 
			: base(text, indicators)
		{
			ImageSource = imageSource;
		}

		public ImageCellViewModel(string text, string detail, string imageSource, CellIndicators indicators = CellIndicators.None) 
			: base(text, detail, indicators) 
		{
			ImageSource = imageSource;
		}

		public override string CellType
		{
			get { return CellTypeName; }
		}

		public string ImageSource 
		{
			get { return _imageSource; }
			set 
			{
				if (_imageSource != value)
				{
					_imageSource = value;
					OnPropertyChanged("ImageSource");
				}
			}
		}
	}
}
