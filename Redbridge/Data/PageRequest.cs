using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Redbridge.Data
{
	

	[DataContract]
	public class PageRequest
	{
		public static readonly PageRequest None;
		public static readonly PageRequest All;
		public static readonly PageRequest First;

		static PageRequest()
		{
			All = new PageRequest
			{
				Size = int.MaxValue,
				Sort = new[] { new PageSort(1, SortDirection.Ascending, DefaultSortProperty) }
			};

			None = new PageRequest
			{
				Size = 0,
				Page = 1,
				Sort = new[] { new PageSort(1, SortDirection.Ascending, DefaultSortProperty) }
			};

			First = new PageRequest
			{
				Page = 1,
				Size = DefaultPageSize,
				Sort = new[] { new PageSort(1, SortDirection.Ascending, DefaultSortProperty) }
			};
		}

		public const int DefaultPageSize = 25;
		public const string DefaultSortProperty = "Created";

		public static PageRequest Create(int pageSize)
		{
			return Create(pageSize, 1, null, new SearchQuery());
		}

		public static PageRequest Create(int pageSize, int pageNumber)
		{
			return Create(pageSize, pageNumber, new[]
			{
				new PageSort(1, SortDirection.Ascending, DefaultSortProperty)
			}, new SearchQuery());
		}

		public static PageRequest Create(int pageSize, int pageNumber, IEnumerable<PageSort> sorts)
		{
			return Create(pageSize, pageNumber, sorts, new SearchQuery());
		}

		public static PageRequest Create(int pageSize, int pageNumber, IEnumerable<PageSort> sorts, SearchQuery searchQuery)
		{
			return new PageRequest()
			{
				Size = pageSize,
				Page = pageNumber,
				Sort = sorts,
				Filter = searchQuery,
			};
		}

		private PageRequest()
		{
			// Assumption is the first page of records if no number is specified.
			Page = 1;
		}

		public int Skip
		{
			get
			{
				if (Size.HasValue)
				{
					return (Page - 1) * Size.Value;
				}

				return 0;
			}
		}

		[DataMember]
		public int Page
		{
			get;
			set;
		}

		[DataMember]
		public int? Size
		{
			get;
			set;
		}

		[DataMember]
		public IEnumerable<PageSort> Sort
		{
			get;
			set;
		}

		[DataMember]
		public SearchQuery Filter
		{
			get;
			set;
		}

		[DataMember]
		/**
         * For that case where the skip part of the query is handled externally 
         * For example:  The search job entries query already handles the skip
         */
		public bool SkipHandledExternally
		{
			get;
			set;
		}

		public string ToQueryString()
		{
			var builder = new StringBuilder();
			builder.AppendFormat("page={0}", Page);
			if (Size.HasValue)
			{
				builder.AppendFormat("&size={0}", Size.Value);
			}
			if (Sort != null && Sort.Any())
			{
				builder.AppendFormat("&sort={0}", Sort.ToQueryString());
			}
			if (Filter != null && Filter.Parameters.Any())
			{
				builder.AppendFormat("&filter={0}", Filter.ToQueryString());
			}
			return builder.ToString();
		}

		public override string ToString()
		{
			return ToQueryString();
		}
	}
}
