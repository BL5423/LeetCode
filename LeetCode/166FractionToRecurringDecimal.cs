using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _166FractionToRecurringDecimal
    {
        // converting numerator to long so that it won't overflow int.MinValue especially
        public string FractionToDecimal(long numerator, int denominator)
        {
            if (numerator == 0)
                return "0";

            int negatives = 2;
            if (numerator >= 0)
            {
                --negatives;
                numerator = -numerator;
            }
            if (denominator >= 0)
            {
                --negatives;
                denominator = -denominator;
            }

            LinkedList<string> list = new LinkedList<string>();
            if (numerator <= denominator)
            {
                list.AddLast((numerator / denominator).ToString());
                numerator %= denominator;
            }

            Dictionary<long, LinkedListNode<string>> seen = new Dictionary<long, LinkedListNode<string>>();
            if (numerator != 0)
            {
                if (list.Count != 0)
                    list.AddLast(".");
                else
                    list.AddLast("0.");

                seen.Add(numerator, list.Last);
            }

            // numerator is less than denominator since here
            while (numerator != 0)
            {
                // if numerator is not long, operation below would overflow
                numerator *= 10;

                if (numerator > denominator)
                {
                    list.AddLast("0");
                }
                else
                {
                    long m = numerator / denominator;
                    list.AddLast(m.ToString());
                    numerator %= denominator;

                    if (seen.TryGetValue(numerator, out LinkedListNode<string> node))
                    {
                        list.AddAfter(node, new LinkedListNode<string>("("));
                        list.AddLast(")");
                        break;
                    }
                    else
                    {
                        seen.Add(numerator, list.Last);
                    }
                }
            }

            if (negatives == 1)
            {
                list.AddFirst("-");
            }

            return string.Join(string.Empty, list);
        }

        public string FractionToDecimalV1(long numerator, long denominator)
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
