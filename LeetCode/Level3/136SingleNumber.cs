using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _136SingleNumber
    {
        public int SingleNumber(int[] nums)
        {
            int n = nums[0];
            for(int i = 1; i < nums.Length; ++i)
            {
                n ^= nums[i];
            }

            return n;
        }
    }
}
