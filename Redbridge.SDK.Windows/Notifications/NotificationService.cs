using System;
using System.Threading.Tasks;
using Redbridge.Configuration;
using Redbridge.ServiceModel;

namespace Redbridge.Notifications
{
	public class NotificationService : INotificationService
	{
		private readonly IApplicationSettingsRepository _settingsRepository;

		public NotificationService(IApplicationSettingsRepository settingsRepository)
		{
			if (settingsRepository == null) throw new ArgumentNullException(nameof(settingsRepository));
			_settingsRepository = settingsRepository;
		}

		public async Task NotifyAsync(NotificationMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			// We need to cycle through the system configuration for the message and ascertain who it is intended for.
			if (message.IsBroadcastMessage)
				await OnBroadcastMessageAsync(message);
			else
				await Task.FromResult(true);
		}

		private async Task OnBroadcastMessageAsync(NotificationMessage message)
		{
			var settings = _settingsRepository.GetSection<SystemNotificationConfigurationSection>(SystemNotificationConfigurationSection.SectionName);
			await settings.NotifyAllAsync(message);
		}
	}
}
