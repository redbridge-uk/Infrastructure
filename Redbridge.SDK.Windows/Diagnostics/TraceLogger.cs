using System;
using System.Diagnostics;
using Redbridge.Diagnostics;

namespace Redbridge.Windows.Diagnostics
{
	public class TraceLogger : ILogger
	{
		public void WriteVerbose(string message)
		{
			Trace.WriteLine(message, "Verbose");
		}

		public void WriteError(string message)
		{
			Trace.WriteLine(message, "Error");
		}

		public void WriteException(Exception exception)
		{
			Trace.WriteLine(exception.Message, "Error");
		}

		public void WriteWarning(string message)
		{
			Trace.WriteLine(message, "Warning");
		}

		public void WriteInfo(string message)
		{
			Trace.WriteLine(message, "Information");
		}

		public void WriteDebug(string message)
		{
			Trace.WriteLine(message, "Verbose");
		}
	}
}
