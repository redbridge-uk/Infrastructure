using System.Collections.ObjectModel;
using Redbridge.Linq;
using Redbridge.SDK;

namespace Redbridge.Forms
{

	public abstract class ItemPickerTileViewModel<T> : NavigationPageViewModel
	{
		private PickerBehavior _behaviour;

		public ItemPickerTileViewModel(IItemSource<T> itemSource, INavigationService navigationService, ISchedulerService scheduler) : base(navigationService, scheduler)
		{
			Tiles = new ObservableCollection<TileViewModel>();
			var items = itemSource.GetAll();
			items.ForEach(i => Tiles.Add(CreateTile(i)));
		}

		protected abstract TileViewModel CreateTile(T data);

		public ObservableCollection<TileViewModel> Tiles { get; private set; }

		public PickerBehavior PickerBehavior
		{
			get { return _behaviour; }
			set { _behaviour = value; }
		}
	}
}
