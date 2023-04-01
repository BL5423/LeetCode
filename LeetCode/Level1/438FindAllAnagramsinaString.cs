using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _438FindAllAnagramsinaString
    {
        public IList<int> FindAnagrams(string s, string p)
        {
            var res = new List<int>();
            if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(p) && p.Length <= s.Length)
            {
                int match = 0;
                int[] p_counters = new int['z' - 'a' + 1];
                int[] s_counters = new int['z' - 'a' + 1];
                for(int i = 0; i < p.Length; ++i)
                {
                    ++p_counters[p[i] - 'a'];
                    ++s_counters[s[i] - 'a'];
                }

                for(int i = 0; i < 26; ++i)
                {
                    if (p_counters[i] == s_counters[i])
                        ++match;
                }

                if (match == 26)
                    res.Add(0);

                for (int i = p.Length; i < s.Length; ++i)
                {
                    char l = s[i - p.Length], r = s[i];

                    --s_counters[l - 'a'];
                    if (s_counters[l - 'a'] == p_counters[l - 'a'])
                        ++match;
                    else if (s_counters[l - 'a'] == p_counters[l - 'a'] - 1)
                        --match;

                    ++s_counters[r - 'a'];
                    if (s_counters[r - 'a'] == p_counters[r - 'a'])
                        ++match;
                    else if (s_counters[r - 'a'] == p_counters[r - 'a'] + 1)
                        --match;

                    if (match == 26)
                        res.Add(i - p.Length);
                }
            }

            return res;
        }

        public IList<int> FindAnagramsV2(string s, string p)
        {
            int[] pCounts = new int[26];
            foreach(char ch in p)
            {
                ++pCounts[ch - 'a'];
            }

            var res = new List<int>();
            //int[] sCounts = new int[26];
            int start = 0, index = 0;
            while (index < s.Length)
            {
                --pCounts[s[index] - 'a'];
                //++sCounts[s[index] - 'a'];
                if (!pCounts.Any(c => c != 0))
                //if (pCounts.SequenceEqual(sCounts))
                {
                    res.Add(start);

                    ++pCounts[s[start++] - 'a'];
                    //--sCounts[s[start++] - 'a'];
                }
                else if (index - start + 1 == p.Length)
                {
                    ++pCounts[s[start++] - 'a'];
                    //--sCounts[s[start++] - 'a'];
                }

                ++index;
            }

            return res;
        }
                
        public IList<int> FindAnagramsV1(string s, string p)
        {
            var res = new List<int>();
            Dictionary<int, int> counts = new Dictionary<int, int>(26);
            int total = p.Length;
            foreach(char ch in p)
            {
                if (counts.ContainsKey(ch - 'a'))
                {
                    ++counts[ch - 'a'];
                }
                else
                {
                    counts.Add(ch - 'a', 1);
                }
            }

            int start = 0, index = 0;
            while(index < s.Length)
            {
                char ch = s[index];
                if (counts.TryGetValue(ch - 'a', out int count) && count != 0)
                {
                    --total;
                    --counts[ch - 'a'];

                    // found a match
                    if (total == 0)
                    {
                        res.Add(start);

                        // move the sliding window forwards
                        ++counts[s[start] - 'a'];
                        ++total;
                        ++start;
                    }

                    ++index;
                }
                else
                {
                    if (!counts.ContainsKey(ch - 'a'))
                    {
                        for (int i = start; i < index; ++i)
                        {
                            // restore counts
                            ++total;
                            ++counts[s[i] - 'a'];
                        }

                        // unseen character, start over from index
                        start = ++index;
                    }
                    else
                    {
                        // move the sliding window forwards
                        ++counts[s[start] - 'a'];
                        ++total;
                        ++start;
                    }
                }
            }

            return res;
        }
    }
}
