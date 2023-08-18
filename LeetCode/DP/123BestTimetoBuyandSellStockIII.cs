using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _123BestTimetoBuyandSellStockIII
    {
        public int MaxProfit_BottomUp_SpaceOptimized(int[] prices)
        {
            int maxTransactions = 2;
            int[,] prevDp = new int[2, maxTransactions + 1];
            int[,] dp = new int[2, maxTransactions + 1];
            for (int day = prices.Length - 1; day >= 0; --day)
            {
                for (int holding = 0; holding < 2; ++holding)
                {
                    for (int transactions = 0; transactions < maxTransactions; ++transactions)
                    {
                        int doNothing = prevDp[holding, transactions], doSomething = 0;
                        if (holding == 0)
                        {
                            // can only buy
                            doSomething = -prices[day] + prevDp[1, transactions];
                        }
                        else // holding == 1
                        {
                            // can only sell
                            doSomething = prices[day] + prevDp[0, transactions + 1];
                        }

                        dp[holding, transactions] = Math.Max(doNothing, doSomething);
                    }
                }

                var temp = prevDp;
                prevDp = dp;
                dp = temp;
            }

            return prevDp[0, 0];
        }

        public int MaxProfit_BottomUp(int[] prices)
        {
            int maxTransactions = 2;
            int[,,] dp = new int[prices.Length + 1, 2, maxTransactions + 1];
            for(int day = prices.Length - 1; day >= 0; --day)
            {
                for(int holding = 0; holding < 2; ++holding)
                {
                    for(int transactions = 0; transactions < maxTransactions; ++transactions)
                    {
                        int doNothing = dp[day + 1, holding, transactions], doSomething = 0;
                        if (holding == 0)
                        {
                            // can only buy
                            doSomething = -prices[day] + dp[day + 1, 1, transactions];
                        }
                        else // holding == 1
                        {
                            // can only sell
                            doSomething = prices[day] + dp[day + 1, 0, transactions + 1];
                        }

                        dp[day, holding, transactions] = Math.Max(doNothing, doSomething);
                    }
                }
            }

            return dp[0, 0, 0];
        }

        public int MaxProfit_TopDown(int[] prices)
        {
            int[,,] cache = new int[prices.Length, 2, 2];
            for(int d = 0; d < prices.Length; ++d)
            {
                for(int h = 0; h < 2; ++h)
                {
                    for (int t = 0; t < 2; ++t)
                        cache[d, h, t] = -1;
                }
            }
            return MaxProfit_TopDown(prices, 0, 0, 0, cache);
        }

        private int MaxProfit_TopDown(int[] prices, int day, int holding, int transactions, int[,,] cache)
        {
            if (day >= prices.Length || transactions >= 2)
                return 0;

            if (cache[day, holding, transactions] == -1)
            {
                int doNothing = MaxProfit_TopDown(prices, day + 1, holding, transactions, cache);
                int doSomething;
                if (holding == 0)
                {
                    // can only buy
                    doSomething = -prices[day] + MaxProfit_TopDown(prices, day + 1, holding: 1, transactions, cache);
                }
                else
                {
                    // can only sell
                    doSomething = prices[day] + MaxProfit_TopDown(prices, day + 1, holding: 0, transactions + 1, cache);
                }

                cache[day, holding, transactions] = Math.Max(doNothing, doSomething);
            }

            return cache[day, holding, transactions];
        }
    }
}
