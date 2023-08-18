using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _309BestTimetoBuyandSellStockwithCooldown
    {
        public int MaxProfit_BottomUp(int[] prices)
        {
            int totalDays = prices.Length;
            int dp_NotHolding1 = 0, dp_NotHolding2 = 0, dp_Holding1 = 0;
            for (int day = totalDays - 1; day >= 0; --day)
            {
                int dp_Holding = 0, dp_NotHolding = 0;
                for (int holding = 0; holding < 2; ++holding)
                {
                    int doNothing = holding == 1 ? dp_Holding1 : dp_NotHolding1;
                    int doSomething = 0;
                    if (holding == 1)
                    {
                        // can only sell
                        doSomething = prices[day] + dp_NotHolding2;

                        dp_Holding = Math.Max(doNothing, doSomething);
                    }
                    else
                    {
                        // can only buy
                        doSomething = -prices[day] + dp_Holding1;

                        dp_NotHolding = Math.Max(doNothing, doSomething);
                    }
                }

                dp_Holding1 = dp_Holding;
                dp_NotHolding2 = dp_NotHolding1;
                dp_NotHolding1 = dp_NotHolding;
            }

            return dp_NotHolding1;
        }

        public int MaxProfit_BottomUp_V1(int[] prices)
        {
            int totalDays = prices.Length;
            int[,] dp = new int[totalDays + 2, 2];
            for(int day = totalDays - 1; day >= 0; --day)
            {
                for(int holding = 0; holding < 2; ++holding)
                {
                    int doNothing = dp[day + 1, holding];
                    int doSomething = 0;
                    if (holding == 1)
                    {
                        // can only sell
                        doSomething = prices[day] + dp[day + 2, 0];
                    }
                    else
                    {
                        // can only buy
                        doSomething = -prices[day] + dp[day + 1, 1];
                    }

                    dp[day, holding] = Math.Max(doNothing, doSomething);
                }
            }

            return dp[0, 0];
        }

        public int MaxProfitV1(int[] prices)
        {
            int totalDays = prices.Length;
            int[,] cache = new int[totalDays + 1, 2];
            return MaxProfit_TopDown(prices, 1, 0, cache);
        }

        private int MaxProfit_TopDown(int[] prices, int day, int holding, int[,] cache)
        {
            // no more days
            if (day >= cache.GetLength(0))
                return 0;

            if (cache[day, holding] == 0)
            {
                int doNothing = MaxProfit_TopDown(prices, day + 1, holding, cache);
                int doSomething = 0;
                if (holding == 1)
                {
                    // can only sell
                    doSomething = prices[day] + MaxProfit_TopDown(prices, day + 2, 0, cache);
                }
                else
                {
                    // can only buy
                    doSomething = -prices[day] + MaxProfit_TopDown(prices, day + 1, 1, cache);
                }

                cache[day, holding] = Math.Max(doNothing, doSomething);
            }

            return cache[day, holding];
        }
    }
}
