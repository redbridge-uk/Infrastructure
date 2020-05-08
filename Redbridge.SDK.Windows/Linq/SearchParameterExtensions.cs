using System.Linq;
using Redbridge.SDK;

namespace Redbridge.Windows.Linq
{
public static class SearchParameterExtensions
{
	public static IQueryable<T> ApplyCriteria<T>(this SearchParameter parameter, IQueryable<T> query)
	{
		var results = query.Where(parameter.Property, parameter.Search, parameter.WhereOperator);
		return results.AsQueryable();
	} }
}
