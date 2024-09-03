using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _161OneEditDistance
    {
        public bool IsOneEditDistance(string s, string t)
        {
            int indexS = 0, indexT = 0;
            if (Math.Abs(s.Length - t.Length) > 1)
                return false;

            while (indexS < s.Length && indexT < t.Length)
            {
                if (s[indexS] == t[indexT])
                {
                    ++indexS;
                    ++indexT;
                }
                else // s[indexS] != t[indexT] 
                {
                    if (s.Length > t.Length)
                        return string.Compare(s, indexS + 1, t, indexT, t.Length - indexT) == 0;
                    else if (s.Length < t.Length)
                        return string.Compare(s, indexS, t, indexT + 1, t.Length - indexT - 1) == 0;
                    else
                        return string.Compare(s, indexS + 1, t, indexT + 1, t.Length - indexT - 1) == 0;
                }
            }

            return false;
        }
    }
}
