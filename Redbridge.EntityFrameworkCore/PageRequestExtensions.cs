
using Microsoft.EntityFrameworkCore;
using Redbridge.Data;
using Redbridge.Linq;

namespace Redbridge.EntityFrameworkCore
{
	public static class PageRequestExtensions
	{
		public static async Task<PagedResultSet<T>> ExecuteAsync<T>(this PageRequest request, IQueryable<T> source) where T : class
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			if (source == null) throw new ArgumentNullException(nameof(source));

			var pageItemsQuery = request.CreateQuery<T>(source);
			var totalItemsQuery = request.GetTotalRecordCountQuery(source);

			var items = await pageItemsQuery.ToArrayAsync();
			var totalItemsCount = await totalItemsQuery.CountAsync();

			var resultSet = new PagedResultSet<T>(items, request, totalItemsCount);
			return resultSet;
		}

		public static async Task<PagedResultSet<TK>> ExecuteAsync<T, TK>(this PageRequest request, IQueryable<T> source, Func<T, TK> converter) where T : class
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			if (source == null) throw new ArgumentNullException(nameof(source));
			
			var pageItemsQuery = request.CreateQuery<T>(source);
			var totalItemsQuery = request.GetTotalRecordCountQuery(source);
			var items = await pageItemsQuery.ToArrayAsync();
			var totalItemsCount = await totalItemsQuery.CountAsync();

			return new PagedResultSet<TK>(items.Select(converter).ToArray(), request, totalItemsCount);
		}
	}
}
