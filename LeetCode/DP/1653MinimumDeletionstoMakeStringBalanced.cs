using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _1653MinimumDeletionstoMakeStringBalanced
    {
        public int MinimumDeletions(string s)
        {
            // numOfAAfter[j] is the # of a's after s[j](exclusive)
            int[] numOfAAfter = new int[s.Length];
            int a = 0, b = 0;
            for (int i = 0, j = s.Length - 1; i < s.Length; ++i, --j)
            {
                numOfAAfter[j] = a;
                if (s[j] == 'a')
                    ++a;
            }

            int[] dp = new int[s.Length + 1];
            for (int i = s.Length - 1; i >= 0; --i)
            {
                // delete
                int res = 1 + dp[i + 1];

                // do not delete
                if (s[i] == 'a')
                {
                    res = Math.Min(res, dp[i + 1]);
                }
                else // s[i] == 'b'
                {
                    // to keep s[i], we have to delete all a's after s[i]
                    res = Math.Min(res, numOfAAfter[i]);
                }

                dp[i] = res;
            }

            return dp[0];
        }

        public int MinimumDeletions_TopDown(string s)
        {
            // numOfAAfter[j] is the # of a's after s[j](exclusive)
            // numOfBBefore[i] is the # of b's before s[i](exclusive)
            int[] numOfAAfter = new int[s.Length], numOfBBefore = new int[s.Length];
            int a = 0, b = 0;
            for (int i = 0, j = s.Length - 1; i < s.Length; ++i, --j)
            {
                numOfAAfter[j] = a;
                numOfBBefore[i] = b;
                if (s[i] == 'b')
                    ++b;
                if (s[j] == 'a')
                    ++a;
            }

            var cache = new Dictionary<int, int>();
            Helper(s, 0, numOfAAfter, numOfBBefore, 0, cache);

            return cache[0];
        }

        private int Helper(string s, int index, int[] numOfAAfter, int[] numOfBBefore, int deletedB, Dictionary<int, int> cache)
        {
            if (index == s.Length)
                return 0;

            if (cache.TryGetValue(index, out int res))
                return res;

            // delete
            res = 1 + Helper(s, index + 1, numOfAAfter, numOfBBefore, s[index] == 'b' ? 1 + deletedB : deletedB, cache);

            // do not delete
            if (s[index] == 'a')
            {
                // to keep s[index], we have to delete all the remaining b's BEFORE s[index]
                res = Math.Min(res, numOfBBefore[index] - deletedB + Helper(s, index + 1, numOfAAfter, numOfBBefore, deletedB, cache));
            }
            else // s[index] == 'b'
            {
                // to keep s[index], we have to delete all the a's AFTER s[index]
                res = Math.Min(res, numOfAAfter[index]);
            }

            return cache[index] = res;
        }
    }
}
