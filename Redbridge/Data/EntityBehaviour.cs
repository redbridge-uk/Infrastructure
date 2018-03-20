using System;

namespace Redbridge.Data
{
	public abstract class EntityBehaviour<TData> where TData : class
	{
		private readonly TData _data;

		protected EntityBehaviour(TData data)
		{
            _data = data ?? throw new ArgumentNullException(nameof(data));
		}

		protected TData Model => _data;

		public TData GetStateSnapshot()
		{
			return _data;
		}
	}
}
