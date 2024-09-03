using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _680ValidPalindromeII
    {
        public bool ValidPalindrome(string s)
        {
            int left = 0, right = s.Length - 1;
            while (left < right)
            {
                if (s[left] != s[right])
                {
                    // delete s[left]
                    if (IsPalindrome(s, left + 1, right))
                        return true;

                    // delete s[right]
                    return IsPalindrome(s, left, right - 1);
                }

                ++left;
                --right;
            }

            return true;
        }

        private bool IsPalindrome(string s, int left, int right)
        {
            while (left < right)
            {
                if (s[left++] != s[right--])
                    return false;
            }

            return true;
        }
    }
}
