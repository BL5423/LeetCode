using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _371Sum
    {
        public int GetSum(int a, int b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            int negative = 1;
            if (a < 0 && b < 0)
            {
                negative = -1;
            }
            else if (a > 0 && b > 0)
            {
            }
            else if (a < 0 && b > 0)
            {
                return GetSum(b, a);
            }
            else // a > 0 && b < 0
            {
                //negative = a < Math.Abs(b) ? -1 : 1;
            }

            bool substract = a > 0 && b < 0;
            a = Math.Abs(a);
            b = Math.Abs(b);
            int sum = 0;
            if (!substract)
            {
                while (b != 0)
                {
                    int s = a ^ b;
                    int carry = (a & b) << 1;
                    a = s;
                    b = carry;
                }

                sum = a;
            }
            else
            {
                while (b != 0)
                {
                    int s = a ^ b;
                    // ab        borrow
                    // 1          0
                    // 0

                    // 1          0
                    // 1

                    // 0          0
                    // 0 

                    // 0          1
                    // 1
                    int borrow = (~a & b) << 1;
                    a = s;
                    b = borrow;
                }

                sum = a;
            }

            return sum * negative;
        }

        public int GetSumV1(int a, int b)
        {
            if (a == 0) return b;
            if (b == 0) return a;
            if (a == 0 && b == 1) return 1;
            if (a == 1 && b == 0) return 1;
            if (a == 1 && b == 1) return 2;
            if (a == 0 && b == -1) return -1;
            if (a == -1 && b == 0) return -1;
            if (a == -1 && b == -1) return -2;

            //a     0   1
            //b     1   1
            //a^b   1   0 is exactly a + b without considering carry 
            //a&b   0   1 (<<1  = 1   0) is carries of a + b
            return GetSumV1(a^b, (a&b)<<1);

            // a + b = (a/2 + b/2) * 2
            //int bitA = a & 1;
            //int bitB = b & 1;
            //int halfA = a >> 1;
            //int halfB = b >> 1;
            //return GetSum(GetSum(halfA, halfB), GetSum(halfA + bitA, halfB + bitB));
            //return GetSum(GetSum(halfA, halfB) * 2, GetSum(bitA, bitB));
        }
    }
}
