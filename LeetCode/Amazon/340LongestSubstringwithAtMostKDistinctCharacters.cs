using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _340LongestSubstringwithAtMostKDistinctCharacters
    {
        public int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            int totalUnique = 0, maxLen = 0;
            var counters = new int[256];
            for (int right = 0; right < s.Length; ++right)
            {
                char c = s[right];
                if (++counters[(int)c] == 1)
                {
                    ++totalUnique;
                }

                if (totalUnique <= k)
                {
                    ++maxLen;
                }
                else
                {
                    char c2Remove = s[right - maxLen];
                    if (--counters[(int)c2Remove] == 0)
                    {
                        --totalUnique;
                    }
                }
            }

            return maxLen;
        }

        public int LengthOfLongestSubstringKDistinctV2(string s, int k)
        {
            int totalUnique = 0, maxLen = 0;
            var counters = new int[256];
            int left = 0, right = 0;
            while (right < s.Length)
            {
                char c = s[right];
                if (++counters[(int)c] == 1)
                {
                    ++totalUnique;
                }

                if (totalUnique <= k)
                {
                    maxLen = Math.Max(maxLen, right - left + 1);
                }

                while (totalUnique > k)
                {
                    char c2Remove = s[left++];
                    if (--counters[(int)c2Remove] == 0)
                    {
                        --totalUnique;
                    }
                }

                ++right;
            }

            return maxLen;
        }

        public int LengthOfLongestSubstringKDistinctV1(string s, int k)
        {
            int totalUnique = 0, maxLen = 0;
            var counters = new Dictionary<char, int>(s.Length);
            int left = 0, right = 0;
            while (right < s.Length)
            {
                char c = s[right];
                if (!counters.TryGetValue(c, out int count))
                {
                    counters.Add(c, 1);
                    ++totalUnique;
                }
                else
                {
                    ++counters[c];
                }

                if (totalUnique <= k)
                {
                    maxLen = Math.Max(maxLen, right - left + 1);
                }

                while (totalUnique > k)
                {
                    char c2Remove = s[left++];
                    if (--counters[c2Remove] == 0)
                    {
                        counters.Remove(c2Remove);
                        --totalUnique;
                    }
                }

                ++right;
            }

            return maxLen;
        }
    }
}
