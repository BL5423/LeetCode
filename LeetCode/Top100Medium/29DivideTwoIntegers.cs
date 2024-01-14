using ConsoleApp2.Level3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public class _29DivideTwoIntegers
    {
        public int Divide(int dividend, int divisor)
        {
            if (dividend == int.MinValue && divisor == -1)
                return int.MaxValue;

            const int UpperBound = int.MinValue >> 1;
            int negatives = 2;
            if (dividend > 0)
            {
                --negatives;
                dividend = -dividend;
            }
            if (divisor > 0)
            {
                --negatives;
                divisor = -divisor;
            }

            int maxDoubledDivisor = divisor;
            int maxPowerOf2 = 0;
            while (maxDoubledDivisor >= UpperBound && maxDoubledDivisor + maxDoubledDivisor >= dividend)
            {
                maxDoubledDivisor += maxDoubledDivisor;
                maxPowerOf2 += 1;
            }

            int quotient = 0;
            while (divisor >= dividend)
            {
                if (dividend <= maxDoubledDivisor)
                {
                    dividend -= maxDoubledDivisor;
                    quotient += (1 << maxPowerOf2);
                }

                maxDoubledDivisor >>= 1;
                --maxPowerOf2;
            }

            return negatives == 1 ? -quotient : quotient;
        }

        public static int DivideV1(int dividend, int divisor)
        {
            if (dividend == divisor)
                return 1;
            if (divisor == 1)
                return dividend;
            if (divisor == -1)
                return dividend != int.MinValue ? -dividend : int.MaxValue;
            if (divisor == int.MinValue)
                return dividend != int.MinValue ? 0 : 1;

            int offset = 0;
            bool negative = (dividend >= 0 && divisor < 0) || (dividend < 0 && divisor >= 0);
            if (dividend == int.MinValue)
            {
                // make sure abs of dividend is less or equal to int.MaxValue
                dividend += Math.Abs(divisor);
                offset = 1;
            }

            divisor = Math.Abs(divisor);
            dividend = Math.Abs(dividend);

            // assume both of them are positive numbers
            int divisor_msb = MostSiginicantBit(divisor);
            int dividend_msb = MostSiginicantBit(dividend);
            int left = 1, right = (1 << (dividend_msb - divisor_msb + 1)) - 1, res = -1;
            while (left <= right)
            {
                int mid = left + ((right - left) >> 1);
                int mul = Multiply(divisor, mid);
                if (mul < 0 || mul < divisor || mul < mid)
                {
                    // overflow
                    right = mid - 1;
                }
                else if (mul <= dividend)
                {
                    res = mid;
                    left = mid + 1;
                }
                else if (mul > dividend)
                {
                    right = mid - 1;
                }
            }

            // left > right
            return (negative ? -1 : 1) *
                (offset + (res != -1 ? res : right));
        }

        private static int MostSiginicantBit(int num)
        {
            for (int bit = 31; bit > 0; --bit)
            {
                if ((num & (1 << bit)) != 0)
                    return bit;
            }

            return 0;
        }

        private static int Multiply(int divisor, int number)
        {
            int res = 0;
            int @base = 0;
            while (number != 0)
            {
                if ((number & 1) == 1)
                {
                    res += (divisor << @base);
                }

                number >>= 1;
                ++@base;
            }

            return res;
        }
    }
}
