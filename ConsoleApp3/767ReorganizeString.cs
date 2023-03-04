using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class TempNode1
    {
        public char ch;

        public int count;
    }

    public class _767ReorganizeString
    {
        public string ReorganizeStringV2(string s)
        {
            int indexOfMax = 0, max = 0;
            int[] counts = new int[26];
            foreach(char c in s)
            {
                int i = c - 'a';
                if (++counts[i] > max)
                {
                    max = counts[i];
                    indexOfMax = i;
                }
            }

            if ((max - 1) * 2 >= s.Length)
                return string.Empty;

            char[] res = new char[s.Length];
            int index = 0;
            while(counts[indexOfMax] > 0)
            {
                res[index] = (char)(indexOfMax + 'a');
                --counts[indexOfMax];
                index += 2;
            }

            for(int i = 0; i < counts.Length; ++i)
            {
                while (counts[i] > 0)
                {
                    if (index >= res.Length)
                        index = 1;

                    res[index] = (char)(i + 'a');
                    --counts[i];
                    index += 2;
                }
            }

            return new string(res);
        }

        public string ReorganizeString(string s)
        {
            TempNode1[] counts = new TempNode1[26];
            for(int i = 0; i < counts.Length; ++i)
            {
                counts[i] = new TempNode1() { ch = (char)('a' + i), count = 0 };
            }
            foreach(char ch in s)
            {
                int i = ch - 'a';
                ++counts[i].count;
            }
            Array.Sort(counts, (x, y) => y.count - x.count);

            int index = 0;
            char[] res = new char[s.Length];
            for(int i = 0; i < counts.Length; ++i)
            {
                if (counts[i].count > 0)
                {
                    int startIndex = index;
                    while(counts[i].count > 0)
                    {
                        if (res[index] == 0)
                        {
                            res[index] = counts[i].ch;
                            if (index > 0 && res[index-1] == res[index])
                            {
                                break;
                            }
                            --counts[i].count;
                            index += 2;
                        }
                        else
                        {
                            ++index;
                        }

                        index %= res.Length;
                        if (startIndex == index)
                            break;
                    }

                    if (counts[i].count > 0)
                        return string.Empty;
                }
            }

            return new string(res);
        }
    }
}
