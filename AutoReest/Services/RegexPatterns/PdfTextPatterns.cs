using System.Text.RegularExpressions;

namespace AutoReest.Services.RegexPatterns
{
    public static partial class PdfTextPatterns
    {
        public const string NOTATION = "\\d+-\\d+\\/\\d+-\\d+-\\d+-\\S+";
        public const string TABLE = "\\d( )+?(\\d+-\\d+)?( )?(\\d+?.\\d+.?\\d+)";

        private static Regex notationRegex = new Regex(NOTATION);
        private static Regex tableRegex = new Regex(TABLE);

        public static Regex NotationRegex
        {
            get { return notationRegex; }
        }
        public static Regex TableRegex
        {
            get { return tableRegex; }
        }
    }
}
