using System;
using System.Diagnostics;

namespace Redbridge.Diagnostics
{
	public class StopwatchLogger : IDisposable
	{
		private readonly ILogger _logger;
		private readonly string _content;
		private readonly Stopwatch _stopwatch;

		public StopwatchLogger(ILogger logger, string format)
		{
			if (logger == null) throw new ArgumentNullException(nameof(logger));
			_logger = logger;
			_content = format;
			_logger.WriteInfo($"Starting {format}");
			_stopwatch = Stopwatch.StartNew();
		}

		public void Dispose()
		{
			_stopwatch.Stop();
			_logger.WriteInfo($"Finishing {_content} {_stopwatch.ElapsedMilliseconds}ms ({_stopwatch.ElapsedMilliseconds / 1000}secs)");
		}
	}
}
