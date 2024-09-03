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
            if (s1.Length > s2.Length)
                return false;

            int[] counts1 = new int[26];
            int[] counts2 = new int[26];
            foreach (var ch in s1)
            {
                ++counts1[ch - 'a'];
            }

            int match = 0, mismatch = 0;
            for (int i = 0; i < s2.Length; ++i)
            {
                if (i >= s1.Length) // need to shrink
                {
                    int index = s2[i - s1.Length] - 'a';
                    --counts2[index];

                    if (counts2[index] < counts1[index])
                        --match;
                    else // if (counts2[index] >= counts1[index])
                        --mismatch;
                }

                char ch = s2[i];
                ++counts2[ch - 'a'];
                if (counts2[ch - 'a'] <= counts1[ch - 'a'])
                    ++match;
                else // counts2[ch - 'a'] > counts1[ch - 'a']
                    ++mismatch;

                if (mismatch == 0 && match == s1.Length)
                    return true;
            }

            return false;
        }

        public bool CheckInclusionV3(string s1, string s2)
        {
            if (s1.Length > s2.Length)
                return false;

            int[] counts1 = new int[26];
            int[] counts2 = new int[26];
            foreach (var ch in s1)
            {
                ++counts1[ch - 'a'];
            }

            for (int i = 0; i < s2.Length; ++i)
            {
                if (i >= s1.Length) // need to shrink
                {
                    --counts2[s2[i - s1.Length] - 'a'];
                }

                char ch = s2[i];
                ++counts2[ch - 'a'];

                if (counts1.SequenceEqual(counts2))
                    return true;
            }

            return false;
        }

        public bool CheckInclusionV2(string p, string s)
        {
            if (p.Length > s.Length)
                return false;

            int match = 0;
            int[] p_counters = new int['z' - 'a' + 1];
            int[] s_counters = new int['z' - 'a' + 1];
            for (int i = 0; i < p.Length; ++i)
            {
                ++p_counters[p[i] - 'a'];
                ++s_counters[s[i] - 'a'];
            }

            for (int i = 0; i < 26; ++i)
            {
                if (p_counters[i] == s_counters[i])
                    ++match;
            }

            if (match == 26)
                return true;

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
                    return true;
            }

            return false;
        }

        public bool CheckInclusionV1(string s1, string s2)
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
