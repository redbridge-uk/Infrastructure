using System;
using System.Threading.Tasks;

namespace Redbridge.Forms
{
	public interface IAlertController
	{
		Task<AlertResponse> ShowAlert(AlertViewModel alert);
	}
}
