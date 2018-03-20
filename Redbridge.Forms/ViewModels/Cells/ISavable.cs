using Redbridge.SDK;
using Redbridge.Validation;

namespace Redbridge.Forms
{
	public interface ISavable
	{
		ValidationResultCollection TrySave();
	}

}
