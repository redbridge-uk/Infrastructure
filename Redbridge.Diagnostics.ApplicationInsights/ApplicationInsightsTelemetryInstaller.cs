using System;
using System.Configuration;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Redbridge.Configuration;

namespace Redbridge.Diagnostics.ApplicationInsights
{
	public class ApplicationInsightsContextInitializer : ITelemetryInitializer
	{
        private readonly IApplicationSettingsRepository _settings;

        public ApplicationInsightsContextInitializer(IApplicationSettingsRepository settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Initialize(ITelemetry telemetry)
		{
			telemetry.Context.GlobalProperties["tags"] = _settings.GetStringValue("ApplicationInsights:Tags");
		}
	}

	public static class ApplicationInsightsTelemetryInstaller
	{
		public static void Install(IApplicationSettingsRepository settings)
		{
			TelemetryConfiguration.Active.InstrumentationKey = settings.GetStringValue("ApplicationInsights:Key");
		}
	}
}
