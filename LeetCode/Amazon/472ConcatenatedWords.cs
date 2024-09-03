using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _472ConcatenatedWords
    {
        public IList<string> FindAllConcatenatedWordsInADict(string[] words)
        {
            var trie = Trie.Build(words);
            int minLen = words.Min(w => w.Length);
            var res = new LinkedList<string>();
            foreach (var word in words)
            {
                if (word.Length > minLen && DP_BottomUp(word, trie))
                {
                    res.AddLast(word);
                }
            }

            return res.ToList();
        }

        private bool DP_BottomUp(string word, Trie trie)
        {
            // dp[i] indicates if word[i, ...] is valid/concatenated
            bool[] dp = new bool[word.Length + 1];
            dp[word.Length] = true; // base state
            for (int left = word.Length - 1; left >= 0; --left)
            {
                var subTrie = trie;
                for (int right = left; !dp[left] && right < (left != 0 ? word.Length : word.Length - 1); ++right)
                {
                    var nextTrie = subTrie.Get(word, right, right);
                    if (nextTrie != null)
                    {
                        dp[left] = nextTrie.end && dp[right + 1];
                        subTrie = nextTrie;
                    }
                    else
                        break;
                }

                /*
                //                                  do not check the whole word which by design exists in Trie
                for (int right = left; !dp[left] && right < (left != 0 ? word.Length : word.Length - 1); ++right)
                {
                    // word[left, ...] is valid/concatenated if and only if
                    // 1. word[left, ..., right] is valid/concatenated
                    // 2. word[right + 1, ...] is valid/concatenated
                    var subTrie = trie.Get(word, left, right);
                    dp[left] |= (subTrie != null && subTrie.end) && dp[right + 1];
                }

                */
            }

            return dp[0];
        }
    }

    public class Trie
    {
        private Trie[] kids;

        public bool end { get; private set; }

        private Trie()
        {
            this.kids = new Trie[26];
            this.end = false;
        }

        public static Trie Build(string[] words)
        {
            var root = new Trie();
            foreach (var word in words)
            {
                root.Add(word);
            }

            return root;
        }

        private void Add(string word)
        {
            var trie = this;
            foreach (var ch in word)
            {
                if (trie.kids[ch - 'a'] == null)
                    trie.kids[ch - 'a'] = new Trie();

                trie = trie.kids[ch - 'a'];
            }

            trie.end = true;
        }

        public Trie Get(string word, int start, int end)
        {
            var trie = this;
            for (int i = start; i <= end; ++i)
            {
                char ch = word[i];
                if (trie.kids[ch - 'a'] == null)
                    return null;

                trie = trie.kids[ch - 'a'];
            }

            return trie;
        }

        public bool Exists(string word, int start, int end)
        {
            var trie = this;
            for (int i = start; i <= end; ++i)
            {
                char ch = word[i];
                if (trie.kids[ch - 'a'] == null)
                    return false;

                trie = trie.kids[ch - 'a'];
            }

            return trie.end;
        }
    }
}
