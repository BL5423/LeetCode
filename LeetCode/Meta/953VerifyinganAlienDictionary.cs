using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _953VerifyinganAlienDictionary
    {
        public bool IsAlienSorted(string[] words, string order)
        {
            int[] char2order = new int[26];
            for (int i = 0; i < order.Length; ++i)
            {
                char2order[order[i] - 'a'] = i;
            }

            for (int index = 0; index < words.Length - 1; ++index)
            {
                int prefixLen = 0;
                string word1 = words[index];
                string word2 = words[index + 1];
                for (int i = 0; i < Math.Min(word1.Length, word2.Length); ++i)
                {
                    if (char2order[word1[i] - 'a'] > char2order[word2[i] - 'a'])
                        return false; // negative case
                    else if (char2order[word1[i] - 'a'] < char2order[word2[i] - 'a'])
                        break; // skip for next pair of words
                    else
                    {
                        ++prefixLen;
                    }
                }

                if (prefixLen == word2.Length && prefixLen < word1.Length)
                    return false;
            }

            return true;
        }
    }
}
