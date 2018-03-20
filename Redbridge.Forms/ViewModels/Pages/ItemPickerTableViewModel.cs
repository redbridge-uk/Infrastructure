using System;
using Redbridge.Linq;
using Redbridge.SDK;

namespace Redbridge.Forms
{
	public abstract class ItemPickerTableViewModel<T> : TableViewModel
        where T: IDisplayText
	{
		private TableSectionViewModel _defaultSection;
		private PickerBehavior _behavior = PickerBehavior.Single;

		public ItemPickerTableViewModel(IItemSource<T> itemSource, INavigationService navigationService, ISchedulerService scheduler) 
			: base(navigationService, scheduler) 
		{
			_defaultSection = AddSection();

			var sortedList = itemSource.GetAll();
			sortedList.ForEach(s => 
			{
				var section = GetItemSection(s);
				if (section == null) throw new NotSupportedException("Unable to return a section for the supplied item.");

				var itemView = CreateItemViewModel(s);
				section.AddCell(itemView);
			});
		}

		protected virtual TableSectionViewModel GetItemSection(T item)
		{
			return _defaultSection;
		}

		public virtual PickerBehavior PickerBehavior 
		{
			get { return _behavior; }
			set { _behavior = value; }
		}

		protected virtual ICellFactory CreateCellFactory()
		{
			return new TextCellFactory();
		}

		protected abstract ITableCellViewModel CreateItemViewModel (T item);
	}
}
