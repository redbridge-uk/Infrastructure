using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Redbridge.Diagnostics.ApplicationInsights
{

    public class ApplicationInsightsLogger<T> : ILogger, IEventTracker
    {
        private readonly TelemetryClient _client;
        private readonly LoggingSettings _settings;
        
		public ApplicationInsightsLogger(TelemetryClient client) : this(client, LoggingSettings.Default) { }

		public ApplicationInsightsLogger(TelemetryClient client, LoggingSettings settings)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        private void Flush()
        {
            if (_settings.AutoFlush)
            {
                _client.Flush();
            }
        }

		public void WriteException(Exception exception)
		{
            if ( _settings.DuplicateToConsole){ Console.WriteLine($"EXCEPTION: {exception.Message} [{typeof(T).Name}]");}
            _client.TrackException(exception);
            Flush();
        }

		public void WriteError(string format)
		{
            if (_settings.DuplicateToConsole)
            {
                Console.WriteLine($"ERROR: {format} [{typeof(T).Name}]");
            }

            _client.TrackTrace(format, SeverityLevel.Error);
            Flush();
        }

        public void WriteInfo(string format)
		{
            if (_settings.DuplicateToConsole)
            {
                Console.WriteLine($"INFO: {format} [{typeof(T).Name}]");
            }

            _client.TrackTrace(format, SeverityLevel.Information);
            Flush();
        }

        public void WriteDebug(string format)
		{
            if (_settings.DuplicateToConsole)
            {
                Console.WriteLine($"DEBUG: {format} [{typeof(T).Name}]");
            }

            _client.TrackTrace(format, SeverityLevel.Verbose);
            Flush();
        }

		public void WriteWarning(string format)
		{
            if (_settings.DuplicateToConsole)
            {
                Console.WriteLine($"WARNING: {format} [{typeof(T).Name}]");
            }

            _client.TrackTrace(format, SeverityLevel.Warning);
            Flush();
        }

        public void WriteEvent (string eventName, IDictionary<string, string> properties = null)
        {
            if (_settings.DuplicateToConsole)
            {
                Console.WriteLine($"*** Tracked event {eventName}  [{typeof(T).Name}] ***");
            }

            _client.TrackEvent(eventName, properties);
            Flush();
        }
	}
}
