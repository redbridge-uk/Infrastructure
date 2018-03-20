using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Redbridge.Diagnostics.ApplicationInsights
{
	public class ApplicationInsightsLogger : ILogger
	{
		private readonly TelemetryClient _telemetryClient;

		public ApplicationInsightsLogger()
		{
			_telemetryClient = new TelemetryClient();
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
	}
}
