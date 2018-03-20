using System;
using System.Diagnostics;

namespace Redbridge.SDK
{
	[DebuggerDisplay("Category Id: {Id}, Name: {Name}")]
	public class Category : NamedUniqueData<int>
	{
		public const string DefaultCategoryName = "Default";
		public const string NotificationCategoryName = "Notifications";
		public const string ProgressCategoryName = "Progress";

		public static Category Default => new Category(1, DefaultCategoryName);

		public static Category Notifications => new Category(2, NotificationCategoryName);

		public static Category Progress => new Category(3, ProgressCategoryName);

		public Category(int id, string name) : base(id, name) { }

		public override string ToString()
		{
			return $"Category: {Name}";
		}
	}
}
