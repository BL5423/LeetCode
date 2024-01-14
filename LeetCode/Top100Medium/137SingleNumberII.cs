using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public class _137SingleNumberII
    {
        public int SingleNumber(int[] nums)
        {
            int theNum = 0;
            for (int bit = 0; bit <= 32; ++bit)
            {
                int op = 0;
                foreach (var num in nums)
                {
                    op += (num >> bit) & 1;
                }

                // for any number that appears 3 times, module 3 will erase the bit of it, just like XOR
                theNum |= ((op % 3) << bit);
            }

            return theNum;
        }
    }
}
