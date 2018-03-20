using System;
using System.Collections.ObjectModel;

namespace Redbridge.Forms
{
	public class ToolbarViewModel : ViewModel
	{
		public ToolbarViewModel()
		{
			ToolbarItems = new ObservableCollection<IToolbarItemViewModel>();
		}

        public T Add<T>(T item) where T : IToolbarItemViewModel
        {
            ToolbarItems.Add(item);
            return item;
        }

		public IToolbarItemViewModel Add (IToolbarItemViewModel item)
		{
			if (item == null) throw new ArgumentNullException(nameof(item));
			ToolbarItems.Add(item);
            return item;
		}

		public ObservableCollection<IToolbarItemViewModel> ToolbarItems { get; }
	}
}
