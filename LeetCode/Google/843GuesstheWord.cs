using MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _843GuesstheWord
    {
        public const int N = 6;

        public void FindSecretWord(string[] words, Master master)
        {
            int len = words.Length;
            IList<int> candidates = new List<int>(len);
            for (int i = 0; i < len; ++i)
            {
                candidates.Add(i);
            }

            int index = new Random().Next(0, candidates.Count);
            bool[] used = new bool[len];
            //for(int i = 0; i < 10; ++i)
            while (true)
            {
                used[candidates[index]] = true;
                string word = words[candidates[index]];
                int match = master.Guess(word);
                if (match == N)
                {
                    break;
                }

                var list = new List<int>(len);
                for (int j = 0; j < candidates.Count; ++j)
                {
                    if (Match(word, words[candidates[j]]) == match && used[candidates[j]] == false)
                        list.Add(candidates[j]);
                }

                candidates = list;
                index = new Random().Next(0, candidates.Count);
            }
        }

        public static int Match(string word1, string word2)
        {
            int match = 0;
            for (int i = 0; i < word1.Length; ++i)
            {
                if (word1[i] == word2[i])
                    ++match;
            }
            return match;
        }
    }

    public class Master
    {
        private string secret;

        public Master(string s)
        {
            this.secret = s;
        }

        public int Guess(string word)
        {
            int match = 0;
            for (int i = 0; i < word.Length; ++i)
            {
                if (word[i] == this.secret[i])
                    ++match;
            }
            return match;
        }
    }

}
