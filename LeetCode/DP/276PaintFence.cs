using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _276PaintFence
    {
        public int NumWays_BottomUp_Constant(int n, int k)
        {
            int dp1 = k * k, dp2 = k;
            if (n == 1)
                return dp2;
            if (n == 2)
                return dp1;

            for (int i = n - 3; i >= 0; --i)
            {
                int dp = (k - 1) * dp1 + (k - 1) * dp2;
                dp2 = dp1;
                dp1 = dp;
            }

            return dp1;
        }

        public int NumWays(int n, int k)
        {
            int[] cache = new int[n];
            return NumWays_TopDown(n, k, 0, cache);
        }

        private int NumWays_TopDown(int n, int k, int postIndex, int[] cache)
        {
            // base states
            if (postIndex == n - 1)
                return k;
            if (postIndex == n - 2)
                return k * k;

            if (cache[postIndex] == 0)
            {
                                   // paint i post differently as to i + 1
                cache[postIndex] = (k - 1) * NumWays_TopDown(n, k, postIndex + 1, cache) + 

                                   // paint i post same as to i + 1 only when i + 1 and i + 2 are different
                                   (k - 1) * NumWays_TopDown(n, k, postIndex + 2, cache);
            }

            return cache[postIndex];
        }

        public int NumWays_BottomUp(int n, int k)
        {
            int[] dp = new int[n];

            if (n - 1 >= 0)
                dp[n - 1] = k;
            if (n - 2 >= 0)
                dp[n - 2] = k * k;

            for(int i = n - 3; i >= 0; --i)
            {
                dp[i] = (k - 1) * dp[i + 1] + (k - 1) * dp[i + 2];
            }

            return dp[0];
        }
    }
}
