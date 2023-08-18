using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _518CoinChangeII
    {
        public int Change(int amount, int[] coins)
        {
            int[] dp = new int[amount + 1];
            dp[0] = 1;

            foreach (var coin in coins)
            {
                for (int value = coin; value <= amount; ++value)
                {
                    dp[value] += dp[value - coin];
                }
            }

            return dp[amount];
        }

        //https://leetcode.com/problems/coin-change-ii/discuss/176706/Beginner-Mistake%3A-Why-an-inner-loop-for-coins-doensn't-work-Java-Soln

        public int ChangeV1(int amount, int[] coins)
        {
            int[] dp = new int[amount + 1];
            // amount 0 only has 1 combination(no coin at all)
            dp[0] = 1;
            foreach(int coin in coins)
            {
                for(int sum = coin; sum <= amount; ++sum)
                {
                    // if there is any combination(C) with amount equal to sum - coin, then we can add the current coin to C to have a new set of combinations with amount equal to sum
                    if (dp[sum - coin] != 0)
                    {
                        // total # of combinations with amount equal to sum - coin
                        int total = dp[sum - coin];

                        // total # of combinations with amount equal to sum now has more results from above(with one more extra coin)
                        dp[sum] += total;
                    }
                }
            }

            return dp[amount];
        }

        public int Change_DP_2D(int amount, int[] coins)
        {
            if (amount <= 0)
                return 0;

            int[,] dp = new int[coins.Length + 1, amount + 1];
            dp[0, 0] = 1; // base case
            for(int i = 1; i <= coins.Length; ++i)
            {
                int coin = coins[i - 1];
                for(int k = 0; k <= amount; ++k)
                {
                    // without coin
                    dp[i, k] = dp[i - 1, k];
                    if (k >= coin)
                    {
                        // with coin(can have dup coins)
                        dp[i, k] += dp[i, k - coin];
                    }
                }
            }

            return dp[coins.Length, amount] != 0 ? dp[coins.Length, amount] : 0;
        }
    }
}
