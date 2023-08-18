using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _1473PaintHouseIII
    {
        private static int MaxCost = 10001;

        public int MinCost(int[] houseColors, int[][] costs, int houses, int colors, int target)
        {
            // dp[h, t, c] is the min cost to paint 0~h houses with h in c color of t neighbors in total
            int[,] prevDp = new int[target + 1, colors + 1];
            int[,] dp = new int[target + 1, colors + 1];
            for (int nb = 0; nb <= target; ++nb)
            {
                for (int c = 0; c <= colors; ++c)
                {
                    prevDp[nb, c] = MaxCost;
                    dp[nb, c] = MaxCost;
                }
            }

            for (int house = 0; house < houses; ++house)
            {
                for (int neighbors = 1; neighbors <= Math.Min(house + 1, target); ++neighbors)
                {
                    if (houseColors[house] == 0)
                    {
                        for (int color = 1; color <= colors; ++color)
                        {
                            int cost = MaxCost;
                            for (int prevColor = 1; prevColor <= colors; ++prevColor)
                            {
                                cost = Math.Min(cost, costs[house][color - 1] + (house - 1 >= 0 ? prevDp[color == prevColor ? neighbors : neighbors - 1, prevColor] : 0));
                            }

                            dp[neighbors, color] = cost;
                        }
                    }
                    else
                    {
                        int cost = MaxCost;
                        for (int prevColor = 1; prevColor <= colors; ++prevColor)
                        {
                            dp[neighbors, prevColor] = MaxCost;
                            cost = Math.Min(cost, house - 1 >= 0 ? prevDp[houseColors[house] == prevColor ? neighbors : neighbors - 1, prevColor] : 0);
                        }

                        dp[neighbors, houseColors[house]] = cost;
                    }
                }

                var temp = dp;
                dp = prevDp;
                prevDp = temp;
            }

            int minCost = MaxCost;
            for (int color = 1; color <= colors; ++color)
                minCost = Math.Min(minCost, prevDp[target, color]);

            return minCost != MaxCost ? minCost : -1;
        }

        public int MinCostV2(int[] houseColors, int[][] costs, int houses, int colors, int target)
        {
            // dp[h, t, c] is the min cost to paint 0~h houses with h in c color of t neighbors in total
            int[,,] dp = new int[houses + 1, target + 1, colors + 1];           
            for(int h = 0; h < houses; h++)
            {
                for(int nb = 0; nb <=target; ++nb)
                {
                    for (int c = 0; c <= colors; ++c)
                        dp[h, nb, c] = MaxCost;
                }
            }

            for(int house = 0; house < houses; ++house)
            {
                for (int neighbors = 1; neighbors <= Math.Min(house + 1, target); ++neighbors)
                {
                    if (houseColors[house] == 0)
                    {
                        for (int color = 1; color <= colors; ++color)
                        {
                            int cost = MaxCost;
                            for (int prevColor = 1; prevColor <= colors; ++prevColor)
                            {
                                cost = Math.Min(cost, costs[house][color - 1] + (house - 1 >= 0 ? dp[house - 1, color == prevColor ? neighbors : neighbors - 1, prevColor] : 0));
                            }

                            dp[house, neighbors, color] = cost;
                        }
                    }
                    else
                    {
                        int cost = MaxCost;
                        for (int prevColor = 1; prevColor <= colors; ++prevColor)
                        {
                            cost = Math.Min(cost, house - 1 >= 0 ? dp[house - 1, houseColors[house] == prevColor ? neighbors : neighbors - 1, prevColor] : 0);
                        }

                        dp[house, neighbors, houseColors[house]] = cost;
                    }
                }
            }

            int minCost = MaxCost;
            for (int color = 1; color <= colors; ++color)
                minCost = Math.Min(minCost, dp[houses - 1, target, color]);

            return minCost != MaxCost ? minCost : -1;
        }

        public int MinCostV1(int[] houseColors, int[][] costs, int houses, int colors, int target)
        {
            int[,,] cache = new int[houses + 1, target + 1, colors + 1];            
            int cost = this.MinCost_TopDown(houseColors, costs, 0, 0, 0, target, cache);
            return cost != MaxCost ? cost : -1;
        }

        private int MinCost_TopDown(int[] houseColors, int[][] costs, int houseIndex, int targetsSoFar, int prevColor, int targets, int[,,] cache)
        {
            if (targetsSoFar > targets)
                return MaxCost;
            if (houseIndex == houseColors.Length)
            {
                return targetsSoFar == targets ? 0 : MaxCost;
            }

            if (cache[houseIndex, targetsSoFar, prevColor] == 0)
            {
                int minCost = MaxCost;
                if (houseColors[houseIndex] == 0)
                {
                    for(int color = 1; color <= costs[houseIndex].Length; ++color)
                    {
                        minCost = Math.Min(minCost, costs[houseIndex][color - 1] + 
                            MinCost_TopDown(houseColors, costs, houseIndex + 1, color != prevColor ? targetsSoFar + 1 : targetsSoFar, color, targets, cache));
                    }
                }
                else
                {
                    minCost = MinCost_TopDown(houseColors, costs, houseIndex + 1, 
                        houseColors[houseIndex] != prevColor ? targetsSoFar + 1 : targetsSoFar, houseColors[houseIndex], targets, cache);
                }

                cache[houseIndex, targetsSoFar, prevColor] = minCost;
            }

            return cache[houseIndex, targetsSoFar, prevColor];
        }

        private int MinCost_TopDownV1(int[] houseColors, int[][] costs, int houseIndex, int targetsSoFar, int curColor, int targets, int[,,] cache)
        {
            if (targetsSoFar > targets)
            {
                return MaxCost;
            }
            if (houseIndex >= houseColors.Length)
            {
                return 0;
            }

            if (houseColors[houseIndex] == 0)
            {
                if (cache[houseIndex, targetsSoFar, curColor] == 0)
                {
                    int minCost = MaxCost;
                    int cost = costs[houseIndex][curColor - 1];
                    for (int nextColor = 1; nextColor <= costs[houseIndex].Length; ++nextColor)
                    {
                        if (curColor != nextColor)
                        {
                            minCost = Math.Min(minCost, cost + MinCost_TopDown(houseColors, costs, houseIndex + 1, targetsSoFar + 1, nextColor, targets, cache));
                        }
                        else
                        {
                            minCost = Math.Min(minCost, cost + MinCost_TopDown(houseColors, costs, houseIndex + 1, targetsSoFar, nextColor, targets, cache));
                        }
                    }

                    cache[houseIndex, targetsSoFar, curColor] = minCost;
                }

                return cache[houseIndex, targetsSoFar, curColor];
            }
            else
            {
                // do not need to paint the current house
                curColor = houseColors[houseIndex];
                if (cache[houseIndex, targetsSoFar, curColor] == 0)
                {
                    // skip to the next house to paint
                    int prevColor = curColor;
                    int nextHouseIndex = houseIndex + 1;
                    int updatedTargets = targetsSoFar;
                    while(nextHouseIndex < houseColors.Length)
                    {
                        if (houseColors[nextHouseIndex] == 0)
                            break;
                        if (prevColor != houseColors[nextHouseIndex])
                            ++updatedTargets;

                        prevColor = houseColors[nextHouseIndex];
                        ++nextHouseIndex;
                    }

                    int minCost = MaxCost;
                    if (nextHouseIndex < houseColors.Length && updatedTargets <= targets)
                    {
                        for (int nextColor = 1; nextColor <= costs[houseIndex].Length; ++nextColor)
                        {
                            if (nextColor != curColor)
                            {
                                minCost = Math.Min(minCost, MinCost_TopDown(houseColors, costs, nextHouseIndex, updatedTargets + 1, nextColor, targets, cache));
                            }
                            else
                            {
                                minCost = Math.Min(minCost, MinCost_TopDown(houseColors, costs, nextHouseIndex, updatedTargets, nextColor, targets, cache));
                            }
                        }
                    }

                    cache[houseIndex, targetsSoFar, curColor] = minCost;
                }

                return cache[houseIndex, targetsSoFar, curColor];
            }
        }
    }
}
