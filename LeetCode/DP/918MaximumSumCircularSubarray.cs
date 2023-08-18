using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _918MaximumSumCircularSubarray
    {
        public int MaxSubarraySumCircular_InverseKadane(int[] nums)
        {
            int minSumSoFar = 0, minSum = int.MaxValue, maxSumSoFar = 0, maxSum = int.MinValue, sum = 0;
            foreach(var num in nums)
            {
                minSumSoFar = Math.Min(num, minSumSoFar + num);
                minSum = Math.Min(minSum, minSumSoFar);

                maxSumSoFar = Math.Max(num, maxSumSoFar + num);
                maxSum = Math.Max(maxSum, maxSumSoFar);

                sum += num;
            }

            return minSum != sum ? Math.Max(maxSum, sum - minSum) : maxSum;
        }

        public int MaxSubarraySumCircular_Kadane(int[] nums)
        {
            int sumSoFar = 0, maxSum = int.MinValue;
            foreach(var num in nums)
            {
                sumSoFar = Math.Max(num, sumSoFar + num);
                maxSum = Math.Max(maxSum, sumSoFar);
            }

            int[] suffixSums = new int[nums.Length];
            suffixSums[nums.Length - 1] = nums[nums.Length - 1];
            sumSoFar = nums[nums.Length - 1];
            for (int index = nums.Length - 2; index >= 0; --index)
            {
                sumSoFar += nums[index];
                suffixSums[index] = Math.Max(sumSoFar, suffixSums[index + 1]);
            }

            sumSoFar = 0;
            for(int index = 0; index < nums.Length - 1; ++index)
            {
                sumSoFar += nums[index];
                maxSum = Math.Max(sumSoFar + suffixSums[index + 1], maxSum);
            }

            return maxSum;
        }

        public int MaxSubarraySumCircularV1(int[] nums)
        {
            // TLE
            int maxSumSoFar = int.MinValue;
            for(int index = 0; index < nums.Length; ++index)
            {
                maxSumSoFar = Math.Max(maxSumSoFar, nums[index]);
                if (nums[index] > 0)
                {
                    maxSumSoFar = Math.Max(maxSumSoFar, MaxSubarray(nums, index));
                }
            }

            return maxSumSoFar;
        }

        private int MaxSubarray(int[] nums, int startIndex)
        {
            int curIndex = startIndex, sumSoFar = 0, maxSum = int.MinValue;
            do
            {
                sumSoFar = Math.Max(sumSoFar + nums[curIndex], nums[curIndex]);
                maxSum = Math.Max(maxSum, sumSoFar);
                curIndex = (++curIndex) % nums.Length;
            } while (curIndex != startIndex);

            return maxSum;
        }
    }
}
