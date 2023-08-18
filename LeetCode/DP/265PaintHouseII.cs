using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _265PaintHouseII
    {
        public int MinCostII_BottomUp_MinSecMin_V2(int[][] costs)
        {
            int houses = costs.Length, colors = costs[0].Length;
            int prevMinCost = 0, secPrevMinCost = 0, prevMinColor = -1;
            for(int house = houses - 1; house >= 0; --house)
            {
                int minCost = int.MaxValue, secMinCost = int.MaxValue, minColor = -1;
                for(int color = 0; color < colors; color++)
                {
                    int cost = costs[house][color];
                    if (color != prevMinColor)
                        cost += prevMinCost;
                    else
                        cost += secPrevMinCost;

                    if (cost < minCost)
                    {
                        secMinCost = minCost;
                        minCost = cost;
                        minColor = color;
                    }
                    else if (cost < secMinCost)
                    {
                        secMinCost = cost;
                    }
                }

                prevMinColor = minColor;
                prevMinCost = minCost;
                secPrevMinCost = secMinCost;
            }

            return prevMinCost;
        }

        public int MinCostII_BottomUp_MinSecMin(int[][] costs)
        {
            int houses = costs.Length, colors = costs[0].Length;
            int[] dp = new int[colors], prevDp = new int[colors];
            int[] minColors = new int[houses + 1];
            for (int house = houses - 1; house >= 0; --house)
            {
                int cost = int.MaxValue;
                for (int color = 0; color < colors; ++color)
                {
                    int minCostOfNextHouseWithDiffColor = int.MaxValue;
                    if (color != minColors[house + 1])
                    {
                        minCostOfNextHouseWithDiffColor = prevDp[minColors[house + 1]];
                    }
                    else
                    {
                        for (int nextColor = 0; nextColor < colors; ++nextColor)
                        {
                            if (nextColor != color)
                                minCostOfNextHouseWithDiffColor = Math.Min(minCostOfNextHouseWithDiffColor, prevDp[nextColor]);
                        }
                    }

                    dp[color] = costs[house][color] + minCostOfNextHouseWithDiffColor;
                    if (dp[color] < cost)
                    {
                        cost = dp[color];
                        minColors[house] = color;
                    }
                }

                var temp = prevDp;
                prevDp = dp;
                dp = temp;
            }

            int minCost = int.MaxValue;
            for (int color = 0; color < colors; ++color)
            {
                minCost = Math.Min(minCost, prevDp[color]);
            }

            return minCost;
        }

        public int MinCostII_BottomUp_1D(int[][] costs)
        {
            int houses = costs.Length, colors = costs[0].Length;
            int[] dp = new int[colors], prevDp = new int[colors];
            for (int house = houses - 1; house >= 0; --house)
            {
                for (int color = 0; color < colors; ++color)
                {
                    int minCostOfNextHouseWithDiffColor = int.MaxValue;
                    for (int nextColor = 0; nextColor < colors; ++nextColor)
                    {
                        if (nextColor != color)
                            minCostOfNextHouseWithDiffColor = Math.Min(minCostOfNextHouseWithDiffColor, prevDp[nextColor]);
                    }

                    dp[color] = costs[house][color] + minCostOfNextHouseWithDiffColor;
                }

                var temp = prevDp;
                prevDp = dp;
                dp = temp;
            }

            int minCost = int.MaxValue;
            for (int color = 0; color < colors; ++color)
            {
                minCost = Math.Min(minCost, prevDp[color]);
            }

            return minCost;
        }

        public int MinCostII_BottomUp(int[][] costs)
        {
            int houses = costs.Length, colors = costs[0].Length;
            int[,] dp = new int[houses + 1, colors];
            for(int house = houses - 1; house >= 0; --house)
            {
                for(int color = 0; color < colors; ++color)
                {
                    int minCostOfNextHouseWithDiffColor = int.MaxValue;
                    for(int nextColor = 0; nextColor < colors; ++nextColor)
                    {
                        if (nextColor != color)
                            minCostOfNextHouseWithDiffColor = Math.Min(minCostOfNextHouseWithDiffColor, dp[house + 1, nextColor]);
                    }

                    dp[house, color] = costs[house][color] + minCostOfNextHouseWithDiffColor;
                }
            }

            int minCost = int.MaxValue;
            for(int color = 0; color < colors; ++color)
            {
                minCost = Math.Min(minCost, dp[0, color]);
            }

            return minCost;
        }

        public int MinCostII_V1(int[][] costs)
        {
            int[,] cache = new int[costs.Length, costs[0].Length + 1];
            return MinCost_TopDown(costs, 0, costs[0].Length, cache);
        }

        private int MinCost_TopDown(int[][] costs, int houseIndex, int colorUsed, int[,] cache)
        {
            if (houseIndex >= costs.Length)
                return 0;

            if (cache[houseIndex, colorUsed] == 0)
            {
                int minCost = int.MaxValue;
                for(int color = 0; color < costs[houseIndex].Length; ++color)
                {
                    if (color != colorUsed)
                    {
                        minCost = Math.Min(minCost, costs[houseIndex][color] + MinCost_TopDown(costs, houseIndex + 1, color, cache));
                    }
                }

                cache[houseIndex, colorUsed] = minCost;
            }

            return cache[houseIndex, colorUsed];
        }
    }
}
