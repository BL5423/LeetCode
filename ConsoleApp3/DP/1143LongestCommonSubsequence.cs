using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _1143LongestCommonSubsequence
    {
        public int LongestCommonSubsequence_1D_V2(string text1, string text2)
        {
            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
                return 0;

            if (text2.Length > text1.Length)
            {
                // swap
                return LongestCommonSubsequence_1D_V2(text2, text1);
            }

            int[] dp = new int[text2.Length + 1];
            for(int i = 1; i <= text1.Length; ++i)
            {
                int prevDP = dp[0];
                for(int j = 1; j <= text2.Length; ++j)
                {
                    int tempDP = dp[j];

                    if (text1[i] == text2[j])
                        dp[j] = prevDP + 1;
                    else
                    {
                        dp[j] = Math.Max(dp[j - 1], dp[j]);
                    }

                    prevDP = tempDP;
                }
            }

            return dp[text2.Length];
        }

        public int LongestCommonSubsequence_1D(string text1, string text2)
        {
            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
                return 0;

            // optmization with 1D array, this is possible by reusing the values in dp as previous results of last row(i - 1)
            int[] dp = new int[text2.Length];
            int val = 0;
            for (int j = 0; j < text2.Length; ++j)
            {
                if (text2[j] == text1[0])
                    val = 1;

                dp[j] = val;
            }

            int indexI = text1.IndexOf(text2[0]);
            for (int i = 1; i < text1.Length; ++i)
            {
                // dp[i - 1, j - 1]
                int dpJInPrevRow = dp[0];
                dp[0] = 0;
                if (indexI != -1 && i >= indexI)
                    dp[0] = 1;

                for (int j = 1; j < text2.Length; ++j)
                {
                    // keep the dp[i - 1, j] since it is the dp[i - 1, j - 1] of next j
                    int tempDpJ = dp[j];
                    if (text1[i] == text2[j])
                        dp[j] = dpJInPrevRow + 1;
                    else
                        dp[j] = Math.Max(dp[j - 1], dp[j]);

                    dpJInPrevRow = tempDpJ;
                }
            }

            return dp[text2.Length - 1];
        }

        public int LongestCommonSubsequence_2_1D(string text1, string text2)
        {
            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
                return 0;

            int[] prevDp = new int[text2.Length];
            int[] curDp = new int[text2.Length];
            int val = 0;
            for(int j = 0; j < text2.Length; ++j)
            {
                if (text2[j] == text1[0])
                    val = 1;

                prevDp[j] = val;
            }

            int indexI = text1.IndexOf(text2[0]);
            for (int i = 1; i < text1.Length; ++i)
            {                
                curDp[0] = 0;
                if (indexI != -1 && i >= indexI)
                    curDp[0] = 1;

                for(int j = 1; j < text2.Length; ++j)
                {
                    if (text1[i] == text2[j])
                        curDp[j] = prevDp[j - 1] + 1;
                    else
                    {
                        curDp[j] = Math.Max(curDp[j - 1], prevDp[j]);
                    }
                }

                // swap
                int[] temp = prevDp;
                prevDp = curDp;
                curDp = temp;
            }

            return prevDp[text2.Length - 1];
        }

        public int LongestCommonSubsequence_2D(string text1, string text2)
        {
            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
                return 0;

            // dp[i, j] is the length of the longest common subseq of text1[0,...,i] and text2[0,...,j]
            int[,] dp = new int[text1.Length, text2.Length];
            int val = 0;
            for(int i = 0; i < text1.Length; ++i)
            {
                if (text1[i] == text2[0])
                    val = 1;
                
                // base status
                dp[i, 0] = val;
            }

            val = 0;
            for (int j = 0; j < text2.Length; ++j)
            {
                if (text2[j] == text1[0])
                    val = 1;

                dp[0, j] = val;
            }

            for(int i = 1; i < text1.Length; ++i)
            {
                for(int j = 1; j < text2.Length; ++j)
                {
                    if (text1[i] == text2[j])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i, j - 1], dp[i - 1, j]);
                    }
                }
            }

            return dp[text1.Length - 1, text2.Length - 1];
        }
    }
}
