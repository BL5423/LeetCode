using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _1043PartitionArrayforMaximumSum
    {
        public int MaxSumAfterPartitioning(int[] arr, int k)
        {
            return DP(arr, k);
        }

        private int DP(int[] arr, int k)
        {
            // dp[i] is the max sum from index + i + 1 in arr
            int[] dp = new int[k];
            dp[(arr.Length - 2) % k] = arr[arr.Length - 1]; // i starts with arr.Length - 2 and j starts with 0 on line 27
            for (int i = arr.Length - 2; i >= 0; --i)
            {
                int max_sofar = 0, max_sum = 0;
                for (int j = 0; j < k && i + j < arr.Length; ++j)
                {
                    max_sofar = Math.Max(max_sofar, arr[i + j]);
                    max_sum = Math.Max(max_sum, max_sofar * (j + 1) + dp[(i + j) % k]);
                }

                // result for i of next iteration on line 27, it will apprear in front of dp[i % k]
                dp[(i - 1 + k) % k] = max_sum;
            }

            return dp[(0 - 1 + k) % k];
        }

        private int DPV2(int[] arr, int k)
        {
            // dp[i] is the max sum from index + i + 1 in arr
            int[] dp = new int[k + 1];
            dp[0] = arr[arr.Length - 1];
            int[] nextDp = new int[k + 1];
            for (int i = arr.Length - 2; i >= 0; --i)
            {
                int max_sofar = 0, max_sum = 0;
                for (int j = 0; j < k && i + j < arr.Length; ++j)
                {
                    max_sofar = Math.Max(max_sofar, arr[i + j]);
                    max_sum = Math.Max(max_sum, max_sofar * (j + 1) + dp[j]);

                    nextDp[j + 1] = dp[j];
                }

                nextDp[0] = max_sum;
                var temp = dp;
                dp = nextDp;
                nextDp = temp;
            }

            return dp[0];
        }

        private int DPV1(int[] arr, int k)
        {
            // dp[i] is the max sum from index i given k
            int[] dp = new int[arr.Length + 1];
            // base case
            dp[arr.Length - 1] = arr[arr.Length - 1];
            for (int i = arr.Length - 2; i >= 0; --i)
            {
                int cur_max = 0;
                for (int j = 0; j < k && i + j < arr.Length; ++j)
                {
                    cur_max = Math.Max(cur_max, arr[i + j]);
                    dp[i] = Math.Max(dp[i], cur_max * (j + 1) + dp[i + j + 1]);
                }
            }

            return dp[0];
        }

        private Dictionary<int, int> cache = new Dictionary<int, int>();

        private int PartitionRecursively(int[] arr, int index, int k)
        {
            if (cache.TryGetValue(index, out int value))
                return value;
            if (index >= arr.Length)
                return 0;

            int cur_max = 0, cur_sum = 0;
            for (int i = index; i < Math.Min(index + k, arr.Length); ++i)
            {
                cur_max = Math.Max(cur_max, arr[i]);
                cur_sum = Math.Max(cur_sum,
                                   cur_max * (i - index + 1) + PartitionRecursively(arr, i + 1, k));
            }

            cache.Add(index, cur_sum);
            return cur_sum;
        }
    }
}
