using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Top100Liked
{
    public class _3LongestSubstringWithoutRepeatingCharacters
    {
        public int LengthOfLongestSubstring(string s)
        {
            Dictionary<char, int> counts = new Dictionary<char, int>(s.Length);
            int start = 0, maxLength = 0;
            for(int index = 0; index < s.Length; ++index)
            {
                char ch = s[index];
                if (counts.TryGetValue(ch, out int count))
                {
                    ++counts[ch];
                }
                else
                {
                    counts.Add(ch, 1);
                }

                while (counts[ch] > 1)
                {
                    --counts[s[start++]];
                }

                maxLength = Math.Max(maxLength, index - start + 1);
            }

            return maxLength;
        }

        public int LengthOfLongestSubstringIndex(string s)
        {
            Dictionary<char, int> index = new Dictionary<char, int>(s.Length);
            int start = 0, end = 0, length = 0;
            for(int i = 0; i < s.Length; ++i)
            {
                char ch = s[i];
                if (index.TryGetValue(ch, out int j) && j >= start)
                {
                    // dup found
                    length = Math.Max(length, end - start);
                    start = j + 1;
                }

                ++end;
                index[ch] = i;
            }

            return Math.Max(length, end - start);
        }
    }
}
