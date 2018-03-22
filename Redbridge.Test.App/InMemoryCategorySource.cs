using System;
using System.Linq;
using Redbridge.Forms;
using Redbridge.SDK;

namespace TesterApp
{
	public class InMemoryThemeSource : IItemSource<RedbridgeTheme>, IFilteredItemSource<RedbridgeTheme>
	{
		public RedbridgeTheme[] GetAll()
		{
			return RedbridgeThemeManager.All.ToArray();
		}

		public RedbridgeTheme[] GetFiltered(Func<RedbridgeTheme, bool> filter)
		{
			var all = GetAll();
			var filtered = all.Where(i => filter(i)).ToArray();
			return filtered;
		}
	}

	public class InMemoryCategorySource : IItemSource<CategoryData>, IFilteredItemSource<CategoryData>
	{
		public CategoryData[] GetAll()
		{
			return new CategoryData[]
			{
				new CategoryData() { Name = "Gas", ImageSource = "logo32.jpg" },
				new CategoryData() { Name = "Water", ImageSource = "logo32" },
				new CategoryData() { Name = "Electricity", ImageSource = "logo32.png" },
				new CategoryData() { Name = "Communications", ImageSource = "automation.png" },
				new CategoryData() { Name = "Maintenance", ImageSource = "logo32.png" },
				new CategoryData() { Name = "Insurance", ImageSource = "logo32.png" }
			};
		}

		public CategoryData[] GetFiltered(Func<CategoryData, bool> filter)
		{
			var all = GetAll();
			var filtered = all.Where(i => filter(i)).ToArray();
			return filtered;
		}
	}
}
