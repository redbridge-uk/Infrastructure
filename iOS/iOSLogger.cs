using System;
using System.Diagnostics;
using Easilog.SDK.Diagnostics;

namespace Easilog.iOS
{
	public class iOSLogger : ILogger
	{
		public void WriteDebug(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteError(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteException(Exception exception)
		{
			Debug.WriteLine(exception.Message);
		}

		public void WriteInfo(string message)
		{
			Debug.WriteLine(message);
		}

		public void WriteWarning(string message)
		{
			Debug.WriteLine(message);
		}
	}
}
