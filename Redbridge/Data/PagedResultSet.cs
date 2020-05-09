using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Redbridge.Data
{
	[DataContract]
	public class PagedResultSet<T> : PagedResultSet
	{
		public PagedResultSet() { }

		public PagedResultSet(T[] items) : this(items, PageRequest.All, items.Count()) { }

		public PagedResultSet(T[] items, PageRequest currentPageRequest, int totalItems)
		{
			if (items == null) throw new ArgumentNullException(nameof(items));
			if (currentPageRequest == null) throw new ArgumentNullException(nameof(currentPageRequest));

			Items = items;
			TotalItems = totalItems;
			TotalPages = CalculateTotalPages(currentPageRequest, totalItems);
			if (currentPageRequest.Size != null) PageSize = currentPageRequest.Size.Value;
			Page = currentPageRequest.Page;
		}

		[DataMember]
		public T[] Items
		{
			get;
			set;
		}

		public PagedResultSet<TK> ChangeType<TK>(Func<T, TK> func)
		{
			var items = Items.Select(func).ToArray();
			var result = new PagedResultSet<TK>(items)
			{
				PageSize = PageSize,
				Page = Page,
				TotalItems = TotalItems,
				TotalPages = TotalPages
			};
			return result;
		}
	}

	[DataContract]
	public class PagedResultSet
	{
		[DataMember]
		public int PageSize
		{
			get;
			set;
		}

		[DataMember]
		public int Page
		{
			get;
			set;
		}

		[DataMember]
		public int TotalPages
		{
			get;
			set;
		}

		[DataMember]
		public int TotalItems
		{
			get;
			set;
		}

		public static PagedResultSet<T> All<T>(IEnumerable<T> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException(nameof(items));
			}

			var itemArray = items.ToArray();
			var set = new PagedResultSet<T>(itemArray)
			{
				Page = 1,
				PageSize = itemArray.Length,
				TotalPages = 1,
				TotalItems = itemArray.Count(),
			};

			return set;
		}

		public static PagedResultSet<T> AsPage<T>(IEnumerable<T> items, PageRequest request)
		{
			var itemsArray = items != null ? items.ToArray() : Enumerable.Empty<T>().ToArray();
			return AsPage(itemsArray, itemsArray.Count(), request);
		}

		public static PagedResultSet<T> AsPage<T>(T[] items, int totalItems, PageRequest request)
		{
			if (items == null)
			{
				throw new ArgumentNullException(nameof(items));
			}

			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			var resultSet = new PagedResultSet<T>(items)
			{
				Page = request.Page,
				TotalItems = totalItems,
			};

			if (request.Size.HasValue && request.Size > 0)
			{
				resultSet.PageSize = request.Size.Value;
			}

			resultSet.TotalPages = CalculateTotalPages(request, totalItems);
			return resultSet;
		}

		protected static int CalculateTotalPages(PageRequest request, int totalItems)
		{
			if (totalItems > 0 && request.Size.HasValue && request.Size > 0)
			{
				var modulus = totalItems % request.Size.Value;

				if (modulus > 0)
					return (totalItems / request.Size.Value) + 1;

				return (totalItems / request.Size.Value);
			}

			return 1;
		}

		public static PagedResultSet<T> CreatePage<T>(IEnumerable<T> items, PageRequest request)
		{
			return CreatePage(items.AsQueryable(), request);
		}

		public static PagedResultSet<T> CreatePage<T>(IQueryable<T> items, PageRequest request)
		{
			if (items == null)
			{
				throw new ArgumentNullException(nameof(items));
			}

			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			// If we dont have a page size supplied then we deliver all results as a single page...
			if (request.Size.HasValue)
			{
				var itemsList = items.Skip(request.Skip).Take(request.Size.Value).ToArray();
				var totalItems = items.Count();

				return new PagedResultSet<T>(itemsList)
				{
					Page = request.Page,
					PageSize = request.Size.Value,
					TotalPages = CalculateTotalPages(request, totalItems),
				};
			}

			return All(items);
		}

		public static PagedResultSet<T> CreatePage<T>(IEnumerable<T> items, PageRequest request, int totalItems)
		{
			return CreatePage(items.AsQueryable(), request, totalItems);
		}

		public static PagedResultSet<T> CreatePage<T>(IQueryable<T> items, PageRequest request, int totalItems)
		{
			if (items == null)
			{
				throw new ArgumentNullException(nameof(items));
			}

			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			// If we dont have a page size supplied then we deliver all results as a single page...
			if (request.Size.HasValue)
			{
				var itemsList = items.Skip(request.Skip).Take(request.Size.Value).ToArray();

				return new PagedResultSet<T>(itemsList)
				{
					Page = request.Page,
					PageSize = request.Size.Value,
					TotalPages = CalculateTotalPages(request, totalItems),
					TotalItems = totalItems,
				};
			}

			return All(items);
		}

		public static PagedResultSet<TCast> CreatePage<T, TCast>(IQueryable<T> items, PageRequest request, Func<T, TCast> converter)
		{
			if (items == null) throw new ArgumentNullException(nameof(items));
			if (request == null) throw new ArgumentNullException(nameof(request));

			// If we dont have a page size supplied then we deliver all results as a single page...
			if (request.Size.HasValue)
			{
				var itemsList = items.Skip(request.Skip).Take(request.Size.Value).ToArray().Select(converter).ToArray();
				var totalItems = items.Count();

				var totalPages = 1;
				if (totalItems > request.Size.Value)
				{
					totalPages = totalItems / request.Size.Value;
				}

				return new PagedResultSet<TCast>(itemsList)
				{
					Page = request.Page,
					PageSize = request.Size.Value,
					TotalPages = totalPages,
				};
			}

			return All(items.Select(converter));
		}

		public static PagedResultSet<T> Empty<T>(PageRequest request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			return new PagedResultSet<T>(Enumerable.Empty<T>().ToArray(), request, 0);
		}

		public static PagedResultSet<T> FromResult<T>(T result)
		{
			return new PagedResultSet<T>(new[] { result }) { Page = 1, PageSize = 20, TotalItems = 1, TotalPages = 1 };
		}
	}
}
