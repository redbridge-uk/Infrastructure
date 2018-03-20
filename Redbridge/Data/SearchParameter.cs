using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Redbridge.SDK
{

	[DataContract]
	public class SearchParameter
	{
		[DataMember]
		public string Property { get; set; }

		[DataMember]
		public string Search { get; set; }

		[DataMember]
		public WhereOperation WhereOperator { get; set; }

		[DataMember]
		public SearchParameterOperator Operator { get; set; }

		public SearchParameter(string property, string search, WhereOperation whereOperator)
		{
			if (property == null) throw new ArgumentNullException(nameof(property));
			if (search == null) throw new ArgumentNullException(nameof(search));

			Operator = SearchParameterOperator.Or;
			Property = property;
			Search = search;
			WhereOperator = whereOperator;
		}

		public string AsQueryString()
		{
			switch (WhereOperator)
			{
				case WhereOperation.Equal:
					return $"{Property}%3D{Search}";
				case WhereOperation.Contains:
					return $"{Property}%3D*{Search}*";
				case WhereOperation.StartsWith:
					return $"{Property}%3D{Search}*";
				case WhereOperation.EndsWith:
					return $"{Property}%3D*{Search}";
				default:
					throw new NotSupportedException("Unknown where operator.");
			}
		}
	}
	
}
