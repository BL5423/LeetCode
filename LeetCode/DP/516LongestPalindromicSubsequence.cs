using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _516LongestPalindromicSubsequence
    {
        public int LongestPalindromeSubseq_DP_1D(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            // dp[i][j] is the length of the longest palindrome of characters s[i] to s[j], i is implicitly included in each iteration
            int[] dp = new int[s.Length];
            for(int j = 0; j < s.Length; ++j)
            {
                // a character is a palindrome of itself
                dp[j] = 1;
            }

            for(int i = s.Length - 2; i >= 0; --i)
            {
                // dp[i + 1][j - 1]
                int prevDpJ = dp[i + 1]; // since j will start at i + 2, hence i + 1 is j - 1
                dp[i + 1] = 1;
                if (s[i] == s[i + 1])
                    dp[i + 1] = 2;

                for (int j = i + 2; j < s.Length; ++j)
                {
                    // dp[i + 1][j]
                    int dpI1_J = dp[j];

                    if (s[i] == s[j])
                        dp[j] = 2 + prevDpJ;
                    else
                    {
                        // dp[i][j - 1]
                        int dpI_J1 = dp[j - 1];
                        dp[j] = Math.Max(dpI_J1, dpI1_J);
                    }

                    prevDpJ = dpI1_J;
                }
            }

            return dp[s.Length - 1];
        }

        public int LongestPalindromeSubseq_DP_2D_v2(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            // dp[i, j] is the length of the longest palindrome subseqence between s[i] and s[j], i is implicited hidden in each loop
            //        b b b a b
            // cur        i   j
            //                              cur[i,j] depens on prev[i,j - 1], prev[i, j] and cur[i, j - 1]
            // prev         i j
            int[] dpPrev = new int[s.Length];
            int[] dpCur = new int[s.Length];
            int maxLength = 1;

            // initialize the base status for last 'TWO' characters in s
            if (s.Length >= 2)
            {
                int j = s.Length - 2;
                dpPrev[j] = 1;
                dpPrev[j + 1] = 1;
                if (s[j] == s[j + 1])
                { 
                    dpPrev[j + 1] = 2;
                    maxLength = 2;
                }
            }

            // i starts with the 'THIRD' character from the end of s
            for (int i = s.Length - 3; i >= 0; --i)
            {
                // single character is palindrome of itself, this is also the pre-initialization of dpPrev as we'll swap them later
                dpCur[i] = 1;
                // setup dpCur[j - 1] as the base status for the for loop below
                dpCur[i + 1] = 1; // j starts with i + 2, hence i + 1 == j - 1
                if (s[i] == s[i + 1])
                    dpCur[i + 1] = 2;

                for (int j = i + 2; j < s.Length; ++j)
                {
                    dpCur[j] = Math.Max(dpCur[j - 1], dpPrev[j]);
                    if (s[i] == s[j])
                        dpCur[j] = Math.Max(dpCur[j], dpPrev[j - 1] + 2);

                    if (dpCur[j] > maxLength)
                    {
                        maxLength = dpCur[j];
                    }
                }

                // swap dpPrev and dpCur
                var temp = dpPrev;
                dpPrev = dpCur;
                dpCur = temp;                
            }

            return maxLength;
        }

        public int LongestPalindromeSubseq_DP_2D(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            // dp[i, j] is the length of the longest palindrome subseqence between s[i] and s[j]
            int[,] dp = new int[s.Length, s.Length];
            int maxLength = 1;
            for (int i = 0; i < s.Length; ++i)
            {
                dp[i, i] = 1;
                if (i < s.Length - 1)
                {
                    if (s[i] == s[i + 1])
                    {
                        dp[i, i + 1] = 2;
                        maxLength = 2;
                    }
                    else
                    {
                        dp[i, i + 1] = 1;
                    }
                }
            }

            for(int i = s.Length - 3; i >= 0; --i)
            {
                for(int j = i + 2; j < s.Length; ++j)
                {
                    dp[i, j] = Math.Max(dp[i, j - 1], dp[i + 1, j]);
                    if (s[i] == s[j])
                        dp[i, j] = Math.Max(dp[i, j], dp[i + 1, j - 1] + 2);

                    if (dp[i, j] > maxLength)
                    {
                        maxLength = dp[i, j];
                    }
                }
            }

            return maxLength;
        }
    }
}
