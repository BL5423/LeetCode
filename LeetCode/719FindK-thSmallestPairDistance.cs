using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _719FindK_thSmallestPairDistance
    {
        public int SmallestDistancePair(int[] nums, int k)
        {
            Array.Sort(nums);
            int startD = 0, endD = nums[nums.Length - 1] - nums[0];
            while (startD < endD)
            {
                int dis = startD + ((endD - startD) >> 1);
                int left = 0, countOfPairs = 0;
                for(int right = 1; right < nums.Length; ++right)
                {
                    while (nums[right] - nums[left] > dis)
                        ++left;

                    // # of pairs whose difference are less or equal to dis
                    countOfPairs += right - left;
                }

                if (countOfPairs >= k)
                    endD = dis;
                else // countOfPairs < k
                    startD = dis + 1;
            }

            // startD == endD
            return endD;
        }
    }
}
