using System;
using System.Globalization;
using System.IO;

namespace laba1
{
    public static class ConsoleInput
    {
        private const string RetryMessage = "Некорректное значение, повторите ввод: ";
        private const string YesAnswer = "y";
        public static string ReadNonEmptyLine(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                string line = ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    return line.Trim();
                }

                Console.Write(RetryMessage);
            }
        }

        public static decimal ReadDecimalAtLeast(string prompt, decimal minimum)
        {
            Console.Write(prompt);
            while (true)
            {
                decimal value;
                if (TryParseDecimal(ReadLine(), out value) && value >= minimum)
                {
                    return value;
                }

                Console.Write(RetryMessage);
            }
        }

        public static double ReadDoubleGreaterThan(string prompt, double exclusiveMinimum)
        {
            Console.Write(prompt);
            while (true)
            {
                double value;
                if (TryParseDouble(ReadLine(), out value) && value > exclusiveMinimum)
                {
                    return value;
                }

                Console.Write(RetryMessage);
            }
        }

        public static int ReadIntInRange(string prompt, int minimum, int maximum)
        {
            Console.Write(prompt);
            while (true)
            {
                int value;
                if (int.TryParse(ReadLine(), out value) && value >= minimum && value <= maximum)
                {
                    return value;
                }

                Console.Write(RetryMessage);
            }
        }

        public static bool ReadYesNo(string prompt)
        {
            Console.Write(prompt);
            string answer = ReadLine();
            return answer != null
                   && answer.Trim().StartsWith(YesAnswer, StringComparison.OrdinalIgnoreCase);
        }

        private static string ReadLine()
        {
            string line = Console.ReadLine();
            if (line == null)
            {
                throw new EndOfStreamException("Входной поток закончился.");
            }

            return line;
        }

        private static bool TryParseDouble(string text, out double value)
        {
            return double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value)
                   || double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value);
        }

        private static bool TryParseDecimal(string text, out decimal value)
        {
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value)
                   || decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value);
        }
    }
}
