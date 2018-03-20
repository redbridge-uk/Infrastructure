using System;
using System.Collections.Generic;
using System.Linq;
using Redbridge.SDK;

namespace Redbridge.Linq
{
public static class SearchQueryExtensions
{
	public static IQueryable<T> CreateQuery<T>(this SearchQuery searchQuery, IEnumerable<T> input)
	{
		return CreateQuery(searchQuery, input.AsQueryable());
	}

	public static IQueryable<T> CreateQuery<T>(this SearchQuery searchQuery, IQueryable<T> input)
	{
		if (searchQuery.Parameters.Any())
		{
			//var andQuery = input;
			var orQueries = new List<IQueryable<T>>();

			foreach (var searchParameter in searchQuery.Parameters)
			{
				if (searchParameter.Operator == SearchParameterOperator.Or)
				{
					// Create a separate query for the or...
					orQueries.Add(searchParameter.ApplyCriteria(input));
				}
				else
				{
					throw new NotSupportedException();
					// Not yet supported
					//searchParameter.ApplyCriteria(andQuery);
				}
			}

			IQueryable<T> finalQuery = orQueries.Aggregate<IQueryable<T>, IQueryable<T>>(null, (current, orQuery) => current == null ? orQuery : current.Concat(orQuery));
			return finalQuery;
		}

		return input;
	} }
}
