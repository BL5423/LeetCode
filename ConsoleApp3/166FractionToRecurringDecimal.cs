using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _166FractionToRecurringDecimal
    {
        public string FractionToDecimal(long numerator, long denominator)
        {
            int signOfNumerator = numerator > 0 ? 1 : -1;
            int signOfDenominator = denominator > 0 ? 1 : -1;
            numerator *= signOfNumerator;
            denominator *= signOfDenominator;

            Dictionary<long, int> results = new Dictionary<long, int>();
            StringBuilder chars = new StringBuilder();
            long temp = numerator;
            int indexOfDigit = -1;
            while (temp != 0)
            {
                while (temp < denominator)
                {   
                    if (indexOfDigit == -1)
                    {
                        if (chars.Length == 0)
                            chars.Append("0.");
                        else
                            chars.Append(".");
                        indexOfDigit = chars.Length - 1;

                        if (!results.ContainsKey(temp))
                            results.Add(temp, chars.Length);
                        else
                        {
                            results[temp] = chars.Length;
                        }
                    }
                    else
                    {
                        chars.Append("0");
                    }
                    temp *= 10;
                }

                long result = temp / denominator;
                // remaining
                temp = temp % denominator;
                chars.Append(result);
                if (!results.ContainsKey(temp))
                {
                    results.Add(temp, chars.Length);
                }
                else
                {
                    break;
                }

                if (indexOfDigit != -1)
                    temp *= 10;            
            }

            if (temp != 0)
            {
                // found repeat
                int repeatIndex = results[temp];
                chars.Insert(repeatIndex, "(");
                chars.Append(")");

                if (signOfNumerator != signOfDenominator)
                    chars.Insert(0, "-");
                return chars.ToString();
            }
            else
            {
                if (chars.Length != 0)
                {
                    if (signOfNumerator != signOfDenominator)
                        chars.Insert(0, "-");
                    return chars.ToString();
                }

                return "0";
            }
        }
    }
}
