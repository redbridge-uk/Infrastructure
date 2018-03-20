using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Redbridge.IntegrationTesting
{
    public static class TestFieldParserExtensions
    {
        public static TextFieldParser AssertLineCount(this TextFieldParser parser, int lines)
        {
            var totalLines = 0;

            while (parser.ReadLine() != null)
                totalLines++;
            Assert.AreEqual(lines, totalLines, "Unexpected line count in csv file.");


            return parser;
        }
    }
}
