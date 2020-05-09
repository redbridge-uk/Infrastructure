using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redbridge.Exceptions;

namespace Redbridge.Data
{
	public abstract class MultiTypeEntityConverter<TInput, TOutput> : EntityConverter<TInput, TOutput>
		where TInput : class
		where TOutput : class
	{
		private readonly Dictionary<Type, IEntityConverter<TInput, TOutput>> _converters = new Dictionary<Type, IEntityConverter<TInput, TOutput>>();

		public void RegisterConverter(IEntityConverter<TInput, TOutput> converter)
		{
			var converterType = converter.GetType();

			if ( _converters.ContainsKey(converterType) )
				throw new NotSupportedException($"This multi type converter already contains a converter for type {converterType}. Only one is supported.");
			
			_converters.Add(converterType,  converter);
		}

		protected class TypeConverter<TBaseType, TBaseInterfaceType> : IEntityConverter<TInput, TOutput>
			where TBaseType : IUpdatable<TBaseInterfaceType>, TOutput, new()
			where TBaseInterfaceType : class, TInput
		{
			public bool CanConvert(TInput input)
			{
				return input is TBaseInterfaceType;
			}

			public async Task<TOutput> ConvertAsync(TInput input)
			{
				var actionType = new TBaseType();
				var action = actionType as IUpdatable<TBaseInterfaceType>;
				action.UpdateFrom((TBaseInterfaceType)input);
				return await Task.FromResult(actionType);
			}

			public TOutput Convert(TInput input)
			{
				var actionType = new TBaseType();
				var action = actionType as IUpdatable<TBaseInterfaceType>;
				action.UpdateFrom((TBaseInterfaceType)input);
				return actionType;
			}
		}

		protected override async Task<TOutput> OnConvertAsync(TInput input)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input), "The supplied input is not permitted to be null");
			}

			var converter = _converters.Values.FirstOrDefault(c => c.CanConvert(input));
			if (converter == null) throw new ConverterNotRegisteredException($"The multi-type entity converter does not have a matching converter for the input type {input.GetType()} you must register a converter to support this conversion.");
			var result = await converter.ConvertAsync(input);
			await OnAfterConversionAsync(result);
			return result;
		}

		protected override TOutput OnConvert(TInput input)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input), "The supplied input is not permitted to be null");
			}

			var converter = _converters.Values.FirstOrDefault(c => c.CanConvert(input));
			if (converter == null) throw new ConverterNotRegisteredException($"The multi-type entity converter does not have a matching converter for the input type {input.GetType()} you must register a converter to support this conversion.");
			var result = converter.Convert(input);
			OnAfterConversion(result);
			return result;
		}

		protected virtual void OnAfterConversion(TOutput result) { }

		protected virtual Task OnAfterConversionAsync(TOutput result)
		{
			return Task.FromResult(true);
		}
	}
}
