using System.Collections.Generic;
using System.Text;

namespace Redbridge.Data
{

	public static class PageSortExtensions
	{
		public static string ToQueryString(this IEnumerable<PageSort> sorts)
		{
			if (sorts != null)
			{
				var builder = new StringBuilder();

				foreach (var sort in sorts)
				{
					builder.AppendFormat("{0}{1}", sort.Symbol, sort.Property.ToLowerInvariant());
				}

				return builder.ToString();
			}

			return string.Empty;
		}

		public static string ToQueryString(this IEnumerable<SearchParameter> searchParameters)
		{
			if (searchParameters != null)
			{
				var builder = new StringBuilder();
				var isFirstParameter = true;

				foreach (var search in searchParameters)
				{
					if (!isFirstParameter)
					{
						builder.Append(search.Operator == SearchParameterOperator.Or ? '|' : '+');
					}

					builder.Append(search.AsQueryString());

					isFirstParameter = false;
				}

				return builder.ToString();
			}

			return string.Empty;
		}
	}
	
}
