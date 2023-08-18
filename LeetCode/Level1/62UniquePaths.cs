using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _62UniquePaths
    {
        public int UniquePaths(int m, int n)
        {
            return (int)Math.Floor(0.5 + Factoria(m + n - 2) / (Factoria(n - 1) * Factoria(m - 1)));
        }

        private double Factoria(int x)
        {
            double f = 1;
            while (x > 1)
            {
                f *= x;
                --x;
            }
            return f;
        }

        public int UniquePaths_DP(int m, int n)
        {
            if (m > n)
                return UniquePaths_DP(n, m);

            int[] dpM = new int[m];
            int[] dp = new int[m];
            for (int i = 0; i < m; ++i)
            {
                // the right most path, downside only
                dpM[i] = 1;
            }
            dp[m - 1] = 1;

            for (int j = n - 2; j >= 0; --j)
            {
                for (int i = m - 2; i >= 0; --i)
                {
                    dp[i] = dpM[i] + dp[i + 1];
                }

                var temp = dpM;
                dpM = dp;
                dp = temp;
            }

            return dpM[0];
        }

        public int UniquePathsV1(int m, int n)
        {
            int[,] dp = new int[m, n];
            for(int i = m - 1; i >= 0; --i)
            {
                // the right most path, downside only
                dp[i, n - 1] = 1;
            }
            for(int j = n - 1; j >= 0; --j)
            {
                // the bottom path, rightside only
                dp[m - 1, j] = 1;
            }

            for(int j = n - 2; j >= 0; --j)
            {
                for(int i = m - 2; i >= 0; --i)
                {
                    // at each pos, only go right or down
                    dp[i, j] = dp[i + 1, j] + dp[i, j + 1];
                }
            }

            return dp[0, 0];
        }
    }
}
