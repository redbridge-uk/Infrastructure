using System;
using System.Diagnostics;

namespace Redbridge.Diagnostics
{
	public class TraceWriterConsoleListenerLogger : ILogger
	{
		public TraceWriterConsoleListenerLogger()
		{
			Trace.Listeners.Add(new TextWriterTraceListener(System.Console.Out));
		}

		public void WriteVerbose(string message)
		{
			Trace.WriteLine(message);
		}

		public void WriteError(string message)
		{
			Trace.WriteLine(message);
		}

		public void WriteException(Exception exception)
		{
			Trace.WriteLine(exception.Message);
		}

		public void WriteWarning(string message)
		{
			Trace.WriteLine(message);
		}

		public void WriteInfo(string message)
		{
			Trace.WriteLine(message);
		}

		public void WriteDebug(string message)
		{
			Trace.WriteLine(message);
		}
	}
}
