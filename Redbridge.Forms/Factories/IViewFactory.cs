using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface IViewFactory
	{
		Page CreatePage(object model, bool ignoreGenerics = false);
		Page CreatePageFromModel<T>();
	}
}
