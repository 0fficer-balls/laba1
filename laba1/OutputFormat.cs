using System.Globalization;

namespace laba1
{
    public static class OutputFormat
    {
        public static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        public const string Price = "0.00";
        public const string SingleDecimal = "0.#";
        public const string CurrencySymbol = "$";
    }
}
