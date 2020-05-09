using System;

namespace Redbridge.Data
{
	public static class TryGetResult
	{
		public static TryGetResult<T> Fail<T>()
		{
			return new TryGetResult<T>(default(T));
		}

		public static TryGetResult<T> FromResult<T>(T result)
		{
            if (!object.Equals(result, null))
			{
				return new TryGetResult<T>(result);
			}
			return Fail<T>();
		}

		public static TryGetResult<T> FromResult<T, TK>(TK result, Func<TK, T> converter)
		{
            if (!object.Equals(result, null))
			{
				return new TryGetResult<T>(converter(result));
			}
			return Fail<T>();
		}
	}

	public class TryGetResult<TEntity>
	{
		public TryGetResult(TEntity entity)
		{
			Item = entity;
		}

		public static TryGetResult<T> FromResult<T>(T result) where T : class
		{
			return new TryGetResult<T>(result);
		}

        public bool Success { get { return !object.Equals(Item, null); } }

		public TEntity Item { get; private set; }
	}
}
