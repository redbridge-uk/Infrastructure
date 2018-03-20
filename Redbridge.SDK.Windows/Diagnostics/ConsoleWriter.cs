using System;
namespace Redbridge.Diagnostics
{
	public class ConsoleWriter : IDisposable
	{
		private readonly ConsoleColor _previousForegroundColor;
		private readonly ConsoleColor _previousBackgroundColor;
		public ConsoleWriter(ConsoleColor foreground) : this(Console.BackgroundColor, foreground) { }
		public ConsoleWriter(ConsoleColor background, ConsoleColor foreground)
		{
			_previousForegroundColor = Console.ForegroundColor;
			_previousBackgroundColor = Console.BackgroundColor;
			Console.ForegroundColor = foreground;
			Console.BackgroundColor = background;
		}

		public static void WriteLine(ConsoleColor colour, string message, params object[] arguments)
		{
			using (var writer = new ConsoleWriter(colour))
			{
				writer.WriteLine(message, arguments);
			}
		}

		public void Write(string message, params object[] arguments)
		{
			Console.Write(message, arguments);
		}

		public void WriteLine(string message, params object[] arguments)
		{
			Console.WriteLine(message, arguments);
		}

		public void Dispose()
		{
			Console.ForegroundColor = _previousForegroundColor;
			Console.BackgroundColor = _previousBackgroundColor;
		}
	}
}
