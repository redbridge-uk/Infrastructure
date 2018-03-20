using System;
namespace Redbridge.SDK
{
	public interface IMetadata<T> : IMetadata
	{
		new T Value { get; }
	}

	public interface IMetadata : IUnique<string>
	{
		object Value { get; }
	}
}
