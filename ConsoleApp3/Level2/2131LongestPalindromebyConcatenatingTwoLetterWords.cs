using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _2131LongestPalindromebyConcatenatingTwoLetterWords
    {
        public int LongestPalindrome(string[] words)
        {
            char[,] counts = new char[26, 26];
            int length = 0, pairs = 0;
            foreach(var word in words)
            {
                int ch1 = word[0] - 'a';
                int ch2 = word[1] - 'a';
                if (counts[ch2, ch1] > 0)
                {
                    --counts[ch2, ch1];
                    length += 4;
                    if (ch1 == ch2)
                    {
                        --pairs;
                    }
                }
                else
                {
                    ++counts[ch1, ch2];
                    if (ch1 == ch2)
                    {
                        ++pairs;
                    }
                }
            }

            return length + (pairs > 0 ? 2 : 0);
        }

        public int LongestPalindromeV1(string[] words)
        {
            Dictionary<int, int> seen = new Dictionary<int, int>();
            Dictionary<int, int> dups = new Dictionary<int, int>();
            int length = 0;
            foreach (var word in words)
            {
                int n = Convert(word, out int reverse);
                if (n != reverse)
                {
                    if (seen.TryGetValue(n, out int count1))
                    {
                        seen[n] = ++count1;
                    }
                    else
                    {
                        count1 = 1;
                        seen.Add(n, count1);
                    }

                    if (seen.TryGetValue(reverse, out int count2) && count2 > 0)
                    {
                        length += 4;
                        --seen[reverse];
                        --seen[n];
                    }
                }
                else
                {
                    if (dups.TryGetValue(n, out int count))
                    {
                        ++dups[n];
                    }
                    else
                    {
                        dups.Add(n, 1);
                    }
                }
            }

            bool center = false;
            foreach (var word in dups)
            {
                length += (word.Value / 2) * 4;
                if (!center && (word.Value & 1) == 1)
                {
                    length += 2;
                    center = true;
                }
            }

            return length;
        }

        private int Convert(string word, out int reverse)
        {
            int ret = (word[0] - 'a') * 26 + word[1] - 'a';
            reverse = (word[1] - 'a') * 26 + word[0] - 'a';
            return ret;
        }
    }
}
