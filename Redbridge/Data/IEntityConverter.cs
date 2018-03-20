using System;
using System.Threading.Tasks;

namespace Redbridge.SDK
{
	public interface IEntityConverter<in TInput, TOutput>
	{
		Task<TOutput> ConvertAsync(TInput input);

		TOutput Convert(TInput input);

		bool CanConvert(TInput input);
	}
}
