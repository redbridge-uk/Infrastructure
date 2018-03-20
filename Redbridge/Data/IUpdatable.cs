using System;
namespace Redbridge.SDK
{
	public interface IUpdatable<in TSource>
	{
		void UpdateFrom(TSource source);
	}
}
