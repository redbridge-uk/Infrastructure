using System.Threading.Tasks;
using Redbridge.SDK;

namespace Redbridge.ServiceModel
{
	public interface INotificationService
	{
		Task NotifyAsync(NotificationMessage message);
	}
}
