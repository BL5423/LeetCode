using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _5LongestPalindromicString
    {
        public string LongestPalindrome_DP_1D(string s)
        {
            // dp[j] indicates if [s[i], ... ,s[j]] is palindrome, i is implicitly override in the for loop
            // dp[i, j] depends on dp[i + 1, j - 1]
            // so to optimize storage complexity, we need to iterate i from s.length - 1 to 0
            // as to j, we need to keep the prev dp[j - 1] in a temp variable for each dp[j]
            bool[] dp = new bool[s.Length];
            int minLeft = 0, maxRight = 0;
            for (int j = 0; j < s.Length; ++j)
            {
                // initialize dp[j, j]
                dp[j] = true;
            }

            for (int left = s.Length - 2; left >= 0; --left)
            {
                // right starts with left + 1(instead of left + 2 in order to consider for s[left] and s[left + 1])
                // dp[left] is the previous result dp[left + 1, right(left + 1) - 1]
                bool prevDpJ = dp[left];
                for (int right = left + 1; right < s.Length; ++right)
                {
                    // tempDpJ is the current result of dp[i, j], which is to be updated
                    // tempDpJ is also the result of dp[i + 1, j], which is the dp[i + 1, j' - 1] of next iteration for current j
                    bool tempDpJ = dp[right];
                    dp[right] = prevDpJ && s[left] == s[right];

                    if (dp[right] && right - left > maxRight - minLeft)
                    {
                        maxRight = right;
                        minLeft = left;
                    }

                    // set prevDpJ as tempDpJ as we move to next iteration of j
                    prevDpJ = tempDpJ;
                }
            }

            return s.Substring(minLeft, maxRight - minLeft + 1);
        }

        public string LongestPalindrome_DP_2D(string s)
        {
            // dp[i,j] indicates if [s[i], ... ,s[j]] is palindrome
            bool[,] dp = new bool[s.Length, s.Length];
            int minLeft = 0, maxRight = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                dp[i, i] = true;
                if (i + 1 < s.Length)
                {
                    dp[i, i + 1] = (s[i] == s[i + 1]);
                    if (dp[i, i + 1])
                    {
                        maxRight = i + 1;
                        minLeft = i;
                    }
                }
            }
            
            for(int left = s.Length - 2; left >= 0; --left)
            {
                for(int right = left + 2; right < s.Length; ++right)
                {
                    dp[left, right] = dp[left + 1, right - 1] && s[left] == s[right];

                    if (dp[left, right] && right - left > maxRight - minLeft)
                    {
                        maxRight = right;
                        minLeft = left;
                    }
                }
            }

            return s.Substring(minLeft, maxRight - minLeft + 1);
        }

        public string LongestPalindrome(string s)
        {
            int longestStart = 0, longestLength = 1;
            int[,] flags = new int[s.Length, s.Length];
            for(int start = 0; start < s.Length; ++start)
            {
                // the char itself is palindromic
                flags[start,start] = 1;

                // try find palindromic strings between 0 to start
                int previous = start - 1;
                if (previous >= 0 && s[start] == s[previous])
                {
                    // something like 'aa' is palindromic
                    flags[previous, start] = 1;
                    int currentLength = start - previous + 1;
                    if (currentLength > longestLength)
                    {
                        longestLength = currentLength;
                        longestStart = previous;
                    }
                }

                for (previous = start - 2; previous >=0; --previous)
                {
                    // [previous, start] is palindromic
                    // only when [previous + 1, start - 1] is palindromic AND
                    // s[previous] == s[start]
                    if (flags[previous + 1, start - 1] == 1 &&
                        s[previous] == s[start])
                    {
                        flags[previous, start] = 1;
                        int currentLength = start - previous + 1;
                        if (currentLength > longestLength)
                        {
                            longestLength = currentLength;
                            longestStart = previous;
                        }
                    }
                }
            }

            return s.Substring(longestStart, longestLength);
        }
    }
}
