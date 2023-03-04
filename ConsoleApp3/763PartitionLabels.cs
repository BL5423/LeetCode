using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _763PartitionLabels
    {
        public IList<int> PartitionLabels(string s)
        {
            var res = new List<int>(s.Length);
            HashSet<char> seen = new HashSet<char>(s.Length);
            int totalRemaining = 0;
            int[] counts = new int[26];
            foreach(char ch in s)
            {
                ++counts[ch - 'a'];
            }

            int preIndex = -1;
            for(int i = 0; i < s.Length; ++i)
            {
                char ch = s[i];
                if (seen.Contains(ch))
                {
                    --totalRemaining;
                }
                else
                {
                    seen.Add(ch);
                    totalRemaining += (counts[ch - 'a'] - 1); // minus one since ch should be counted, we only need the ch's left
                }

                if (totalRemaining == 0)
                {
                    // match all
                    res.Add(i - preIndex);
                    preIndex = i;
                    seen.Clear();
                }
            }

            return res;
        }

        public IList<int> PartitionLabelsV2(string s)
        {
            int[] lastIndex = new int[26];
            for(int i = 0; i < s.Length; ++i)
            {
                lastIndex[s[i] - 'a'] = i;
            }

            var res = new List<int>(s.Length);
            int end = 0, prevEnd = -1;
            for(int i = 0; i < s.Length; ++i)
            {
                end = Math.Max(end, lastIndex[s[i] - 'a']);
                if (end == i)
                {
                    res.Add(end - prevEnd);
                    prevEnd = end;
                }
            }

            return res;
        }
    }
}
