using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _523ContinuousSubarraySum
    {
        public bool CheckSubarraySum(int[] nums, int k)
        {
            int prefixSum = 0;
            var seen = new Dictionary<int, int>();
            seen.Add(0, -1);
            for (int i = 0; i < nums.Length; ++i)
            {
                prefixSum += nums[i];
                int remaining = prefixSum % k;
                if (seen.TryGetValue(remaining, out int index)) // seen another prefix sum with the same remaining
                {
                    if (i > index + 1) // at least two numbers in between
                        return true;
                }
                else
                    seen.Add(remaining, i);
            }

            return false;
        }
    }
}
