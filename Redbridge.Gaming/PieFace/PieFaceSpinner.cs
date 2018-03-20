using System;

namespace Redbridge.Gaming.PieFace
{
	public class PieFaceSpinner : Spinner<int>
	{
		public PieFaceSpinner() : base(new[] { 2,3,4,5 })
		{
		}
	}
}
