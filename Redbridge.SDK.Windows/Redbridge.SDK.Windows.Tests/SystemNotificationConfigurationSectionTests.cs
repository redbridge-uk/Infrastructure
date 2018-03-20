using System;
using NUnit.Framework;
using Redbridge.Configuration;

namespace Redbridge.SDK.Windows.Tests
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

		[Test]
		public void GetSectionFromApplicationSettingsManagerCheckNotifiersCollection()
		{
			var provider = new WindowsApplicationSettingsRepository();
			var settings = provider.GetSection<SystemNotificationConfigurationSection>(SystemNotificationConfigurationSection.SectionName);
			Assert.AreEqual(2, settings.Notifiers.Count);
			Assert.AreEqual(typeof(SlackNotifierConfigurationElement), settings.Notifiers["User Registration Slack Channel"].GetType());
			var slack = (SlackNotifierConfigurationElement)settings.Notifiers["User Registration Slack Channel"];
		}

		[Test]
		public void GetSectionFromApplicationSettingsManagerCheckNotifiersCollectionIncludesFilters()
		{
			var provider = new WindowsApplicationSettingsRepository();
			var settings = provider.GetSection<SystemNotificationConfigurationSection>(SystemNotificationConfigurationSection.SectionName);
			Assert.AreEqual(typeof(SlackNotifierConfigurationElement), settings.Notifiers["User Registration Slack Channel"].GetType());
			var slack = (SlackNotifierConfigurationElement)settings.Notifiers["User Registration Slack Channel"];
			Assert.AreEqual(1, slack.Filters.Count);
		}
	}
}
