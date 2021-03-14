using System;
using System.Runtime.Serialization;

namespace Redbridge.Exceptions
{
	[Serializable]
	public class DependencyResolutionException : RedbridgeException
	{
		public DependencyResolutionException() { }

		public DependencyResolutionException(string message) : base(message) { }

		public DependencyResolutionException(string message, Exception inner) : base(message, inner) { }

        protected DependencyResolutionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
	}
}
