using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _187RepeatedDNASequences
    {
        private const int L = 10;

        private static Dictionary<char, int> mapping = new Dictionary<char, int>()
        {
            { 'A', 0 /* 00 */ },
            { 'C', 1 /* 01 */ },
            { 'G', 2 /* 10 */ },
            { 'T', 3 /* 11 */ },
        };

        public IList<string> FindRepeatedDnaSequences(string s)
        {
            HashSet<int> masks = new HashSet<int>(s.Length / L + 1);
            var res = new HashSet<string>();

            //int highMask = (int)Math.Pow(2, 20) - 1;
            int highMask = ~(3 << L * 2);
            int mask = 0;
            int start = 0, index = 0;
            while (index < s.Length)
            {
                // take in s[index] and fade out any characters beyond index - L
                mask <<= 2;
                mask &= highMask;
                mask |= mapping[s[index]];

                if (index - start + 1 == L)
                {
                    if (masks.Contains(mask))
                    {
                        res.Add(s.Substring(start, L));
                    }

                    masks.Add(mask);
                    ++start;
                }

                ++index;
            }

            return res.ToList();
        }
    }
}
