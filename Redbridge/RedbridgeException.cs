using System;
using System.Runtime.Serialization;

namespace Redbridge
{
    [Serializable]
	public class RedbridgeException : Exception
	{
		public RedbridgeException() { }

		public RedbridgeException(string message) : base(message) { }

		public RedbridgeException(string message, Exception inner) : base(message, inner) { }

        protected RedbridgeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
