using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _300LongestIncreasingSubsequence
    {
        public int LengthOfLIS_BinarySearch(int[] nums)
        {
            int[] dp = new int[nums.Length];
            int maxLen = 1;
            dp[0] = nums[0];
            for(int i = 1; i < nums.Length; ++i)
            {
                int left = 0, right = maxLen - 1, index = -1;
                while (left <= right)
                {
                    int mid = left + ((right - left) >> 1);
                    if (dp[mid] >= nums[i])
                    {
                        index = mid;
                        right = mid - 1;
                    }
                    else // dp[mid] < nums[i]
                    {
                        left = mid + 1;
                    }
                }

                // left > right
                if (index != - 1)
                {
                    // still within the boundary
                    dp[index] = nums[i];
                }
                else
                {
                    // nums[i] is the largest num so far and it falls on the right most pos of dp
                    dp[left] = nums[i];
                    ++maxLen;
                }
            }

            return maxLen;
        }

        public int LengthOfLIS_DP(int[] nums)
        {
            int maxLen = 0;
            int[] dp = new int[nums.Length];
            dp[0] = 1;
            for (int i = 1; i < nums.Length; ++i)
            {
                dp[i] = 1;
                for (int j = i - 1; j >= 0; --j)
                {
                    if (nums[i] > nums[j])
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                    }
                }

                if (dp[i] > maxLen)
                    maxLen = dp[i];
            }

            return maxLen;
        }

        public int LengthOfLISV1(int[] nums)
        {
            // result[k] is the # of items < nums[k] in the original nums
            int[] results = new int[nums.Length];
            for(int index = nums.Length - 1; index >= 0; --index)
            {
                int k = index;
                results[index] = 1;
                int max = 0;
                while (++k < nums.Length && (nums.Length - k) > max)
                {
                    if (nums[index] < nums[k] && results[k] > max)
                    {
                        max = results[k];
                    }
                }

                results[index] += max;
            }

            return results.Max();
        }

        public int LengthOfLISDPandBS(int[] nums)
        {
            int[] dp = new int[nums.Length];
            int size = 1;
            dp[0] = nums[0];
            for(int i = 1; i < nums.Length; ++i)
            {
                if (nums[i] < dp[0])
                {
                    dp[0] = nums[i];
                }
                else if (nums[i] > dp[size-1])
                {
                    dp[size++] = nums[i];
                }
                else
                {
                    // nums[i] falls in between of the last items of these temp LIS, then find its ceil and overwrite
                    // so that we can have potentially longer LIS with a smaller end item(nums[i])
                    int pos = BinarySearch(dp, -1, size - 1, nums[i]);
                    int pos1 = BinarySearch1(dp, 0, size - 1, nums[i]);
                    Console.WriteLine("pos: {0}, pos1 : {1}", pos, pos1);
                    //int pos = Array.BinarySearch(dp, 0, size - 1, nums[i]);
                    //if (pos == -1) pos = ~pos;
                    dp[pos] = nums[i];
                }
            }

            return size;
        }

        private int BinarySearch(int[] dp, int start, int end, int number)
        {
            while (start < end - 1)
            {
                int middle = (start + end) / 2;
                if (dp[middle] >= number)
                {
                    end = middle;
                }
                else if (dp[middle] < number)
                {
                    start = middle;
                }
            }

            return end;
        }

        private int BinarySearch1(int[] dp, int start, int end, int number)
        {
            while (start < end)
            {
                int middle = (start + end) / 2;
                if (dp[middle] >= number)
                {
                    // do not move end ahead of middle since middle might be the postion we want to insert
                    end = middle;
                }
                else if (dp[middle] < number)
                {
                    start = middle + 1;
                }
            }

            return end;
        }
    }
}
