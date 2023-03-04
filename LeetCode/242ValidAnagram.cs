using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _242ValidAnagram
    {
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length)
                return false;

            int[] counters = new int[26];

            for(int index = 0; index < s.Length; ++index)
            {
                ++counters[s[index] - 'a'];
                --counters[t[index] - 'a'];
            }

            return !counters.Any(c => c != 0);
        }
    }
}
