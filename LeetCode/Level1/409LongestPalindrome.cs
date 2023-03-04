using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _409LongestPalindrome
    {
        public int LongestPalindrome(string s)
        {
            Dictionary<char, int> counts = new Dictionary<char, int>();
            foreach(char ch in s)
            {
                if (counts.TryGetValue(ch, out int count))
                {
                    ++counts[ch];
                }
                else
                {
                    counts.Add(ch, 1);
                }
            }

            int length = 0, odd = 0;
            foreach(var pair in counts)
            {
                if (pair.Value % 2 == 0)
                {
                    length += pair.Value;
                }
                else
                { 
                    length += (pair.Value - 1);
                    odd = 1;
                }
            }

            return length + odd;
        }
    }
}
