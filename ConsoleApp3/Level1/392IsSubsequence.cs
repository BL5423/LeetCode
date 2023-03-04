using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _392IsSubsequence
    {
        public bool IsSubsequence(string s, string t)
        {
            if (s.Length > t.Length)
                return false;

            int index = 0;
            for (int i = 0; i < t.Length; ++i)
            {
                if (index >= s.Length)
                    return true;

                if (s[index] == t[i])
                {
                    ++index;
                }
            }

            if (index >= s.Length)
                return true;
            return false;
        }

        public bool IsSubsequenceV2(string s, string t)
        {
            if (s.Length > t.Length)
                return false;

            Dictionary<char, List<int>> indices = new Dictionary<char, List<int>>();
            for(int tIndex = 0; tIndex < t.Length; ++tIndex)
            {
                if (indices.TryGetValue(t[tIndex], out List<int> list))
                {
                    list.Add(tIndex);
                }
                else
                {
                    indices.Add(t[tIndex], new List<int>() { tIndex });
                }
            }

            int prexIndex = -1;
            for (int i = 0; i < s.Length; ++i)
            {
                char ch = s[i];
                if (!indices.TryGetValue(ch, out List<int> list))
                    return false;

                /*
                var next = list.BinarySearch(prexIndex);
                if (next < 0)
                {
                    next = ~next;
                }
                else
                {
                    next += 1;
                }
                */
                int next = BinarySearch(list, prexIndex);
                if (next == list.Count)
                    return false;

                prexIndex = list[next];
            }

            return true;
        }

        private int BinarySearch(List<int> list, int num)
        {
            int start = 0, end = list.Count - 1;
            while (start <= end)
            {
                int middle = (start + end) / 2;
                if (list[middle] > num)
                {
                    end = middle;
                    if (start == end)
                    {
                        break;
                    }
                }
                else // list[middle] <= num
                {
                    start = middle + 1;
                }
            }

            return start;
        }
    }
}
