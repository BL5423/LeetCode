using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _1770MaximumScorefromPerformingMultiplicationOperations
    {
        public int MaximumScore_BottomUp_1D_V2(int[] nums, int[] multipliers)
        {
            int[] dp = new int[multipliers.Length + 1];
            for (int operationsLeft = 1; operationsLeft <= multipliers.Length; ++operationsLeft)
            {
                int operationIndex = multipliers.Length - operationsLeft;
                int dp11 = dp[operationIndex + 1];
                for (int left = operationIndex; left >= 0; --left)
                {
                    int point = Math.Max(multipliers[operationIndex] * nums[left] + dp11,
                                         multipliers[operationIndex] * nums[nums.Length - 1 - (operationIndex - left)] + dp[left]);

                    dp11 = dp[left];
                    dp[left] = point;
                }
            }

            return dp[0];
        }

        public int MaximumScore_BottomUp_1D(int[] nums, int[] multipliers)
        {
            int[] dp = new int[multipliers.Length + 1];
            for (int operationsDone = multipliers.Length - 1; operationsDone >= 0; --operationsDone)
            {
                int dp11 = dp[operationsDone + 1];
                for (int left = operationsDone; left >= 0; --left)
                {
                    int point = Math.Max(multipliers[operationsDone] * nums[left] + dp11,
                                         multipliers[operationsDone] * nums[nums.Length - 1 - (operationsDone - left)] + dp[left]);

                    dp11 = dp[left];
                    dp[left] = point;
                }
            }

            return dp[0];
        }

        public int MaximumScore_BottomUp(int[] nums, int[] multipliers)
        {
            int[,] dp = new int[multipliers.Length + 1, multipliers.Length + 1];
            for(int operationsDone = multipliers.Length - 1; operationsDone >= 0; --operationsDone)
            {
                for(int left = operationsDone; left >= 0; --left)
                {
                    dp[operationsDone, left] = Math.Max(multipliers[operationsDone] * nums[left] + dp[operationsDone + 1, left + 1],
                                                        multipliers[operationsDone] * nums[nums.Length - 1 - (operationsDone - left)] + dp[operationsDone + 1, left]);
                }
            }

            return dp[0, 0];
        }

        public int MaximumScore_TopDown(int[] nums, int[] multipliers)
        {
            int[,] cache = new int[multipliers.Length, multipliers.Length];
            return this.MaximumScore(nums, 0, multipliers, 0, cache);
        }

        private int MaximumScore(int[] nums, int leftIndex, int[] multipliers, int operationsDone, int[,] cache)
        {
            if (operationsDone == multipliers.Length)
                return 0;

            if (cache[operationsDone, leftIndex] == 0)
            {
                cache[operationsDone, leftIndex] = Math.Max(multipliers[operationsDone] * nums[leftIndex] + MaximumScore(nums, leftIndex + 1, multipliers, operationsDone + 1, cache),
                                                            multipliers[operationsDone] * nums[nums.Length - 1 - (operationsDone - leftIndex)] + MaximumScore(nums, leftIndex, multipliers, operationsDone + 1, cache));
            }

            return cache[operationsDone, leftIndex];
        }
    }
}
