using System;
using System.Net.Http;
using System.Threading.Tasks;
using Redbridge.Configuration;
using Redbridge.ServiceModel;
using Redbridge.Web.Messaging;

namespace Redbridge.Notifications
{
	public class NotificationService : INotificationService
	{
		private readonly IApplicationSettingsRepository _settingsRepository;
        private readonly IHttpClientFactory _clientFactory;

        public NotificationService(IApplicationSettingsRepository settingsRepository, IHttpClientFactory clientFactory)
        {
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
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
			await settings.NotifyAllAsync(message, _clientFactory);
		}
	}
}
