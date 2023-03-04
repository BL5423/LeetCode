using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _322CoinChange
    {
        public int CoinChange(int[] coins, int amount)
        {
            // dp[a] is the minimal # of coins we need to make up for amount a
            int[] dp = new int[amount + 1];
            dp[0] = 0;
            for (int value = 1; value <= amount; ++value)
            {
                dp[value] = -1;
            }

            for (int index = 0; index < coins.Length; ++index)
            {
                int coin = coins[index];
                for (int value = coin; value <= amount; ++value)
                {
                    if (dp[value - coin] >= 0)
                    {
                        // the # of coins(including coin) we need to make up for amount value is the minimal of:
                        // the # of coins(without coin) we need to make up for amount value 
                        // the # of coins we need to make up for (amount - value of coin) + 1(the coin itself)
                        if (dp[value] > 0)
                            dp[value] = Math.Min(dp[value - coin] + 1, dp[value]);
                        else
                            dp[value] = dp[value - coin] + 1;
                    }
                }
            }

            return dp[amount] >= 0 ? dp[amount] : -1;
        }

        public int CoinChange_DP_2D(int[] coins, int amount)
        {
            if (amount <= 0)
                return 0;

            int MAX = amount + 1;
            int[,] dp = new int[coins.Length + 1, amount + 1];
            for (int k = 1; k <= amount; ++k)
            {
                dp[0, k] = MAX;
            }

            for(int i = 1; i <= coins.Length; ++i)
            {
                int coin = coins[i - 1];
                for(int k = 0; k <= amount; ++k)
                {
                    dp[i, k] = dp[i - 1, k];
                    if (k >= coin)
                        dp[i,k] = Math.Min(dp[i, k], dp[i, k - coin] + 1);
                }
            }

            return dp[coins.Length, amount] != MAX ? dp[coins.Length, amount] : -1;
        }

        public int CoinChangeBFSv2(int[] coins, int amount)
        {
            // BFS, bottom to top
            Queue<(int, int)> queue = new Queue<(int, int)>();
            HashSet<int> inQueue = new HashSet<int>();
            queue.Enqueue((0, 0));
            inQueue.Add(0);
            while(queue.Count > 0)
            {
                var head = queue.Dequeue();
                int value = head.Item1;
                int countOfCoins = head.Item2;
                if (value == amount)
                    return countOfCoins;

                foreach(var coin in coins)
                {
                    int nextValue = value + coin;
                    if (nextValue <= amount && !inQueue.Contains(nextValue))
                    {
                        queue.Enqueue((nextValue, countOfCoins + 1));
                        inQueue.Add(nextValue);
                    }
                }
            }

            return -1;
        }

        public int CoinChangeBFSv1(int[] coins, int amount)
        {
            // top to bottom
            Queue<(int, int)> queue = new Queue<(int, int)>();
            HashSet<int> inQueue = new HashSet<int>();
            queue.Enqueue((amount, 0));
            inQueue.Add(amount);
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                int valueLeft = head.Item1;
                int countOfCoins = head.Item2;
                if (valueLeft == 0)
                    return countOfCoins;

                foreach(var coin in coins)
                {
                    int nextValue = valueLeft - coin;
                    if (nextValue >= 0 && !inQueue.Contains(nextValue))
                    {
                        queue.Enqueue((nextValue, countOfCoins + 1));
                        inQueue.Add(nextValue);
                    }
                }
            }

            return -1;
        }

        public int CoinChangeV2(int[] coins, int amount)
        {
            // Bottom to top solution(iterative from 1 to amount)
            int[] results = new int[amount + 1];            
            results[0] = 0;
            for(int value = 1; value <= amount; ++value)
            {
                int result = int.MaxValue;
                foreach(var coin in coins)
                {
                    // CoinChange(value) == 1 + min[CoinChange(value - coin)], where coin is in coins
                    var valueLeft = value - coin;
                    if (valueLeft > 0 && results[valueLeft] > 0)
                    {
                        result = Math.Min(results[valueLeft], result);
                    }
                    else if (valueLeft == 0)
                    {
                        result = 0;
                    }
                }

                if (result != int.MaxValue)
                {
                    results[value] = result + 1;
                }
                else
                {
                    // no solution
                    results[value] = -1;
                }
            }

            return results[amount];
        }

        public int CoinChangeV1(int[] coins, int amount)
        {
            // Top to bottom solution(like a tree)
            // CoinChange(amount) == 1 + min[CoinChange(amount - coin)], where coin is in coins
            int[] results = new int[amount + 1];
            return FindNumberOfCoins(coins, amount, results);
        }

        private int FindNumberOfCoins(int[] coins, int amount, int[] results)
        {
            if (amount == 0)
            {
                return 0;
            }
            else if (amount < 0)
            {
                return -1;
            }
            else if (results[amount] != 0)
            {
                return results[amount];
            }
            else
            {
                int result = int.MaxValue;
                foreach(var coin in coins)
                {
                    int amountLeft = amount - coin;
                    int r = FindNumberOfCoins(coins, amountLeft, results);
                    if (r >= 0)
                    {
                        result = Math.Min(result, r);
                    }
                }

                if (result != int.MaxValue)
                    results[amount] = result + 1;
                else
                    results[amount] = -1;
            }

            return results[amount];
        }

        public int CoinChangeV0(int[] coins, int amount)
        {
            // exceeds time limit
            if (amount == 0)
                return 0;

            Dictionary<int, int>[] coinsToCount = new Dictionary<int, int>[coins.Length];
            Array.Sort(coins);
            for(int index = 0; index < coins.Length; ++index)
            {
                int coin = coins[index];
                coinsToCount[index] = new Dictionary<int, int>();                
                int count = amount / coin;
                int amountPerCoin = count * coin;
                while (amountPerCoin >= 0)
                {
                    coinsToCount[index].Add(amountPerCoin, count);
                    --count;
                    amountPerCoin -= coin;
                }
            }

            Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
            int firstCoin = coins.Length - 1;
            while (firstCoin >= 0 && queue.Count == 0)
            {
                foreach (var count in coinsToCount[firstCoin])
                {
                    if (count.Key <= amount)
                    {
                        int amountLeft = amount - count.Key;
                        int nextCoin = firstCoin - 1;
                        int currentCount = count.Value;
                        queue.Enqueue((amountLeft, nextCoin, currentCount));
                    }
                }
                --firstCoin;
            }

            int minCoinCount = int.MaxValue;
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                int amountLeft = head.Item1;
                int coinIndex = head.Item2;
                int currentCount = head.Item3;
                if (amountLeft == 0)
                {
                    // found a solution
                    minCoinCount = Math.Min(minCoinCount, currentCount);
                }
                else if (coinIndex >= 0)
                {
                    if (coins[coinIndex] <= amountLeft)
                    {
                        foreach (var count in coinsToCount[coinIndex])
                        {
                            if (count.Key <= amountLeft)
                            {
                                int newAmountLeft = amountLeft - count.Key;
                                int nextCoin = coinIndex - 1;
                                int newCurrentCount = currentCount + count.Value;
                                if (newCurrentCount < minCoinCount)
                                {
                                    queue.Enqueue((newAmountLeft, nextCoin, newCurrentCount));
                                }
                            }
                        }
                    }
                    else // coins[coinIndex] > amountLeft
                    {
                        // jump to next coin
                        queue.Enqueue((amountLeft, coinIndex - 1, currentCount));
                    }
                }
            }

            return minCoinCount != int.MaxValue ? minCoinCount : -1;
        }

        public int CoinChangeBottomUp(int[] coins, int amount)
        {
            int[] results = new int[amount + 1];
            int value = 0;
            results[0] = 0;
            while (value++ < amount)
            {
                var numberOfCoins = int.MaxValue;
                foreach(var coin in coins)
                {
                    if (value >= coin && results[value - coin] >= 0)
                    {
                        numberOfCoins = Math.Min(results[value - coin], numberOfCoins);
                    }
                }

                if (numberOfCoins != int.MaxValue)
                    results[value] = numberOfCoins + 1;
                else
                    results[value] = -1;
            }

            return results[amount];
        }

        public int CoinChangeV3TopDown(int[] coins, int amount)
        {
            int[] results = new int[amount + 1];
            return ChangeRercusive(coins, amount, results);
        }

        private int ChangeRercusive(int[] coins, int amount, int[] results)
        {
            if (amount == 0)
                return 0;

            if (amount < 0)
                return -1;

            if (results[amount] != 0)
                return results[amount];

            int numberOfCoins = int.MaxValue;
            foreach(var coin in coins)
            {
                int changed = ChangeRercusive(coins, amount - coin, results);
                if (changed >= 0)
                {
                    numberOfCoins = Math.Min(changed, numberOfCoins);
                }
            }

            if (numberOfCoins != int.MaxValue)
            {
                results[amount] = numberOfCoins + 1;
            }
            else
            {
                results[amount] = -1;
            }

            return results[amount];
        }
    }
}
