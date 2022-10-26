using System.Reflection;

namespace Redbridge.IO
{
	public class TextResourceReader : ResourceReader
	{
		public string ReadText(string resourcePath)
		{
			return ReadText(Assembly.GetCallingAssembly(), resourcePath);
		}

		public string ReadText(Assembly assembly, string resourcePath)
		{
			var stream = GetResourceStream(assembly, resourcePath);
			return stream.AsString();
		}
	}
}
 