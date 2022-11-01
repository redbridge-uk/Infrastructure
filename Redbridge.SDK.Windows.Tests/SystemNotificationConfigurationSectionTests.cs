using NUnit.Framework;
using Redbridge.Configuration;

namespace Redbridge.Windows.Tests
{
	[TestFixture]
	public class SystemNotificationConfigurationSectionTests
	{
		[Test]
		public void GetSectionFromApplicationSettingsManagerCheckEnvironmentName()
		{
			var provider = new WindowsApplicationSettingsRepository();
			var settings = provider.GetSection<SystemNotificationConfigurationSection>(SystemNotificationConfigurationSection.SectionName);
			Assert.IsNotNull(settings);
			Assert.AreEqual("Development", settings.Environment);
		}
    }
}
