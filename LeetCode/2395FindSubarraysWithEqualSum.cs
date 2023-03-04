using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _2395FindSubarraysWithEqualSum
    {
        public bool FindSubarrays(int[] nums)
        {
            HashSet<int> seen = new HashSet<int>(nums.Length / 2);

            for(int index = 1; index < nums.Length; ++index)
            {
                int sum = nums[index] + nums[index - 1];
                if (!seen.Add(sum))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
