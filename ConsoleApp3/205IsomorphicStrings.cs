using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _205IsomorphicStrings
    {
        public bool IsIsomorphic(string s, string t)
        {
            HashSet<char> tChars = new HashSet<char>(t.Length);
            Dictionary<char, char> s2t = new Dictionary<char, char>(128);
            for (int index =0; index < s.Length; ++index)
            {
                char c;
                if (s2t.TryGetValue(s[index], out c))
                {
                    if (c != t[index])
                    {
                        return false;
                    }
                }
                else
                {
                    if (tChars.Contains(t[index]))
                        return false;

                    s2t[s[index]] = t[index];
                    tChars.Add(t[index]);
                }
            }

            return true;
        }
    }
}
