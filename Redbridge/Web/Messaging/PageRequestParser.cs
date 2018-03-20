using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Redbridge.SDK
{
	public static class SearchStringParser
	{
		public static bool TryParseSearchString(string searchString, out SearchQuery query)
		{
			// If no string or a null string is provided then we don't apply a filter.
			if (string.IsNullOrWhiteSpace(searchString))
			{
				// Return all.
				query = new SearchQuery();
				return true;
			}

			var queryParts = searchString.Split(new[] { '|' });
			var splitParts = queryParts.Select(qp => qp.Split(new[] { '=' })).ToArray();

			// Any badly formed requests...
			if (splitParts.Any(s => s.Count() != 2))
			{
				query = null;
				return false;
			}

			var parts = splitParts.Where(s => s.Count() == 2).Select(p => new SearchParameter(p[0], p[1].Trim('*'), EvaluateOperationType(p[1])));
			query = new SearchQuery(parts);
			return true;
		}

		private static WhereOperation EvaluateOperationType(string input)
		{
			var trimmedInput = input.Trim();
			if (trimmedInput.StartsWith("*"))
			{
				if (trimmedInput.EndsWith("*"))
				{
					return WhereOperation.Contains;
				}
				return WhereOperation.EndsWith;
			}

			if (trimmedInput.EndsWith("*"))
			{
				return WhereOperation.StartsWith;
			}

			return WhereOperation.Equal;
		}
	}

	public static class PageRequestParser
	{
		private static class GroupNames
		{

			public const string Sort = "Sort";

			public const string Property = "Property";
		}

		private const string Ascending = "+";
		private const int DefaultPageSize = 20;

		private static readonly Regex SortRegex = new Regex("(?<Sort>[+-]{1})(?<Property>[^-+]*)",
			RegexOptions.CultureInvariant);

		public static PageRequest ParseUrlRequest(int page, int size, string sort = "", string filter = "")
		{
			if (page < 1)
				page = 1;

			if (size < 1)
				size = DefaultPageSize;

			var sorting = ParseSortRequest(sort);

			SearchQuery parsedQuery;
			if (SearchStringParser.TryParseSearchString(filter, out parsedQuery))
				return PageRequest.Create(size, page, sorting, parsedQuery);

			return PageRequest.None;
		}

		private static IEnumerable<PageSort> ParseSortRequest(string sort)
		{
			if (!string.IsNullOrWhiteSpace(sort))
			{
				var sortingMatches = SortRegex.Matches(sort);

				var sorts = new List<PageSort>();
				var priority = 0;
				foreach (Match match in sortingMatches)
				{
					if (match.Length > 0)
					{
						sorts.Add(match.Groups[GroupNames.Sort].Value == Ascending
							? new PageSort(priority, SortDirection.Ascending, match.Groups[GroupNames.Property].Value)
							: new PageSort(priority, SortDirection.Descending, match.Groups[GroupNames.Property].Value));

						priority++;
					}
				};
				return sorts;
			}
			return Enumerable.Empty<PageSort>();
		}
	}
}
