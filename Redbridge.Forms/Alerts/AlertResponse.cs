using System;
using System.Threading.Tasks;

namespace Redbridge.Forms
{

	public class AlertResponse
	{
		public AlertResponse(bool result)
		{
			Result = result;	
		}

		public bool Result
		{
			get;
			set;
		}
	}
	
}
