using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    // similar problem about prefix sum : https://leetcode.com/problems/contiguous-array/
    public class _560SubarraySumEqualsK
    {
        public int SubarraySum(int[] nums, int k)
        {
            Dictionary<int, int> prevSums = new Dictionary<int, int>(nums.Length);
            prevSums[0] = 1;
            int sum = 0, counts = 0;
            for(int index = 0; index < nums.Length; ++index)
            {
                sum += nums[index];

                // a potential previous sum
                int prevSum = sum - k;
                if (prevSums.TryGetValue(prevSum, out int count))
                {
                    counts += count;
                }

                if (prevSums.TryGetValue(sum, out int c))
                {
                    ++prevSums[sum];
                }
                else
                {
                    prevSums[sum] = 1;
                }
            }

            return counts;
        }

        public int SubarraySumV1(int[] nums, int k)
        {
            Dictionary<int, int> sumToCount = new Dictionary<int, int>(nums.Length);
            int sum = 0, count = 0;
            for(int i = 0; i < nums.Length; ++i)
            {
                sum += nums[i];
                if (sum == k)
                {
                    ++count;
                }

                // prevSum is a potential sum we have encoutered before
                int prevSum = sum - k;
                if (sumToCount.TryGetValue(prevSum, out int c))
                {
                    // if prevSum did appear before then we know the difference between sum and prevSum is k
                    // we just add the frequency(which indicates the different sub arrays) of prevSum to the results
                    count += c;
                }

                // put the current sum into the dictionary to track its frequency
                if (sumToCount.TryGetValue(sum, out c))
                {
                    ++sumToCount[sum];
                }
                else
                {
                    sumToCount.Add(sum, 1);
                }
            }

            return count;
        }
    }
}
