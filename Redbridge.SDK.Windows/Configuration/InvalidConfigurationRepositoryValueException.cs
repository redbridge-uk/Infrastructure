using System;

namespace Redbridge.Configuration
{
	public class InvalidConfigurationRepositoryValueException : RedbridgeException
	{
		public InvalidConfigurationRepositoryValueException() { }

		public InvalidConfigurationRepositoryValueException(string message) : base(message) { }

		public InvalidConfigurationRepositoryValueException(string message, Exception inner) : base(message, inner) { }
	}
}
