using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _1216ValidPalindromeIII
    {
        public bool IsValidPalindrome1DBottomUp(string s, int k)
        {
            // dp[i, j] is the min # of characters need to be removed from s[i, ..., j] to make it a valid palindrome
            int[] dp = new int[s.Length];
            for (int i = s.Length - 2; i >= 0; --i)
            {
                int i1_j1 = 0, i_j1 = 0;
                for (int j = i + 1; j < s.Length; ++j)
                {
                    int i1_j = dp[j];

                    if (s[i] != s[j])
                        dp[j] = 1 + Math.Min(i1_j, i_j1);
                    else
                        dp[j] = j > i + 1 ? i1_j1 : 0;

                    i1_j1 = i1_j;
                    i_j1 = dp[j];
                }
            }

            return dp[s.Length - 1] <= k;
        }

        public bool IsValidPalindrome2DBottomUp(string s, int k)
        {
            // dp[i, j] is the min # of characters need to be removed from s[i, ..., j] to make it a valid palindrome
            int[,] dp = new int[s.Length, s.Length];
            for (int i = s.Length - 2; i >= 0; --i)
            {
                for (int j = i + 1; j < s.Length; ++j)
                {
                    if (s[i] != s[j])
                        dp[i, j] = 1 + Math.Min(dp[i + 1, j], dp[i, j - 1]);
                    else
                        dp[i, j] = j > i + 1 ? dp[i + 1, j - 1] : 0;
                }
            }

            return dp[0, s.Length - 1] <= k;
        }

        public bool IsValidPalindrome2DTopDown(string s, int k)
        {
            return MinimalCharatersToRemove(s, 0, s.Length - 1) <= k;
        }

        private IDictionary<(int, int), int> memo = new Dictionary<(int, int), int>();

        private int MinimalCharatersToRemove(string s, int left, int right)
        {
            if (left == right)
                return 0;

            if (left == right - 1)
                return s[left] != s[right] ? 1 : 0;

            if (memo.TryGetValue((left, right), out int count))
                return count;

            count = 0;
            while (left < right)
            {
                if (s[left] != s[right])
                {
                    count = 1 + Math.Min(MinimalCharatersToRemove(s, left + 1, right),
                    MinimalCharatersToRemove(s, left, right - 1));
                    break;
                }

                ++left;
                --right;
            }

            memo.TryAdd((left, right), count);
            return count;
        }

        public bool IsValidPalindromeBottomUp(string s, int k)
        {
            // dp[i, j, k] means if s[i, ..., j] is a valid k-palindrome 
            bool[,,] dp = new bool[s.Length + 1, s.Length + 1, k + 1];
            for (int i = 0; i < s.Length; ++i)
            {
                for (int l = 0; l <= k; ++l)
                    dp[i, i, l] = true; // single character
            }

            for (int i = s.Length - 1; i >= 0; --i)
            {
                for (int j = i + 1; j < s.Length; ++j)
                {
                    for (int l = 0; l <= k; ++l)
                    {
                        if (s[i] != s[j])
                        {
                            if (l != 0)
                                dp[i, j, l] |= dp[i + 1, j, l - 1] || dp[i, j - 1, l - 1];
                            else
                                dp[i, j, l] = false;
                        }
                        else
                            dp[i, j, l] |= j > i + 1 ? dp[i + 1, j - 1, l] : true;
                    }
                }
            }

            return dp[0, s.Length - 1, k];
        }

        public bool IsValidPalindromeTopDown(string s, int k)
        {
            return IsValidPalindrome(s, 0, s.Length - 1, k);
        }

        private IDictionary<(int, int, int), bool> cache = new Dictionary<(int, int, int), bool>();

        private IDictionary<(int, int), int> failedK = new Dictionary<(int, int), int>();

        private bool IsValidPalindrome(string s, int left, int right, int k)
        {
            if (k < 0)
                return false;

            if (left == right)
                return true;

            if (failedK.TryGetValue((left, right), out int failed) && failed >= k)
                return false;

            if (cache.TryGetValue((left, right, k), out bool result))
                return result;

            bool valid = true;
            while (left < right)
            {
                if (s[left] != s[right])
                {
                    valid = IsValidPalindrome(s, left + 1, right, k - 1) ||
                    IsValidPalindrome(s, left, right - 1, k - 1);
                    break;
                }

                ++left;
                --right;
            }

            cache.TryAdd((left, right, k), valid);
            if (!valid)
            {
                if (failedK.TryGetValue((left, right), out int f))
                {
                    if (f > k)
                        failedK.TryAdd((left, right), k);
                }
                else
                    failedK.Add((left, right), k);
            }
            return valid;
        }
    }
}
