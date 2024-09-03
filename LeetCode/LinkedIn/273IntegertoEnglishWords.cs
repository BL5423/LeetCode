using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _273IntegertoEnglishWords
    {
        public string NumberToWords(int num)
        {
            if (num == 0)
                return "Zero";

            const int billion = 1000000000;
            const int million = 1000000;
            const int thousand = 1000;

            StringBuilder sb = new StringBuilder();
            int b = num / billion;
            if (b != 0)
            {
                sb.Append(Convert(b));
                sb.Append(" Billion");
            }

            int m = (num / million) % thousand;
            if (m != 0)
            {
                if (sb.Length != 0)
                    sb.Append(" ");
                sb.Append(Convert(m));
                sb.Append(" Million");
            }

            int t = (num / thousand) % thousand;
            if (t != 0)
            {
                if (sb.Length != 0)
                    sb.Append(" ");
                sb.Append(Convert(t));
                sb.Append(" Thousand");
            }

            int h = num % thousand;
            if (h != 0)
            {
                if (sb.Length != 0)
                    sb.Append(" ");
                sb.Append(Convert(h));
            }

            return sb.ToString();
        }

        private static string[] LastDigit2Word = new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };

        private static string[] SecondDigit2Word = new string[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        private static string[] Last2Digits2Word = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

        private static StringBuilder Convert(int num)
        {
            // convert up to 3 digits to words
            StringBuilder sb = new StringBuilder();
            int h = num / 100;
            if (h != 0)
            {
                sb.Append(LastDigit2Word[h]);
                sb.Append(" Hundred");
            }

            int r = num % 100;
            if (r >= 20)
            {
                int t = (num % 100) / 10;
                if (t != 0)
                {
                    if (sb.Length != 0)
                        sb.Append(" ");
                    sb.Append(SecondDigit2Word[t]);
                }

                int n = num % 10;
                if (n != 0)
                {
                    if (sb.Length != 0)
                        sb.Append(" ");
                    sb.Append(LastDigit2Word[n]);
                }
            }
            else if (r != 0)
            {
                if (sb.Length != 0)
                    sb.Append(" ");

                if (r < 10)
                {
                    sb.Append(LastDigit2Word[r]);
                }
                else
                {
                    sb.Append(Last2Digits2Word[r - 10]);
                }
            }

            return sb;
        }
    }
}
