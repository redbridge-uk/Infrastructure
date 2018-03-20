using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class RedbridgeTheme : ViewModel, IDisplayText
	{
		static readonly RedbridgeTheme _whiteTheme;
		static readonly RedbridgeTheme _blackTheme;
		static readonly RedbridgeTheme _orangeTheme;
		static readonly RedbridgeTheme _blueTheme;

		static RedbridgeTheme()
		{
            _whiteTheme = new RedbridgeTheme() { Name = "Default", IconColour = Color.Black,  TableCellTextColour = Color.Black, TableCellDetailColour = Color.Black, NavigationBarColour = Color.White, NavigationTextColour = Color.Black, TintColour = Color.White, BusyIndicatorColour = Color.Black };
            _blackTheme = new RedbridgeTheme() { Name = "Black", IconColour = Color.White, TableCellTextColour = Color.White, TableCellDetailColour = Color.White, NavigationBarColour = Color.Black, NavigationTextColour = Color.White, TintColour = Color.Black, BusyIndicatorColour = Color.Black  };
            _orangeTheme = new RedbridgeTheme() { Name = "Orange", IconColour = Color.White,  TableCellTextColour = Color.Orange, TableCellDetailColour = Color.Black, NavigationBarColour = Color.Orange, NavigationTextColour = Color.White, TintColour = Color.Orange, BusyIndicatorColour = Color.Orange };
            _blueTheme = new RedbridgeTheme() { Name = "Blue", IconColour = Color.White, TableCellTextColour = Color.Black, TableCellDetailColour = Color.Black, NavigationBarColour = Color.FromRgb(46, 112, 185), NavigationTextColour = Color.White, TintColour = Color.FromRgb(46, 112, 185), BusyIndicatorColour = Color.FromRgb(46, 112, 185) };
		}

		public static RedbridgeTheme Default
		{
			get { return _whiteTheme; }
		}

		public static RedbridgeTheme White
		{
			get { return _whiteTheme; }
		}

		public static RedbridgeTheme Black
		{
			get { return _blackTheme; }
		}

		public static RedbridgeTheme Blue
		{
			get { return _blueTheme; }
		}

		public static RedbridgeTheme Orange
		{
			get { return _orangeTheme; }
		}

		private Color _busyIndicatorColour;
		private Color _tintColour;
		private Color _navigationBarColour;
		private Color _navigationTextColour;
        private Color _tableCellTextColour;
        private Color _tableCellDetailColour;
        private Color _tableBackgroundColour;
        private Color _iconColour = Color.Black;
        private double _busyOverlayOpacity = 0.3D;

		public string Name { get; set; }

		public Color BusyIndicatorColour
		{
			get { return _busyIndicatorColour; }
			set
			{
				if (_busyIndicatorColour != value)
				{
					_busyIndicatorColour = value;
					OnPropertyChanged("BusyIndicatorColour");
				}
			}
		}

        public double BusyOverlayOpacity
        {
			get { return _busyOverlayOpacity; }
			set
			{
                if (!_busyOverlayOpacity.Equals(value) )
				{
					_busyOverlayOpacity = value;
					OnPropertyChanged("BusyOverlayOpacity");
				}
			}
        }


		public Color TintColour 
		{
			get { return _tintColour; }
			set
			{
				if (_tintColour != value)
				{
					_tintColour = value;
					OnPropertyChanged("TintColour");
				}
			}
		}

		public Color NavigationBarColour
		{
			get { return _navigationBarColour; }
			set
			{
				if (_navigationBarColour != value)
				{
					_navigationBarColour = value;
					OnPropertyChanged("NavigationBarColour");
				}
			}
		}

		public Color NavigationTextColour
		{
			get { return _navigationTextColour; }
			set
			{
				if (_navigationTextColour != value)
				{
					_navigationTextColour = value;
					OnPropertyChanged("NavigationTextColour");
				}
			}
		}

        public Color TableBackgroundColour
        {
            get { return _tableBackgroundColour; }
            set
            {
                if (_tableBackgroundColour != value)
                {
                    _tableBackgroundColour = value;
                    OnPropertyChanged("TableBackgroundColour");
                }
            }
        }

        public Color TableCellTextColour
        {
            get { return _tableCellTextColour; }
            set
            {
                if (_tableCellTextColour != value)
                {
                    _tableCellTextColour = value;
                    OnPropertyChanged("TableCellTextColour");
                }
            }
        }

        public Color TableCellDetailColour
        {
            get { return _tableCellDetailColour; }
            set
            {
                if (_tableCellDetailColour != value)
                {
                    _tableCellDetailColour = value;
                    OnPropertyChanged("TableCellDetailColour");
                }
            }
        }

        public Color IconColour
        {
            get { return _iconColour; }
            set
            {
                if (_iconColour != value)
                {
                    _iconColour = value;
                    OnPropertyChanged("IconColour");
                }
            }
        }

        string IDisplayText.DisplayText => Name;
    }

	public static class RedbridgeThemeManager
	{
		static BehaviorSubject<RedbridgeTheme> _theme;
		public static IObservable<RedbridgeTheme> Theme => _theme;
		public static RedbridgeTheme Current 
		{
			get { return _theme.Value; }
		}

		public static bool HasTheme => Current != null;

		static RedbridgeThemeManager()
		{
			_theme = new BehaviorSubject<RedbridgeTheme>(null);	
		}

		public static void SetTheme(RedbridgeTheme theme)
		{
			_theme.OnNext(theme);
		}

		public static IEnumerable<RedbridgeTheme> All
		{
			get 
			{
				yield return RedbridgeTheme.Black;
				yield return RedbridgeTheme.Blue;
				yield return RedbridgeTheme.Orange;
			}
		}
	}
}
