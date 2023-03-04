using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _746MinCostsClimbingStairs
    {
        public int MinCostClimbingStairsBottomUp(int[] cost)
        {
            if (cost.Length <= 1)
                return 0;

            int up1 = cost[0], up2 = cost[1];
            for(int i = 2; i <= cost.Length; ++i)
            {
                int upI = Math.Min(up1, up2) + (i != cost.Length ? cost[i] : 0);
                up1 = up2;
                up2 = upI;
            }

            return up2;
        }

        public int MinCostClimbingStairsDPTopDown(int[] cost)
        {
            //int[] dp = new int[cost.Length];
            //dp[cost.Length - 1] = cost[cost.Length - 1];
            //dp[cost.Length - 2] = cost[cost.Length - 2];
            int dp2 = cost[cost.Length - 1];
            int dp1 = cost[cost.Length - 2];
            for (int index = cost.Length - 3; index >= 0; --index)
            {
                int dp = Math.Min(dp1, dp2) + cost[index];                
                    dp2 = dp1;
                    dp1 = dp;
                
                //dp[index] = Math.Min(cost1, cost2) + cost[index];
            }

            return Math.Min(dp1, dp2);
            //return Math.Min(dp[0], dp[1]);
        }
    }
}
