using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public class _260SingleNumberIII
    {
        public int[] SingleNumber(int[] nums)
        {
            int xor = 0;
            foreach (var num in nums)
            {
                xor ^= num;
            }

            // get the last bits start with 1
            int mask = xor & (-xor);
            int[] res = new int[2];
            foreach (var num in nums)
            {
                if ((num & mask) == 0)
                    res[0] ^= num;
            }

            res[1] = xor ^ res[0];
            return res;
        }
    }
}
