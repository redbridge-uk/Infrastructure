namespace Redbridge.Data
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
