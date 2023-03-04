using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _424LongestRepeatingCharacterReplacement
    {
        public int CharacterReplacement(string s, int k)
        {
            int index = 0, startIndex = 0, length = 0;
            Dictionary<char, int> counters = new Dictionary<char, int>();
            while (index < s.Length)
            {
                char ch = s[index];
                if (counters.TryGetValue(ch, out int count))
                {
                    ++count;
                    counters[ch] = count;
                }
                else
                {
                    count = 1;
                    counters.Add(ch, count);
                }

                if (index - startIndex + 1 - counters.Max(v => v.Value) > k)
                {
                    if (--counters[s[startIndex]] == 0)
                        counters.Remove(s[startIndex]);
                    ++startIndex;
                }

                length = Math.Max(length, index - startIndex + 1);
                ++index;
            }

            return length;
        }
    }
}
