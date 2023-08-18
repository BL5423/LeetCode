using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public class _334IncreasingTripletSubsequence
    {
        public bool IncreasingTriplet(int[] nums)
        {
            if (nums.Length < 3)
                return false;

            // dp[i] is the smallest number where the increasing subsequence of length i ends
            int[] dp = new int[3];
            dp[1] = nums[0];
            int maxLen = 1;
            for(int i = 1; maxLen < 3 && i < nums.Length; ++i)
            {
                int left = 1, right = maxLen, index = -1;
                while (left <= right)
                {
                    int mid = left + ((right - left) / 2);
                    if (dp[mid] >= nums[i])
                    {
                        // a potential pos for nums[i] in dp
                        index = mid;
                        right = mid - 1;
                    }
                    else // dp[mid] < nums[i]
                    {
                        left = mid + 1;
                    }
                }

                // left > right
                if (index != -1)
                {
                    // put nums[i] into the proper pos
                    dp[index] = nums[i];
                }
                else
                {
                    // found a longer increasing subsequence
                    dp[++maxLen] = nums[i];
                }
            }

            return maxLen == 3;
        }

        public bool IncreasingTripletV2(int[] nums)
        {
            int smallest = int.MaxValue, smaller = int.MaxValue;
            foreach (var num in nums)
            {
                if (smallest > num)
                    smallest = num;
                else if (num > smallest && smaller > num)
                    smaller = num;
                else if (num > smallest && num > smaller)
                    return true;
            }

            return false;
        }

        public bool IncreasingTripletV1(int[] nums)
        {
            if (nums.Length < 3)
                return false;

            // Similar to Kadane, but instead of finding the subarray with max sum, we look for the min&max num on the left&right from a given index
            int[] minFromLeft = new int[nums.Length];
            int[] maxFromRight = new int[nums.Length];
            minFromLeft[0] = nums[0];
            maxFromRight[nums.Length - 1] = nums[nums.Length - 1];
            for(int i = 1; i < nums.Length; ++i)
            {
                minFromLeft[i] = Math.Min(nums[i], minFromLeft[i - 1]);
                maxFromRight[nums.Length - i - 1] = Math.Max(nums[nums.Length - i - 1], maxFromRight[nums.Length - i]);
            }

            for(int i = 1; i < nums.Length -1; ++i)
            {
                if (minFromLeft[i - 1] < nums[i] && nums[i] < maxFromRight[i + 1])
                    return true;
            }

            return false;
        }
    }
}
