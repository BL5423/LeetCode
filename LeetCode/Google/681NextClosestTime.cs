using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _681NextClosestTime
    {
        public string NextClosestTime(string time)
        {
            int[] digits = new int[4];
            digits[3] = time[4] - '0';
            digits[2] = time[3] - '0';
            digits[1] = time[1] - '0';
            digits[0] = time[0] - '0';

            int digitToIndex = -1, digitFromIndex = -1;
            for (int i = digits.Length - 1; digitToIndex == -1 && i >= 0; --i)
            {
                int digit = digits[i];

                // try to find the minimal number other than digit, that is larger than digit
                int minimalViable = int.MaxValue;
                for (int j = digits.Length - 1; j >= 0; --j)
                {
                    if (digits[j] < minimalViable && digits[j] > digit)
                    {
                        digits[i] = digits[j];
                        if (IsValid(digits))
                        {
                            minimalViable = digits[j];
                            digitToIndex = i;
                            digitFromIndex = j;
                        }

                        // undo and keep finding better number
                        digits[i] = digit;
                    }
                }
            }

            int min = digits.Min();
            if (digitToIndex == -1)
            {
                digits[0] = min;
                digits[1] = min;
                digits[2] = min;
                digits[3] = min;
            }
            else
            {
                digits[digitToIndex] = digits[digitFromIndex];
                for (int i = digitToIndex + 1; i < digits.Length; ++i)
                    digits[i] = min;
            }

            return string.Format("{0}{1}:{2}{3}", digits[0], digits[1], digits[2], digits[3]);
        }

        private static bool IsValid(int[] digits)
        {
            if (digits[0] >= 3)
                return false;
            if (digits[0] == 2 && digits[1] >= 4)
                return false;
            if (digits[2] >= 6)
                return false;

            return true;
        }
    }
}
