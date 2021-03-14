using System;
using System.Runtime.Serialization;

namespace Redbridge.Configuration
{
	[Serializable]
	public class InvalidConfigurationRepositoryValueException : RedbridgeException
	{
		public InvalidConfigurationRepositoryValueException() { }

		public InvalidConfigurationRepositoryValueException(string message) : base(message) { }

		public InvalidConfigurationRepositoryValueException(string message, Exception inner) : base(message, inner) { }

        protected InvalidConfigurationRepositoryValueException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
	}
}
