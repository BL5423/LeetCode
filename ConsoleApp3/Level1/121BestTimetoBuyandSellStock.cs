using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _121BestTimetoBuyandSellStock
    {
        public int MaxProfit(int[] prices)
        {
            int min = int.MaxValue;
            int profit = 0;
            for(int index = 0; index < prices.Length; ++index)
            {
                var price = prices[index];
                if (price < min)
                {
                    min = price;
                }
                else if (price > min)
                {
                    profit = Math.Max(profit, price - min);
                }
            }

            return profit;
        }
    }
}
