using System;
namespace Redbridge.SDK
{
	public static class EventHandlerExtensions
	{
		public static void SafeInvoke(this EventHandler input, object sender)
		{
			input.SafeInvoke(sender, EventArgs.Empty);
		}

		public static void SafeInvoke(this EventHandler input, object sender, EventArgs args)
		{
			// Compiler time safety, it is built into the compiler to make a copy of this reference
			var handler = input;
			handler?.Invoke(sender, args);
		}

		public static void SafeInvoke<T>(this EventHandler<T> input, object sender, T args)
		{
			// Compiler time safety, it is built into the compiler to make a copy of this reference
			var handler = input;
			handler?.Invoke(sender, args);
		}
	}
}
