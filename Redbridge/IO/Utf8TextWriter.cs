using System;
using System.IO;
using System.Text;

namespace Redbridge.IO
{

	public class Utf8TextWriter : TextWriter
	{
		public override Encoding Encoding
		{
			get { return Encoding.UTF8; }
		}

		public override void Write(char value)
		{
			throw new NotImplementedException();
		}
	}
}
