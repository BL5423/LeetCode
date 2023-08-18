using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _740DeleteAndEarn
    {
        public int DeleteAndEarn(int[] nums)
        {
            int max = int.MinValue;
            var points = new Dictionary<int, int>(nums.Length);
            foreach(var num in nums)
            {
                if (max < num)
                    max = num;

                if (!points.TryGetValue(num, out int p))
                {
                    points.Add(num, 0);
                }

                points[num] = p + num;
            }

            int dp2 = 0, dp1 = 0;
            if (points.TryGetValue(1, out int point))
            {
                dp1 = point;
            }

            for (int i = 2; i <= max; ++i)
            {
                points.TryGetValue(i, out int p);
                int dp = Math.Max(p + dp2, dp1);
                dp2 = dp1;
                dp1 = dp;
            }

            return dp1;
        }

        public int DeleteAndEarnV1(int[] nums)
        {
            var counts = new Dictionary<int, int>(nums.Length);
            foreach(var num in nums)
            {
                if (!counts.TryGetValue(num, out int count))
                {
                    counts.Add(num, 0);
                }

                ++counts[num];
            }

            var keys = counts.Keys.ToArray();
            Array.Sort(keys);
            int[] dp = new int[keys.Length + 1];
            dp[keys.Length] = 0;
            dp[keys.Length - 1] = keys.Last() * counts[keys.Last()];
            for (int i = keys.Length - 2; i >= 0; --i)
            {
                int key = keys[i];
                int priorKey = keys[i + 1];
                if (key + 1 == priorKey)
                    dp[i] = Math.Max(key * counts[key] + dp[i + 2], dp[i + 1]);
                else
                    dp[i] = key * counts[key] + dp[i + 1];
            }

            return dp[0];
        }
    }
}
