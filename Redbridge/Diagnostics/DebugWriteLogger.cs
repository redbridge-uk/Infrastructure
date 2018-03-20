using System;
using System.Diagnostics;

namespace Redbridge.Diagnostics
{

	public class DebugWriteLogger : ILogger
	{
		public void WriteVerbose(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteError(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteException(Exception message)
		{
			Debug.WriteLine(message.Message);
		}

		public void WriteWarning(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteInfo(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteDebug(string message)
		{
			Debug.WriteLine(message);
		}
	}
	
}
