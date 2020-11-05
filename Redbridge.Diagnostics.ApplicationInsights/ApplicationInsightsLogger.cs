using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Redbridge.Diagnostics.ApplicationInsights
{
    public class ApplicationInsightsLogger : ILogger, IEventTracker
	{
		private readonly TelemetryClient _telemetryClient;

		public ApplicationInsightsLogger()
		{
			_telemetryClient = new TelemetryClient(TelemetryConfiguration.CreateDefault());
		}

		public void WriteException(Exception exception)
		{
            Console.WriteLine($"EXCEPTION: {exception.Message}");
			_telemetryClient.TrackException(exception);
		}

		public void WriteError(string format)
		{
            Console.WriteLine($"ERROR: {format}");
			_telemetryClient.TrackTrace(format, SeverityLevel.Error);
		}

		public void WriteInfo(string format)
		{
			Console.WriteLine($"INFO: {format}");
			_telemetryClient.TrackTrace(format, SeverityLevel.Information);
		}

		public void WriteDebug(string format)
		{
			Console.WriteLine($"DEBUG: {format}");
			_telemetryClient.TrackTrace(format, SeverityLevel.Verbose);
		}

		public void WriteWarning(string format)
		{
            Console.WriteLine($"WARNING: {format}");
			_telemetryClient.TrackTrace(format, SeverityLevel.Warning);
		}

        public void WriteEvent (string eventName, IDictionary<string, string> properties = null)
        {
            _telemetryClient.TrackEvent(eventName, properties);
        }
	}
}
