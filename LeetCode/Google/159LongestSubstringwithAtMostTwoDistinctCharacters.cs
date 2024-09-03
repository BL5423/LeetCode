using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _159
    {
        public int LengthOfLongestSubstringTwoDistinct(string s)
        {
            if (s.Length <= 1)
                return s.Length;

            int[] counts = new int[52];
            int maxLen = 0, uniqueChars = 0, left = 0;
            for (int right = 0; right < s.Length; ++right)
            {
                if (++counts[GetIndex(s[right])] == 1)
                {
                    ++uniqueChars;
                }

                // try to shrink
                while (uniqueChars > 2)
                {
                    if (--counts[GetIndex(s[left++])] == 0)
                        --uniqueChars;
                }

                if (uniqueChars <= 2)
                {
                    maxLen = Math.Max(maxLen, right - left + 1);
                }
            }

            return maxLen;
        }

        private static int GetIndex(char c)
        {
            if (c >= 'A' && c <= 'Z')
                return c - 'A';

            return c - 'a' + 26;
        }
    }
}
