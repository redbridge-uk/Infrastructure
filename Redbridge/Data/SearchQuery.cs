using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Redbridge.Data
{
    [DataContract]
	public class SearchQuery
	{
		private List<SearchParameter> _searchParameters = new List<SearchParameter>();

		public SearchQuery() { }

		public SearchQuery(IEnumerable<SearchParameter> parameters)
		{
			if (parameters == null) throw new ArgumentNullException(nameof(parameters));
			_searchParameters.AddRange(parameters);
		}

		[DataMember]
		public IEnumerable<SearchParameter> Parameters
		{
			get { return _searchParameters; }
			set { _searchParameters = new List<SearchParameter>(value); }
		}

		public string ToQueryString()
		{
			if (Parameters != null && Parameters.Any())
			{
				return Parameters.ToQueryString();
			}

			return string.Empty;
		}
	}
	
}
