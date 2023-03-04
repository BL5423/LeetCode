using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _139WordBreak
    {
        private Dictionary<string, bool> cache = new Dictionary<string, bool>();

        public bool WordBreakV2(string s, IList<string> wordDict)
        {
            if (cache.TryGetValue(s, out bool result))
            {
                return result;
            }

            var res = false;
            foreach (var word in wordDict)
            {
                if (s.Equals(word))
                {
                    res = true;
                }
                else if (s.StartsWith(word))
                {
                    var subStr = s.Substring(word.Length);
                    res |= WordBreakV2(subStr, wordDict);
                }

                if (res)
                    break;
            }

            cache.Add(s, res);
            return res;
        }

        public bool WordBreakV1(string s, IList<string> wordDict)
        {
            if (!string.IsNullOrEmpty(s))
            {
                // dp[i] indicates that s[0,...,i] can be composed by words in the dictionary
                bool[] dp = new bool[s.Length];

                var dic = new HashSet<string>(wordDict.Count);
                // base states
                foreach (var word in wordDict)
                {
                    dic.Add(word);
                    if (s.StartsWith(word))
                        dp[word.Length - 1] = true;
                }

                for(int i = 0; i < s.Length; ++i)
                {
                    for(int j = i + 1; dp[i] && j < s.Length; ++j)
                    {
                        if (!dp[j])
                            dp[j] = dp[i] && dic.Contains(s.Substring(i + 1, j - i));
                    }

                    if (dp[s.Length - 1])
                        return true;
                }
            }

            return false;
        }
    }
}
