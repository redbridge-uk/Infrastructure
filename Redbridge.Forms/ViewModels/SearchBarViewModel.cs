using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class SearchBarViewModel : ViewModel
	{
		private string _placeholderText;
		private string _searchText;
		private ICommand _searchCommand;
		private Color _cancelButtonColour;
		private Color _placeholderTextColour;

		public ICommand SearchCommand
		{
			get { return _searchCommand; }
			set { _searchCommand = value; }
		}

		public Color CancelButtonColour
		{
			get { return _cancelButtonColour; }
			set
			{
				if (_cancelButtonColour != value)
				{
					_cancelButtonColour = value;
					OnPropertyChanged("CancelButtonColour");
				}
			}
		}

		public string SearchText
		{
			get { return _searchText; }
			set
			{
				if (_searchText != value)
				{
					_searchText = value;
					OnPropertyChanged("SearchText");
				}
			}
		}

		public string PlaceholderText
		{ 
			get { return _placeholderText; }
			set
			{
				if (_placeholderText != value)
				{
					_placeholderText = value;
					OnPropertyChanged("PlaceholderText");
				}
			}
		}

		public Color PlaceholderTextColour
		{
			get { return _placeholderTextColour; }
			set
			{
				if (_placeholderTextColour != value)
				{
					_placeholderTextColour = value;
					OnPropertyChanged("PlaceholderTextColour");
				}
			}
		}
	}
}
