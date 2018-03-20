using System;
using System.Threading.Tasks;
using Redbridge.SDK;

namespace Redbridge.Data
{
    public interface IReadonlyRepository<in TKey, TData> where TData : class
    {
        Task<TData[]> GetAllAsync();
		Task<TData> GetByKeyAsync(TKey key);
		Task<TryGetResult<TData>> TryGetByKeyAsync(TKey key);
    }

    public interface IRepository<in TKey, TData> : IReadonlyRepository<TKey, TData> 
        where TData : class
    {
		void Add(TData data);
		Task UpdateAsync(TData data);
    }
}
