using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _567PermutationinString
    {
        public bool CheckInclusion(string s1, string s2)
        {
            bool found = false;
            if (!string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(s2))
            {
                int total = 0;
                int[] counters = new int['z' - 'a' + 1];
                HashSet<char> chars = new HashSet<char>(26);
                foreach(var ch in s1)
                {
                    chars.Add(ch);
                    ++counters[ch - 'a'];
                    ++total;
                }

                int left = 0, right = 0;
                while (left <= right && right < s2.Length)
                {
                    char ch = s2[right];
                    if (--counters[ch - 'a'] < 0)
                    {
                        if (chars.Contains(ch))
                            --total;

                        while (left <= right && counters[ch - 'a'] < 0)
                        {
                            var lch = s2[left];
                            ++counters[lch - 'a'];
                            if (chars.Contains(lch))
                            {
                                ++total;
                            }

                            ++left;
                        }
                    }
                    else
                    {
                        if (--total == 0)
                            return true;
                    }

                    ++right;
                }
            }

            return found;
        }
    }
}
