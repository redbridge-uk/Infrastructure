using System.Threading.Tasks;
using Redbridge.Notifications;

namespace Redbridge.ServiceModel
{
	public interface INotificationService
	{
		Task NotifyAsync(NotificationMessage message);
	}
}
