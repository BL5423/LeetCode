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
            if (a == 0) return b;
            if (b == 0) return a;
            if (a == 0 && b == 1) return 1;
            if (a == 1 && b == 0) return 1;
            if (a == 1 && b == 1) return 2;
            if (a == 0 && b == -1) return -1;
            if (a == -1 && b == 0) return -1;
            if (a == -1 && b == -1) return -2;

            return GetSum(a^b, (a&b)<<1);

            // a + b = (a/2 + b/2) * 2
            //int bitA = a & 1;
            //int bitB = b & 1;
            //int halfA = a >> 1;
            //int halfB = b >> 1;
            //return GetSum(halfA, halfB) + GetSum(halfA + bitA, halfB + bitB);
        }
    }
}
