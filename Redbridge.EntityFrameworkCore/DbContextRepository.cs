using Microsoft.EntityFrameworkCore;
using Redbridge.Data;
using Redbridge.Exceptions;

namespace Redbridge.EntityFrameworkCore
{
	public abstract class DbContextRepository<TKey, TEntityData, TEntity, TContext>
		: DbContextRepository<TContext>, IRepository<TKey, TEntityData>
		where TEntity : class, IUpdatable<TEntityData>, IUnique<TKey> where TKey : IEquatable<TKey>
		where TEntityData : class, IUnique<TKey>
        where TContext: DbContext
	{
		private readonly TContext _container;
		private readonly Func<TContext, DbSet<TEntity>> _accessor;

		protected DbContextRepository(TContext container) : base(container)
		{
            _container = container ?? throw new ArgumentNullException(nameof(container));
		}

		protected DbContextRepository(TContext container, Func<TContext, DbSet<TEntity>> accessor) : base(container)
		{
            _container = container ?? throw new ArgumentNullException(nameof(container));
			_accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
		}

		protected TEntity[] OnConvertToEntityArray(IEnumerable<TEntityData> items)
		{
			return items?.Select(OnConvertToEntity).ToArray();
		}

		protected TEntityData[] OnConvertToDataArray(IEnumerable<TEntity> items)
		{
			return items?.Select(OnConvertToData).ToArray();
		}

		protected abstract TEntity OnConvertToEntity(TEntityData data);
		protected abstract TEntityData OnConvertToData(TEntity entity);

		protected DbSet<TEntity> DatabaseSet => _accessor(_container);

		public void Add(TEntityData data)
		{
			var entity = OnConvertToEntity(data);
			DatabaseSet.Add(entity);
		}

		public async Task UpdateAsync(TEntityData data)
		{
			var entity = await GetEntityAsync(data);
			entity.UpdateFrom(data);
		}

		public async Task DeleteRangeAsync(TKey[] keys)
		{
			var list = new List<TKey>(keys);
			var accessor = GetDirectAccessor();
			var itemsToRemove = await accessor.Where(a => list.Contains(a.Id)).ToArrayAsync();
			itemsToRemove.ForEach(i => accessor.Remove(i));
		}

		public async Task DeleteAsync(TKey key)
		{
			var accessor = GetDirectAccessor();
			var entity = await accessor.FirstAsync(a => a.Id.Equals(key));
			accessor.Remove(entity);
		}

		private Task<TEntity> GetEntityAsync(TEntityData data)
		{
			return GetEntityByKeyAsync(data.Id);
		}

		private async Task<TEntity> TryGetEntityByKeyAsync(TKey key)
		{
			var accessor = GetAccessor();
			var entity = await accessor.FirstOrDefaultAsync(a => a.Id.Equals(key));
			return entity;
		}

		private async Task<TEntity> GetEntityByKeyAsync(TKey key)
		{
			var entity = await TryGetEntityByKeyAsync(key);
			if (entity == null) throw CreateUnknownEntityException(key);
			return entity;
		}

		public async Task<TEntityData> GetByKeyAsync(TKey key)
		{
			var entity = await GetEntityByKeyAsync(key);
			return OnConvertToData(entity);
		}

		public async Task<TryGetResult<TEntityData>> TryGetByKeyAsync(TKey key)
		{
			var entity = await TryGetEntityByKeyAsync(key);
			if (entity != null)
			{
				var data = OnConvertToData(entity);
				return new TryGetResult<TEntityData>(data);
			}

			return TryGetResult.Fail<TEntityData>();
		}

		public async Task<TEntityData[]> GetAllAsync()
		{
			var accessor = GetAccessor();
			var entities = await accessor.ToArrayAsync();
			return OnConvertToDataArray(entities);
		}

		protected DbSet<TEntity> GetDirectAccessor()
		{
			return _accessor(Context);
		}

		protected IQueryable<TEntity> GetAccessor()
		{
			return OnApplyIncludes(_accessor(Context));
		}

		protected virtual IQueryable<TEntity> OnApplyIncludes(IQueryable<TEntity> entity)
		{
			return entity;
		}

		protected ValidationException CreateUnknownEntityException(TKey key)
		{
			return new ValidationException($"Unable to locate entity with key {key}");
		}
	}

	public abstract class DbContextRepository<TContext> where TContext: class
	{
		protected DbContextRepository(TContext context)
		{
            Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		protected TContext Context { get; }
	}
}