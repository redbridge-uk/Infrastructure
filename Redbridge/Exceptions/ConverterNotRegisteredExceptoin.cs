using System;

namespace Redbridge.Exceptions
{
	public class ConverterNotRegisteredException : Exception
	{
		public ConverterNotRegisteredException()
		{
		}

		public ConverterNotRegisteredException(string message) : base(message)
		{
		}

		public ConverterNotRegisteredException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
