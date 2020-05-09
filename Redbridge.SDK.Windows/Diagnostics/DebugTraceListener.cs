using System;
using System.Diagnostics;
using Redbridge.Diagnostics;

namespace Redbridge.Windows.Diagnostics
{
	public class DebugTraceListener : TraceListener
	{
		public event EventHandler<DebugEventArgs> MessageReceived;

		protected virtual void OnMessageReceived(string message)
		{
			var handler = MessageReceived;

			if (handler != null)
				handler.Invoke(this, new DebugEventArgs(message));
		}

		public override void Write(string message)
		{
			OnMessageReceived(message);
		}

		public override void WriteLine(string message)
		{
			OnMessageReceived(message);
		}
	}
	
}
