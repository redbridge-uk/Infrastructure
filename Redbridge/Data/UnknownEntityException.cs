using System;
namespace Redbridge.SDK.Data
{
	public abstract class UnknownEntityException : RedbridgeException
	{
		protected UnknownEntityException() { }

		protected UnknownEntityException(string message) : base(message) { }

		protected UnknownEntityException(string message, Exception inner) : base(message, (Exception)inner) { }

		public abstract string EntityType
		{
			get;
		}
	}
}
