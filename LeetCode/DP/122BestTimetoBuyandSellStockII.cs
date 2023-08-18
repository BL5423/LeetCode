using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _122BestTimetoBuyandSellStockII
    {
        public int MaxProfit_DP_ConstantSpace(int[] prices)
        {
            int dpHolding1 = 0, dpNotHolding1 = 0;
            for(int day = prices.Length - 1; day >= 0; --day)
            {
                int profitFromSell = prices[day] + dpNotHolding1;
                int profitFromBuy = -prices[day] + dpHolding1;

                int dpHolding = Math.Max(profitFromSell, dpHolding1);
                int dpNotHolding = Math.Max(profitFromBuy, dpNotHolding1);

                dpHolding1 = dpHolding;
                dpNotHolding1 = dpNotHolding;
            }

            return dpNotHolding1;
        }

        public int MaxProfit_DP(int[] prices)
        {
            int[,] dp = new int[prices.Length + 1, 2];
            for(int day = prices.Length - 1; day >= 0; --day)
            {
                for(int holding = 0; holding < 2; ++holding)
                {
                    int doNothing = dp[day + 1, holding];
                    int doSomething = 0;
                    if (holding == 1)
                    {
                        // can only sell
                        doSomething = prices[day] + dp[day + 1, 0];
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

        public int MaxProfit_Greedy(int[] prices)
        {
            int profit = 0;
            for(int day = 0; day < prices.Length - 1; ++day)
            {
                if (prices[day] < prices[day + 1])
                {
                    // buy at today and sell tomorrow
                    profit += prices[day + 1] - prices[day];
                }
            }

            return profit;
        }
    }
}
