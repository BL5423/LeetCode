using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _300LongestIncreasingSubsequence
    {
        public int LengthOfLIS(int[] nums)
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
