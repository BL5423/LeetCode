using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _718MaximumLengthofRepeatedSubarray
    {
        public int FindLength_Sliding(int[] nums1, int[] nums2)
        {
            int len = 0;
            for(int step = 0; step < nums1.Length + nums2.Length - 1; ++step)
            {
                int index1 = Math.Max(0, nums1.Length - 1 - step);
                int index2 = Math.Max(0, step - nums1.Length + 1);
                int curLen = 0;
                while (index1 < nums1.Length && index2 < nums2.Length)
                {
                    if (nums1[index1++] == nums2[index2++])
                    {
                        len = Math.Max(len, ++curLen);
                    }
                    else
                    {
                        // reset
                        curLen = 0;
                    }
                }
            }

            return len;
        }

        public int FindLength_BottomUp_1D_V2(int[] nums1, int[] nums2)
        {
            if (nums1.Length * nums2.Length == 0)
                return 0;

            if (nums1.Length < nums2.Length)
                return FindLength_BottomUp_1D_V2(nums2, nums1);

            int max = int.MinValue;
            // dp[i, j] is the max length of prefix of nums1[i...n-1] and nums2[j...m-1]
            int[] dp = new int[nums2.Length + 1];
            int prev = 0;
            for (int i = nums1.Length - 1; i >= 0; --i)
            {
                prev = dp[nums2.Length];
                for (int j = nums2.Length - 1; j >= 0; --j)
                {
                    int len = 0;
                    if (nums1[i] == nums2[j])
                        len = 1 + prev;

                    prev = dp[j];
                    dp[j] = len;
                    max = Math.Max(dp[j], max);
                }
            }

            return max;
        }

        public int FindLength_BottomUp_1D(int[] nums1, int[] nums2)
        {
            if (nums1.Length * nums2.Length == 0)
                return 0;

            if (nums1.Length < nums2.Length)
                return FindLength_BottomUp_1D(nums2, nums1);

            int max = int.MinValue;
            // dp[i, j] is the max length of prefix of nums1[i...n-1] and nums2[j...m-1]
            int[] dp = new int[nums2.Length + 1];
            int[] prevDp = new int[nums2.Length + 1];
            for (int i = nums1.Length - 1; i >= 0; --i)
            {
                for (int j = nums2.Length - 1; j >= 0; --j)
                {
                    dp[j] = 0;
                    if (nums1[i] == nums2[j])
                        dp[j] = 1 + prevDp[j + 1];
                    
                    max = Math.Max(dp[j], max);
                }

                int[] temp = prevDp;
                prevDp = dp;
                dp = temp;
            }

            return max;
        }

        public int FindLength_BottomUp(int[] nums1, int[] nums2)
        {
            if (nums1.Length * nums2.Length == 0)
                return 0;

            int max = int.MinValue;
            // dp[i, j] is the max length of prefix of nums1[i...n-1] and nums2[j...m-1]
            int[,] dp = new int[nums1.Length + 1, nums2.Length + 1];
            for (int i = nums1.Length - 1; i >=0; --i)
            {
                for(int j = nums2.Length - 1; j >= 0; --j)
                {
                    if (nums1[i] == nums2[j])
                        dp[i, j] = 1 + dp[i + 1, j + 1];
                    else
                        dp[i, j] = 0;

                    max = Math.Max(dp[i, j], max);
                }
            }
            return max;
        }

        private int maxLen = int.MinValue;

        public int FindLength(int[] nums1, int[] nums2)
        {
            if (nums1.Length * nums2.Length == 0)
                return 0;

            int[,] cache = new int[nums1.Length, nums2.Length];
            for(int i = 0; i < nums1.Length; ++i)
            {
                for (int j = 0; j < nums2.Length; ++j)
                    cache[i, j] = -1;
            }

            FindLength_TopDown(nums1, nums2, 0, 0, cache);
            return maxLen;
        }

        private int FindLength_TopDown(int[] nums1, int[] nums2, int index1, int index2, int[,] cache)
        {
            // run out of nums
            if (index1 >= nums1.Length || index2 >= nums2.Length)
                return 0;

            if (cache[index1, index2] == -1)
            {
                cache[index1, index2] = 0;
                if (nums1[index1] == nums2[index2])
                    cache[index1, index2] = 1 + FindLength_TopDown(nums1, nums2, index1 + 1, index2 + 1, cache);

                FindLength_TopDown(nums1, nums2, index1 + 1, index2, cache);
                FindLength_TopDown(nums1, nums2, index1, index2 + 1, cache);
            }

            maxLen = Math.Max(maxLen, cache[index1, index2]);
            return cache[index1, index2];
        }
    }
}
