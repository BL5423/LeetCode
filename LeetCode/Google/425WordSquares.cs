using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _425WordSquares
    {
        public IList<IList<string>> WordSquares(string[] words)
        {
            var res = new List<IList<string>>();
            var trie = Trie.Build(words);
            var list = new LinkedList<string>();
            for (int i = 0; i < words.Length; ++i)
            {
                list.AddLast(words[i]);

                Square(words, list, trie, res);

                list.RemoveLast();
            }

            return res;
        }

        private void Square(string[] words, LinkedList<string> list, Trie trie, IList<IList<string>> res)
        {
            if (list.Count == words[0].Length)
            {
                res.Add(list.ToList());
                return;
            }

            foreach (var match in trie.Match(Prefix(list)))
            {
                list.AddLast(match);

                Square(words, list, trie, res);

                list.RemoveLast();
            }
        }

        private static string Prefix(LinkedList<string> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string word in list)
            {
                sb.Append(word[list.Count]);
            }

            return sb.ToString();
        }
    }

    public class Trie
    {
        private Trie[] children;

        private LinkedList<string> words;

        private Trie()
        {
            this.children = new Trie['z' - 'a' + 1];
            this.words = new LinkedList<string>();
        }

        public static Trie Build(string[] words)
        {
            var root = new Trie();
            foreach (var word in words)
                root.Add(word);
            return root;
        }

        private void Add(string word)
        {
            Trie trie = this;
            for (int i = 0; i < word.Length; ++i)
            {
                char ch = word[i];
                if (trie.children[ch - 'a'] == null)
                    trie.children[ch - 'a'] = new Trie();

                trie = trie.children[ch - 'a'];
                trie.words.AddLast(word);
            }
        }

        public IEnumerable<string> Match(string prefix)
        {
            Trie trie = this;
            foreach (var ch in prefix)
            {
                if (trie.children[ch - 'a'] != null)
                    trie = trie.children[ch - 'a'];
                else
                    return Array.Empty<string>();
            }

            return trie.words;
        }
    }
}
