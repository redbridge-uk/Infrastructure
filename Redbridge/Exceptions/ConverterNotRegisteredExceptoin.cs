using System;
using System.Runtime.Serialization;

namespace Redbridge.Exceptions
{
	[Serializable]
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

        protected ConverterNotRegisteredException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
	}
}
