using System;
using Xamarin.Forms;

namespace Redbridge.Forms.ViewModels.Cells
{

    public class IconTextCellViewModel : TableCellViewModel
    {
        private Icon _iconCode;
        public const string CellTypeName = "icontext";
        private string _text;
        private string _detail;
        private Color _textColour = Color.Black;
        private Color _detailColour = Color.Black;
        private Color _iconColour = Color.Black;
        private IconCellViewMode _viewMode = IconCellViewMode.TitleDetail;

        public IconTextCellViewModel()
        {
            if (RedbridgeThemeManager.HasTheme)
            {
                TextColour = RedbridgeThemeManager.Current.TableCellTextColour;
                DetailColour = RedbridgeThemeManager.Current.TableCellDetailColour;
            }
        }

        public IconTextCellViewModel(string text) : this()
        {
            Text = text;
            DisplayMode = IconCellViewMode.TitleOnly;

            if (RedbridgeThemeManager.HasTheme)
            {
                TextColour = RedbridgeThemeManager.Current.TableCellTextColour;
                DetailColour = RedbridgeThemeManager.Current.TableCellDetailColour;
                IconColour = RedbridgeThemeManager.Current.IconColour;
            }
        }

        public IconTextCellViewModel(Icon icon, string text, CellIndicators indicators = CellIndicators.None) : base(indicators)
        {
            IconCode = icon;
            Text = text;
            DisplayMode = IconCellViewMode.TitleOnly;

            if (RedbridgeThemeManager.HasTheme)
            {
                TextColour = RedbridgeThemeManager.Current.TableCellTextColour;
                DetailColour = RedbridgeThemeManager.Current.TableCellDetailColour;
                IconColour = RedbridgeThemeManager.Current.IconColour;
            }
        }

        public IconTextCellViewModel(Icon icon, string text, string detail = "", CellIndicators indicators = CellIndicators.None) : base(indicators)
        {
            IconCode = icon;
            Text = text;
            Detail = detail;
            DisplayMode = string.IsNullOrWhiteSpace(detail) ? IconCellViewMode.TitleOnly : IconCellViewMode.TitleDetail;

            if (RedbridgeThemeManager.HasTheme)
            {
                TextColour = RedbridgeThemeManager.Current.TableCellTextColour;
                DetailColour = RedbridgeThemeManager.Current.TableCellDetailColour;
                IconColour = RedbridgeThemeManager.Current.IconColour;
            }
        }

        public IconCellViewMode DisplayMode
        {
            get { return _viewMode; }
            set
            {
                if (_viewMode != value)
                {
                    _viewMode = value;
                    OnPropertyChanged("DisplayMode");
                }
            }
        }

        public Icon IconCode
        {
            get { return _iconCode; }
            set
            {
                if (_iconCode != value)
                {
                    _iconCode = value;
                    OnPropertyChanged("IconCode");
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

        public Color DetailColour
        {
            get { return _detailColour; }
            set
            {
                if (_detailColour != value)
                {
                    _detailColour = value;
                    OnPropertyChanged("DetailColour");
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
    }
}
