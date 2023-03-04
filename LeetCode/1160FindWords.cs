using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _1160FindWords
    {
        public int CountCharacters(string[] words, string chars)
        {
            int[] characters = new int[26];
            foreach(var ch in chars)
            {
                ++characters[ch - 'a'];
            }

            int length = 0;
            foreach(var word in words)
            {
                bool found = true;
                int index = 0;
                while(index < word.Length)
                {
                    var count = --characters[word[index++] - 'a'];
                    if (count < 0)
                    {
                        found = false;
                        break;
                    }
                }

                if(found)
                {
                    length += word.Length;
                }

                // restore characters
                for (int i = 0; i < index; ++i)
                {
                    ++characters[word[i] - 'a'];
                }
            }

            return length;
        }
    }
}
