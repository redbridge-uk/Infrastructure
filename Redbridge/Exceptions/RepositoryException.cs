using System;
using System.Runtime.Serialization;

namespace Redbridge.Exceptions
{
	[Serializable]
	public class RepositoryException : RedbridgeException
	{
		public RepositoryException() : base() { }
		public RepositoryException(string message) : base(message) { }
		public RepositoryException(string message, Exception exception) : base(message, exception) { }

        protected RepositoryException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
	}
}
