using System;
using System.IO;
using System.Text;

namespace Redbridge.IO
{
	public class Utf8StringWriter : StringWriter
	{
		public override Encoding Encoding
		{
			get { return Encoding.UTF8; }
		}
	}
}
