using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _983MinimumCostForTickets
    {
        public int MincostTickets(int[] days, int[] costs)
        {
            int[,] dp = new int[days.Length, costs.Length];
            for(int c = 0; c < costs.Length; ++c)
            {
                dp[days.Length - 1, c] = costs[c];
            }

            for(int day = days.Length - 2; day >= 0; --day)
            {
                dp[day, 0] = costs[0] + Math.Min(dp[day + 1, 0], Math.Min(dp[day + 1, 1], dp[day + 1, 2]));

                dp[day, 1] = costs[1];
                int expirationDay = day + 1;
                while (expirationDay < days.Length && days[expirationDay] < days[day] + 7)
                {
                    ++expirationDay;
                }
                if (expirationDay < days.Length)
                    dp[day, 1] += Math.Min(dp[expirationDay, 0], Math.Min(dp[expirationDay, 1], dp[expirationDay, 2]));

                dp[day, 2] = costs[2];
                while (expirationDay < days.Length && days[expirationDay] < days[day] + 30)
                {
                    ++expirationDay;
                }
                if (expirationDay < days.Length)
                    dp[day, 2] += Math.Min(dp[expirationDay, 0], Math.Min(dp[expirationDay, 1], dp[expirationDay, 2]));
            }

            int res = int.MaxValue;
            for (int c = 0; c < costs.Length; ++c)
            {
                res = Math.Min(res, dp[0, c]);
            }

            return res;
        }

        public int MincostTickets_TopDown(int[] days, int[] costs)
        {
            if (days.Length == 1)
                return costs.Min();

            int[,] cache = new int[days.Length, 3];
            return Mincost_TopDown(days, 0, costs, 0, cache);
        }

        private int Mincost_TopDown(int[] days, int day, int[] costs, int period, int[,] cache)
        {
            if (day >= days.Length)
                return 0;

            int periodLeft = period - (day - 1 >= 0 ? days[day] - days[day - 1] : period);
            if (day == days.Length - 1 && periodLeft >= 0)
                return 0;

            if (day > 0 && periodLeft >= 0)
            {
                // pass is still valid
                return Mincost_TopDown(days, day + 1, costs, periodLeft, cache);
            }
            else
            {
                // pass expired
                int cost1;
                if (cache[day, 0] == 0)
                {
                    cache[day, 0] = costs[0] + Mincost_TopDown(days, day + 1, costs, 0, cache);
                }
                cost1 = cache[day, 0];

                int cost2;
                if (cache[day, 1] == 0)
                {
                    cache[day, 1] = costs[1] + Mincost_TopDown(days, day + 1, costs, 6, cache);
                }
                cost2 = cache[day, 1];

                int cost3;
                if (cache[day, 2] == 0)
                {
                    cache[day, 2] = costs[2] + Mincost_TopDown(days, day + 1, costs, 29, cache);
                }
                cost3 = cache[day, 2];

                return Math.Min(cost1, Math.Min(cost2, cost3));
            }
        }
    }
}
