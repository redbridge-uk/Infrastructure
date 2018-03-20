using Redbridge.DependencyInjection;

namespace Redbridge.Forms
{
	public static class ContainerExtensions
	{
		public static void RegisterNavigationView<TViewModel>(this IContainer container)
			where TViewModel : INavigationControllerViewModel
		{
			container.RegisterType<IView, NavigationControllerPage>(typeof(TViewModel).Name);
		}

		public static void RegisterTableView<TViewModel>(this IContainer container)
			where TViewModel : ITableViewModel
		{
			container.RegisterType<IView, TableViewPage>(typeof(TViewModel).Name);
		}

		public static void RegisterView<TView, TViewModel>(this IContainer container)
			where TView : IView
			where TViewModel : IViewModel
		{
			container.RegisterType<IView, TView>(typeof(TViewModel).Name);
		}
	}
}
