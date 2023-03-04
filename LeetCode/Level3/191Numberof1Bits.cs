using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _191Numberof1Bits
    {
        public int HammingWeight(uint n)
        {
            int[] bits = new int[] { 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4 };
            int ones = 0;
            while (n != 0)
            {
                var value = (n & 15);
                ones += bits[value];
                n >>= 4;
            }

            return ones;
        }
    }
}
