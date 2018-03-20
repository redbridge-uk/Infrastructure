using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public interface ITableCellViewModel : INotifyPropertyChanged, IComparable<ITableCellViewModel>
    {
        CellIndicators Accessories { get; set; }
        ICommand Command { get; set; }
        object CommandParameter { get; set; }
        void Enable();
        void Disable();
        bool IsEnabled { get; }
        bool IsVisible { get; }
        string CellType { get; }
        IObservable<string> CellTypeObservable { get; }
        void CellClicked();
        IObservable<bool> Visibility { get; }
        bool AllowCellSelection { get; }
        bool EditMode { get; }
        void BeginEdit();
        void EndEdit();
        TableCellHeight CellHeight { get; }
        Color BackgroundColour { get; }
    }
}
