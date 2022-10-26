using System.Globalization;

namespace Redbridge
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string source)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(source);
        }
    }
}