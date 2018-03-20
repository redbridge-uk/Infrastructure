using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Redbridge.SDK
{

	[DataContract]
	public enum SearchParameterOperator
	{
		[EnumMember]
		Or = 0,
	}
	
}
