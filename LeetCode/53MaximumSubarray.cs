using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _53MaximumSubarray
    {
        public int MaxSubArray(int[] nums)
        {
            return MaxSubArray(nums, 0, nums.Length - 1);
        }

        private int MaxSubArray(int[] nums, int start, int end)
        {
            if (start == end)
                return nums[end];

            int mid = start + (end - start) / 2;
            int leftMax = MaxSubArray(nums, start, mid);
            int rightMax = MaxSubArray(nums, mid + 1, end);

            int maxFromMidLeft = 0, maxFromMidRight = 0, sum = 0;
            int leftIndex = mid - 1, rightIndex = mid + 1;
            while (leftIndex >= start)
            {
                sum += nums[leftIndex];
                maxFromMidLeft = Math.Max(maxFromMidLeft, sum);
                --leftIndex;
            }

            sum = 0;
            while (rightIndex <= end)
            {
                sum += nums[rightIndex];
                maxFromMidRight = Math.Max(maxFromMidRight, sum);
                ++rightIndex;
            }

            int maxSum = Math.Max(Math.Max(leftMax, rightMax), maxFromMidLeft + nums[mid] + maxFromMidRight);
            return maxSum;
        }

        public int MaxSubArrayV1(int[] nums)
        {
            int sum = 0, maxSum = int.MinValue;
            foreach(int num in nums)
            {
                sum += num;
                if (sum > maxSum)
                {
                    maxSum = sum;
                }
                if (sum < 0)
                {
                    sum = 0;
                }
            }

            return maxSum;
        }
    }
}
