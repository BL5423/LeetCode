using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _131PalindromePartitioning
    {
        public IList<IList<string>> Partition(string s)
        {
            int n = s.Length;
            bool[,] dp = new bool[n, n];
            for (int i = 0; i < n; ++i)
            {
                dp[i, i] = true;
                if (i + 1 < n && s[i] == s[i + 1])
                    dp[i, i + 1] = true;
            }

            for (int i = n - 2; i >= 0; --i)
            {
                for (int j = i + 1; j < n; ++j)
                {
                    if (dp[i + 1, j - 1] && s[i] == s[j])
                    {
                        dp[i, j] = true;
                    }
                }
            }

            var res = new List<IList<string>>();
            this.Partition(s, 0, new LinkedList<string>(), res, dp);
            return res;
        }

        private void Partition(string s, int index, LinkedList<string> list, IList<IList<string>> res, bool[,] dp)
        {
            if (index == s.Length)
            {
                if (list.Count != 0)
                {
                    res.Add(list.ToList());
                }
            }
            else
            {
                for(int i = index; i < s.Length; ++i)
                {
                    if (dp[index, i])
                    {
                        var str = s.Substring(index, i - index + 1);
                        list.AddLast(str);

                        this.Partition(s, i + 1, list, res, dp);

                        list.RemoveLast();
                    }
                }
            }
        }

        private static bool IsPalindrome(string s)
        {
            int left = 0, right = s.Length - 1;
            while (left <= right)
            {
                if (s[left] != s[right])
                    return false;

                ++left;
                --right;
            }

            return true;
        }
    }
}
