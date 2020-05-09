namespace Redbridge.Data
{
	public interface IUpdatable<in TSource>
	{
		void UpdateFrom(TSource source);
	}
}
