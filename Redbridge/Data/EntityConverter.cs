using System;
using System.Threading.Tasks;

namespace Redbridge.Data
{
	public abstract class EntityConverter<TInput, TOutput> : IEntityConverter<TInput, TOutput>
		where TOutput : class
		where TInput : class
	{
		public TOutput Convert(TInput input)
		{
			if (input == null)
				throw new ArgumentNullException(nameof(input), "The supplied input instance is null.");

			// See if a conversion is actually necessary...
			var output = input as TOutput;

			// If a conversion is not necessary return the pre-converted type, this avoids needless conversions.
			if (output != null)
				return output;

			OnBeforeConvert(input);
			var result = OnConvert(input);
			OnAfterConvert(input, result);
			return result;
		}

		public async Task<TOutput> ConvertAsync(TInput input)
		{
			if (input == null)
				throw new ArgumentNullException(nameof(input), "The supplied input instance is null.");

			// See if a conversion is actually necessary...
			var output = input as TOutput;

			// If a conversion is not necessary return the pre-converted type, this avoids needless conversions.
			if (output != null)
				return output;

			OnBeforeConvert(input);
			var result = await OnConvertAsync(input);
			await OnAfterConvertAsync(input, result);
			return result;
		}

		protected virtual void OnBeforeConvert(TInput input) { }

		protected virtual Task<TOutput> OnConvertAsync(TInput input)
		{
			return Task.FromResult(OnConvert(input));
		}

		protected abstract TOutput OnConvert(TInput input);

		protected virtual Task OnAfterConvertAsync(TInput input, TOutput output)
		{
			return Task.FromResult(true);
		}

		protected virtual void OnAfterConvert(TInput input, TOutput output)
		{
		}

		public virtual bool CanConvert(TInput input) { return true; }
	}
}
