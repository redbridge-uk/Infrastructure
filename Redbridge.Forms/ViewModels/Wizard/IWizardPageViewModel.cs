using System;
namespace Redbridge.Forms
{
	public interface IWizardPageViewModel : IViewModel
	{
		string Title { get; }
		bool CanFinish { get; }
		bool CanCancel { get; }
		bool CanGoForwards { get; }
		bool CanGoBackwards { get; }
	}
}
