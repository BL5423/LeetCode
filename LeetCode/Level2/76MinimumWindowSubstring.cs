using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _76MinimumWindowSubstring
    {
        public string MinWindow(string s, string t)
        {
            if (t.Length > s.Length)
                return string.Empty;

            int[] tCounts = new int[128];
            foreach (char ch in t)
            {
                ++tCounts[ch];
            }
            int tChars = tCounts.Count(count => count != 0);

            IList<(int, char)> sCharsIndex = new List<(int, char)>(t.Length);
            for(int i = 0; i < s.Length; ++i)
            {
                char sCh = s[i];
                if(tCounts[sCh] != 0)
                {
                    sCharsIndex.Add((i, sCh));
                }
            }

            int[] res = { int.MaxValue, -1, -1 };
            // # of chars have been matched in t, when a char is matched, it means all of its presences are found in s
            int matched = 0;
            int[] sCounts = new int[128];
            int left = 0, right = 0;
            while (right < sCharsIndex.Count)
            {
                var item = sCharsIndex[right];
                if (++sCounts[item.Item2] == tCounts[item.Item2])
                {
                    ++matched;
                }

                while (left <= right && matched == tChars)
                {
                    if (res[0] > sCharsIndex[right].Item1 - sCharsIndex[left].Item1)
                    {
                        res[0] = sCharsIndex[right].Item1 - sCharsIndex[left].Item1;
                        res[1] = sCharsIndex[left].Item1;
                        res[2] = sCharsIndex[right].Item1;
                    }

                    var prevItem = sCharsIndex[left++];
                    if (--sCounts[prevItem.Item2] < tCounts[prevItem.Item2])
                    {
                        --matched;
                    }
                }

                ++right;
            }

            return res[0] != int.MaxValue ? s.Substring(res[1], res[2] - res[1] + 1) : string.Empty;
        }

        public string MinWindowSingleCounter(string s, string t)
        {
            if (t.Length > s.Length)
                return string.Empty;

            HashSet<int> tChars = new HashSet<int>(t.Length);
            int[] tCounts = new int['z' - 'A' + 1];
            foreach (char ch in t)
            {
                ++tCounts[ch - 'A'];
                tChars.Add(ch);
            }

            int[] res = { int.MaxValue, -1, -1 };
            // # of chars have been matched in t, when a char is matched, it means all of its presences are found in s
            int matched = 0;
            int[] sCounts = new int['z' - 'A' + 1];
            int left = 0, right = 0;
            while (right < s.Length)
            {
                char ch = s[right++];
                if (tChars.Contains(ch))
                {
                    if (++sCounts[ch - 'A'] == tCounts[ch - 'A'])
                    {
                        ++matched;
                    }

                    // while found a substring that contains all chars of t
                    // try to shrink the window(left - right) to reduce the window size
                    while (left < right && matched == tChars.Count)
                    {
                        if (right - left < res[0])
                        {
                            res[0] = right - left;
                            res[1] = left;
                            res[2] = right;
                        }

                        char lChar = s[left++];
                        if (tChars.Contains(lChar) && --sCounts[lChar - 'A'] < tCounts[lChar - 'A'])
                        {
                            --matched;
                        }
                    }
                }
            }

            return res[0] != int.MaxValue ? s.Substring(res[1], res[2] - res[1]) : string.Empty;
        }

        public string MinWindowArrayCounters(string s, string t)
        {
            if (t.Length > s.Length)
                return string.Empty;

            int[] tChars = new int[128];
            int[] tCounts = new int['z' - 'A' + 1];
            foreach (char ch in t)
            {
                ++tCounts[ch - 'A'];
                tChars[ch] = 1;
            }

            int left = 0;
            while (left < s.Length && tChars[s[left]] == 0)
            {
                ++left;
            }
            int right = left;
            int minLength = int.MaxValue, minLeft = -1, minRight = -1;
            while (left < s.Length && right < s.Length)
            {
                char sChar = s[right++];
                if (tChars[sChar] != 0)
                {
                    --tCounts[sChar - 'A'];
                    if (!tCounts.Any(count => count > 0))
                    {
                        for (int index = left; index < right; ++index)
                        {
                            char ch = s[index];
                            if (tChars[ch] != 0)
                            {
                                if (right - index < minLength && !tCounts.Any(count => count > 0))
                                {
                                    minLength = right - index;
                                    minLeft = index;
                                    minRight = right;
                                }

                                if (++tCounts[ch - 'A'] > 0)
                                {
                                    left = index + 1;
                                    while (left < s.Length && tChars[s[left]] == 0)
                                    {
                                        ++left;
                                    }

                                    if (left > right)
                                        right = left;

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return minLength != int.MaxValue ? s.Substring(minLeft, minRight - minLeft) : string.Empty;
        }
        
        public string MinWindowCounter(string s, string t)
        {
            if (t.Length > s.Length)
                return string.Empty;

            Dictionary<char, int> dic = new Dictionary<char, int>(128);
            int[] tCounts = new int[128];
            foreach(char ch in t)
            {
                ++tCounts[ch - 'A'];
                if (dic.TryGetValue(ch, out int count))
                {
                    ++dic[ch];
                }
                else
                {
                    dic[ch] = 1;
                }
            }
            int totalInT = t.Length;

            int minLength = int.MaxValue, minStart = -1, minEnd = -1;
            int left = 0, right = 0, totalInS = 0;
            bool searching = false;
            while (left < s.Length && right < s.Length)
            {
                if (!searching)
                {
                    while (left < s.Length && tCounts[s[left] - 'A'] == 0)
                    {
                        ++left;
                        right = left;
                    }

                    searching = true;
                }
                else
                {

                    char sCh = s[right];
                    if (tCounts[sCh - 'A'] > 0)
                    {
                        --totalInT;
                        --tCounts[sCh - 'A'];
                    }
                    if (dic.ContainsKey(sCh))
                    {
                        ++totalInS;
                    }

                    if (totalInT == 0)
                    {
                        if (totalInS > t.Length)
                        {
                            // dups in left to right
                            int index = right;
                            for (; index >= left && totalInT < t.Length; --index)
                            {
                                // search reversely to find the minimum string that contains all characters of t
                                char ch = s[index];
                                if (dic.TryGetValue(ch, out int count) && tCounts[ch - 'A'] < count)
                                {
                                    // restore the counts of t for a potential search next time from right + 1
                                    ++totalInT;
                                    ++tCounts[ch - 'A'];
                                }
                            }

                            left = index + 1;
                        }
                        else
                        {
                            // restore the counts of t for a potential search next time from right + 1
                            totalInT = t.Length;
                            foreach (var item in dic)
                            {
                                tCounts[item.Key - 'A'] = item.Value;
                            }
                        }

                        if (right - left + 1 < minLength)
                        {
                            minLength = right - left + 1;
                            minStart = left;
                            minEnd = right;
                        }

                        left = right + 1;
                        right = left;
                        searching = false;
                    }
                    else
                    {
                        ++right;
                    }
                }
            }

            return minLength != int.MaxValue ? s.Substring(minStart, minEnd - minStart + 1) : string.Empty;
        }
    }
}
