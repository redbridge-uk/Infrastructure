using System;
using System.Diagnostics;

namespace Redbridge.Diagnostics
{
	public class DebugEventArgs : EventArgs
	{
		public DebugEventArgs(string message)
		{
			Message = message;
		}

		public string Message
		{
			get;
			private set;
		}
	}


}
