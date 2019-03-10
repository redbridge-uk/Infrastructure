using System;
using System.Configuration;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Redbridge.Diagnostics.ApplicationInsights
{
	public class ApplicationInsightsContextInitializer : ITelemetryInitializer
	{
		public void Initialize(ITelemetry telemetry)
		{
			telemetry.Context.GlobalProperties["tags"] = ConfigurationManager.AppSettings["ApplicationInsights:Tags"];
		}
	}

	public static class ApplicationInsightsTelemetryInstaller
	{
		public static void Install()
		{
			TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["ApplicationInsights:Key"];
			TelemetryConfiguration.Active.DisableTelemetry = !bool.Parse(ConfigurationManager.AppSettings["ApplicationInsights:Enabled"]);
		}
	}
}
