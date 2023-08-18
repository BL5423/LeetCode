using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _256PaintHouse
    {
        public int MinCost_BottomUp_OptimziedSpace(int[][] costs)
        {
            int houses = costs.Length;
            int colors = costs[0].Length;
            int[] dp = new int[colors];
            for (int house = houses - 1; house >= 0; --house)
            {
                int dp0 = costs[house][0] + Math.Min(dp[1], dp[2]);
                int dp1 = costs[house][1] + Math.Min(dp[2], dp[0]);
                int dp2 = costs[house][2] + Math.Min(dp[0], dp[1]);

                dp[0] = dp0;
                dp[1] = dp1;
                dp[2] = dp2;
            }

            return Math.Min(Math.Min(dp[0], dp[1]), dp[2]);
        }

        public int MinCost_BottomUp_1D(int[][] costs)
        {
            int houses = costs.Length;
            int colors = costs[0].Length;
            int[,] dp = new int[houses + 1, colors];
            for(int house = houses - 1; house >= 0; --house)
            {
                for(int color = 0; color < colors; ++color)
                {
                    dp[house, color] = costs[house][color] + Math.Min(dp[house + 1, (color + 1) % 3], dp[house + 1, (color + 2) % 3]);
                }
            }

            return Math.Min(Math.Min(dp[0, 0], dp[0, 1]), dp[0, 2]);
        }

        public int MinCost_TopDown(int[][] costs)
        {
            int[,] cache = new int[costs.Length, costs[0].Length + 1];
            return MinCost_TopDown(costs, 0, 3, cache);
        }

        private int MinCost_TopDown(int[][] costs, int houseIndex, int colorUsedByPrevHouse, int[,] cache)
        {
            if (cache[houseIndex, colorUsedByPrevHouse] == 0)
            {
                int minCost = int.MaxValue;
                if (houseIndex == costs.Length - 1)
                {
                    for (int i = 0; i < costs[houseIndex].Length; ++i)
                    {
                        if (i != colorUsedByPrevHouse && minCost > costs[houseIndex][i])
                            minCost = costs[houseIndex][i];
                    }
                }
                else
                {
                    for (int color = 0; color < costs[houseIndex].Length; ++color)
                    {
                        if (color != colorUsedByPrevHouse)
                        {
                            minCost = Math.Min(minCost, costs[houseIndex][color] + MinCost_TopDown(costs, houseIndex + 1, color, cache));
                        }
                    }
                }

                cache[houseIndex, colorUsedByPrevHouse] = minCost;
            }

            return cache[houseIndex, colorUsedByPrevHouse];
        }
    }
}
