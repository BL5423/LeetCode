using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _714BestTimetoBuyandSellStockwithTransactionFee
    {
        public int MaxProfit_BottomUp_1D(int[] prices, int fee)
        {
            int dp_nextDay_holding = 0, dp_nextDay_notHolding = 0;
            for (int day = prices.Length - 1; day >= 0; --day)
            {
                for (int holding = 0; holding < 2; ++holding)
                {
                    int doNothing, doSomething;
                    if (holding == 0)
                    {
                        doNothing = dp_nextDay_notHolding;

                        // can only buy
                        doSomething = dp_nextDay_holding - prices[day];

                        int dp = Math.Max(doNothing, doSomething);
                        dp_nextDay_notHolding = dp;
                    }
                    else
                    {
                        doNothing = dp_nextDay_holding;

                        // can only sell and pay the transaction fee
                        doSomething = dp_nextDay_notHolding + prices[day] - fee;

                        int dp = Math.Max(doNothing, doSomething);
                        dp_nextDay_holding = dp;
                    }
                }
            }

            return dp_nextDay_notHolding;
        }

        public int MaxProfit_BottomUp(int[] prices, int fee)
        {
            int[,] dp = new int[prices.Length + 1, 2];
            for(int day = prices.Length - 1; day >= 0; --day)
            {
                for(int holding = 0; holding < 2; ++holding)
                {
                    int doNothing = dp[day + 1, holding], doSomething;
                    if (holding == 0)
                    {
                        // can only buy
                        doSomething = dp[day + 1, 1] - prices[day];
                    }
                    else
                    {
                        // can only sell and pay the transaction fee
                        doSomething = dp[day + 1, 0] + prices[day] - fee;
                    }

                    dp[day, holding] = Math.Max(doNothing, doSomething);
                }
            }

            return dp[0, 0];
        }

        public int MaxProfit_TopDown(int[] prices, int fee)
        {
            int[,] cache = new int[prices.Length, 2];
            return MaxProfit(prices, fee, 0, 0, cache);
        }

        private int MaxProfit(int[] prices, int fee, int day, int holding, int[,] cache)
        {
            if (day >= prices.Length)
                return 0;

            if (cache[day, holding] == 0)
            {
                int doNothing = MaxProfit(prices, fee, day + 1, holding, cache);
                int doSomething = 0;
                if (holding == 1)
                {
                    // can only sell and pay the transaction fee
                    doSomething = prices[day] + MaxProfit(prices, fee, day + 1, 0, cache) - fee;
                }
                else
                {
                    // can only buy
                    doSomething = -prices[day] + MaxProfit(prices, fee, day + 1, 1, cache);
                }

                cache[day, holding] = Math.Max(doNothing, doSomething);
            }

            return cache[day, holding];
        }
    }
}
