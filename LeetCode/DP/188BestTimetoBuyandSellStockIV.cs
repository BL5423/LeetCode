using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _188BestTimetoBuyandSellStockIV
    {
        public int MaxProfit_Merge(int k, int[] prices)
        {
            if (prices.Length <= 0 || k <= 0)
            {
                return 0;
            }

            LinkedList<int[]> list = new LinkedList<int[]>();
            int left = 0, right = 0;
            for(int i = 1; i < prices.Length; ++i)
            {
                if (prices[i] >= prices[i - 1])
                {
                    right = i;
                }
                else 
                {
                    if (right > left)
                        list.AddLast(new int[] { left, right });
                 
                    left = i;
                }
            }

            if (right > left)
                list.AddLast(new int[] { left, right });

            while (list.Count > k)
            {
                int min_profitloss_to_delete = int.MaxValue;
                LinkedListNode<int[]> transaction_to_delete = null;
                var transaction = list.First;
                while (transaction != null)
                {
                    int profitloss = prices[transaction.Value[1]] - prices[transaction.Value[0]];
                    if (profitloss < min_profitloss_to_delete)
                    {
                        min_profitloss_to_delete = profitloss;
                        transaction_to_delete = transaction;
                    }

                    transaction = transaction.Next;
                }

                int min_profit_to_merge = int.MaxValue;
                LinkedListNode<int[]> transaction_to_merge = null;
                transaction = list.First.Next;
                while (transaction != null)
                {
                    var priorTransaction = transaction.Previous;
                    var currentTransaction = transaction;
                    int profitloss = prices[priorTransaction.Value[1]] - prices[currentTransaction.Value[0]];
                    if (profitloss < min_profit_to_merge)
                    {
                        min_profit_to_merge = profitloss;
                        transaction_to_merge = transaction;
                    }

                    transaction = transaction.Next;
                }

                if (min_profitloss_to_delete <= min_profit_to_merge)
                {
                    list.Remove(transaction_to_delete);
                }
                else
                {
                    var previous = transaction_to_merge.Previous;
                    previous.Value[1] = transaction_to_merge.Value[1];
                    list.Remove(transaction_to_merge);
                }
            }

            int totalProfit = 0;
            foreach(var transaction in list)
            {
                totalProfit += (prices[transaction[1]] - prices[transaction[0]]);
            }

            return totalProfit;
        }

        public int MaxProfit_BottomUp_1D(int k, int[] prices)
        {
            int days = prices.Length;
            int[] dpHolding = new int[k + 1];
            int[] dpNotHolding = new int[k + 1];
            for (int day = days - 1; day >= 0; --day)
            {
                //for (int transactionsLeft = 1; transactionsLeft <= k; ++transactionsLeft)
                for (int transactionsLeft = k; transactionsLeft >= 1; --transactionsLeft)
                {
                    for (int hold = 0; hold < 2; ++hold)
                    {
                        int doNothing = 0, doSomething = 0;
                        if (hold == 1)
                        {
                            doNothing = dpHolding[transactionsLeft];

                            // can only sell
                            doSomething = prices[day] + dpNotHolding[transactionsLeft - 1];

                            dpHolding[transactionsLeft] = Math.Max(doNothing, doSomething);
                        }
                        else
                        {
                            doNothing = dpNotHolding[transactionsLeft];

                            // can only buy
                            doSomething = -prices[day] + dpHolding[transactionsLeft];

                            dpNotHolding[transactionsLeft] = Math.Max(doNothing, doSomething);
                        }
                    }
                }
            }

            return dpNotHolding[k];
        }

        public int MaxProfit_BottomUp(int k, int[] prices)
        {
            int days = prices.Length;
            int[,,] dp = new int[days + 1, k + 1, 2];
            for(int day = days - 1; day >= 0; --day)
            {
                for(int transactionsLeft = 1; transactionsLeft <= k; ++transactionsLeft)
                {
                    for (int hold = 0; hold < 2; ++hold)
                    {
                        int doNothing = dp[day + 1, transactionsLeft, hold];
                        int doSomething = 0;
                        if (hold == 1)
                        {
                            // can only sell
                            doSomething = prices[day] + dp[day + 1, transactionsLeft - 1, 0];
                        }
                        else
                        {
                            // can only buy
                            doSomething = -prices[day] + dp[day + 1, transactionsLeft, 1];
                        }

                        dp[day, transactionsLeft, hold] = Math.Max(doNothing, doSomething);
                    }
                }
            }

            return dp[0, k, 0];
        }

        public int MaxProfit_TopDown(int k, int[] prices)
        {
            int days = prices.Length;
            int transactions = k;
            int[,,] cache = new int[days, transactions + 1, 2];
            return this.BuyOrSell(0, transactions, 0, prices, cache);
        }

        private int BuyOrSell(int day, int transactionsLeft, int hold, int[] prices, int[,,] cache)
        {
            if (day == cache.GetLength(0) || transactionsLeft == 0)
                return 0;

            if (cache[day, transactionsLeft, hold] == 0)
            {
                int doNothing = BuyOrSell(day + 1, transactionsLeft, hold, prices, cache);
                int doSomething = 0;

                if (hold == 1)
                {
                    // can only sell
                    doSomething = prices[day] + BuyOrSell(day + 1, transactionsLeft - 1, 0, prices, cache);
                }
                else
                {
                    // can only buy
                    doSomething = -prices[day] + BuyOrSell(day + 1, transactionsLeft, 1, prices, cache);
                }

                cache[day, transactionsLeft, hold] = Math.Max(doNothing, doSomething);
            }

            return cache[day, transactionsLeft, hold];
        }
    }
}
