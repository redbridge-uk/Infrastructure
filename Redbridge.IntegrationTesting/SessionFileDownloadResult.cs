using System;
using Microsoft.VisualBasic.FileIO;

namespace Redbridge.IntegrationTesting
{
	public class SessionFileDownloadResult
	{
		public string FilePath { get; set; }
	}

    public static class SessionFileDownloadResultExtensions
    {
        public static TextFieldParser OpenAsCsv(this SessionFileDownloadResult result)
        {
            return new TextFieldParser(result.FilePath);
        }
    }
}
