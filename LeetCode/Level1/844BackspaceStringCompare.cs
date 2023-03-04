using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _844BackspaceStringCompare
    {
        public bool BackspaceCompare(string s, string t)
        {
            int indexS = GetNext(s, s.Length - 1);
            int indexT = GetNext(t, t.Length - 1);
            while (indexS >= 0 && indexT >= 0)
            {
                if (s[indexS] != t[indexT])
                {
                    return false;
                }

                indexS = GetNext(s, indexS - 1);
                indexT = GetNext(t, indexT - 1);
            }

            return indexS == indexT;
        }

        private int GetNext(string s, int index)
        {
            int ret = -1;
            int backSpace = 0;
            while (index >= 0)
            {
                char ch = s[index];
                if (ch == '#')
                {
                    ++backSpace;
                }
                else
                {
                    if (backSpace == 0)
                    {
                        ret = index;
                        break;
                    }
                    else
                    {
                        --backSpace;
                    }
                }

                --index;
            }

            return ret;
        }

        private int[] GetCounts(string s)
        {
            int[] sCounts = new int[26];
            int index = s.Length - 1;
            int backSpaces = 0;
            while (index >= 0)
            {
                char ch = s[index];
                if (s[index] == '#')
                {
                    ++backSpaces;
                }
                else
                {
                    if (backSpaces == 0)
                    {
                        ++sCounts[ch - 'a'];
                    }
                    else
                    {
                        --backSpaces;
                    }
                }

                --index;
            }

            return sCounts;
        }
    }
}
