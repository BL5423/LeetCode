using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _91DecodeWays
    {
        public int NumDecodings_BottomUp_Optimized(string s)
        {
            int dp1 = 1, dp2 = 2;
            for (int index = s.Length - 1; index >= 0; --index)
            {
                int dp = 0;
                if (s[index] != '0')
                {
                    dp = dp1;
                    if (index < s.Length - 1 && ((s[index] - '0') * 10 + s[index + 1] - '0') <= 26)
                        dp += dp2;
                }

                dp2 = dp1;
                dp1 = dp;
            }

            return dp1;
        }

        public int NumDecodings_BottomUp(string s)
        {
            int[] dp = new int[s.Length + 2];
            dp[s.Length] = dp[s.Length + 1] = 1;
            for(int index = s.Length - 1; index >= 0; --index)
            {
                if (s[index] != '0')
                {
                    dp[index] = dp[index + 1];
                    if (index < s.Length - 1 && ((s[index] - '0') * 10 + s[index + 1] - '0') <= 26)
                        dp[index] += dp[index + 2];
                }
            }

            return dp[0];
        }

        public int NumDecodings_TopDown(string s)
        {
            int[] cache = new int[s.Length];
            return this.NumDecodings(s, 0, cache);
        }

        private int NumDecodings(string s, int index, int[] cache)
        {
            if (index >= s.Length)
                return 1;
         
            if (cache[index] == 0)
            {
                if (s[index] != '0')
                {
                    cache[index] = NumDecodings(s, index + 1, cache);

                    if (index + 1 < s.Length && ((s[index] - '0') * 10 + s[index + 1] - '0') <= 26)
                        cache[index] += NumDecodings(s, index + 2, cache);
                }
            }

            return cache[index];
        }
    }
}
