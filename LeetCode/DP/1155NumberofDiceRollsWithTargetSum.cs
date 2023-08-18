using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _1155NumberofDiceRollsWithTargetSum
    {
        private const int Module = 1000000007;

        public int NumRollsToTarget(int n, int k, int target)
        {
            int[] prevDp = new int[target + 1], dp = new int[target + 1];
            for (int v = 1; v <= Math.Min(k, target); ++v)
                prevDp[v] = 1;

            for (int d = 1; d < n; ++d)
            {
                for (int t = 1; t <= target; ++t)
                {
                    dp[t] = 0;
                    for (int v = 1; v <= Math.Min(k, t); ++v)
                    {
                        dp[t] = (dp[t] % Module) + (prevDp[t - v] % Module);
                    }
                }

                var temp = dp;
                dp = prevDp;
                prevDp = temp;
            }

            return prevDp[target] % Module;
        }

        public int NumRollsToTarget_BottomUp(int n, int k, int target)
        {
            int[,] dp = new int[n, target + 1];
            for (int v = 1; v <= Math.Min(k, target); ++v)
                dp[0, v] = 1;

            for(int d = 1; d < n; ++d)
            {
                for (int t = 1; t <= target; ++t)
                {
                    for (int v = 1; v <= Math.Min(k, t); ++v)
                    {
                        dp[d, t] = (dp[d, t] % Module) + (dp[d - 1, t - v] % Module);
                    }
                }
            }

            return dp[n - 1, target] % Module;
        }

        public int NumRollsToTarget_TopDown(int n, int k, int target)
        {
            int[,] cache = new int[n, target + 1];
            int res = NumRollsToTarget_TopDown(n, k, 0, 0, target, cache);
            return res != -1 ? res : 0;
        }

        private int NumRollsToTarget_TopDown(int n, int k, int index, int targetSoFar, int target, int[,] cache)
        {
            if (index == n)
            {
                if (targetSoFar == target)
                    return 1;

                return -1;
            }

            // still some dices left
            if (targetSoFar >= target)
                return -1;

            if (cache[index, targetSoFar] == 0)
            {
                int nums = 0;
                for(int i = 1; i <= k; ++i)
                {
                    int res = NumRollsToTarget_TopDown(n, k, index + 1, targetSoFar + i, target, cache);
                    if (res != -1)
                        nums = (nums + res) % Module;
                }

                cache[index, targetSoFar] = (nums != 0 ? nums : -1);
            }

            return cache[index, targetSoFar];
        }
    }
}
