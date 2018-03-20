using System;

namespace Redbridge.Forms
{
    // This is the placeholder cell for creating the appropriate device specific
    // type of inline date picker, although as I understand it, Android devices don't do that.
    // They show the picker as a separate popup thing.
	public class DatePickerCellViewModel : TableCellViewModel
	{
        private DateTimeCellViewModel _parentCell;

		public const string CellTypeName = "datepicker";

        public DatePickerCellViewModel ()
        {
            AllowCellSelection = true;
            CellHeight = TableCellHeight.Custom;
        }

        public void SetParentHost (DateTimeCellViewModel host)
        {
            if (host == null) throw new System.ArgumentNullException(nameof(host));

            if ( _parentCell != null && _parentCell != host && _parentCell.PickerVisible )
            {   
                // Hide the previous parent picker.
                _parentCell.ToggglePickerVisibility();
            }

            _parentCell = host;
            if (_parentCell.Value.HasValue)
            {
                Date = _parentCell.Value.Value;
            }
            else
                Date = DateTime.Now;
        }

        public DateTimeCellViewModel Parent => _parentCell;

        public DateTime Date
        {
            get { return _parentCell.Value.HasValue ? _parentCell.Value.Value : DateTime.Now; }
            set
            {
                _parentCell.Value = value;
                OnPropertyChanged("Date");
            }
        }


		public override string CellType
		{
			get { return CellTypeName; }
		}

        internal void ClearHost()
        {
            _parentCell.ToggglePickerVisibility();
            _parentCell = null;
        }
    }
}
