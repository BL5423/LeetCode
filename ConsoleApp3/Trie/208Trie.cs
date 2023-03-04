using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Trie
{
    public class Trie
    {
        public bool endOfWord;

        public Trie[] children;

        public Trie()
        {
            this.children = new Trie[26];
            this.endOfWord = false;
        }

        public void Insert(string word)
        {
            //if (!string.IsNullOrEmpty(word))
            //    this.InsertRecursive(word, 0);

            if (!string.IsNullOrEmpty(word))
            {
                int index = 0;
                Trie root = this;
                while(index < word.Length)
                {
                    char character = word[index];
                    var kid = root.children[character - 'a'];
                    if (kid == null)
                    {
                        root.children[character - 'a'] = kid = new Trie();
                    }
                    root = kid;
                    ++index;
                }

                root.endOfWord = true;
            }
        }

        private void InsertRecursive(string word, int index)
        {
            if (index == word.Length)
            {
                this.endOfWord = true;
                return;
            }

            char character = word[index];
            var kid = this.children[character - 'a'];
            if (kid == null)
            {
                this.children[character - 'a'] = kid = new Trie();
            }

            kid.InsertRecursive(word, index + 1);
        }

        public bool Search(string word)
        {
            //if (!string.IsNullOrEmpty(word))
            //    return this.SearchRecursive(word, 0, true);

            if (!string.IsNullOrEmpty(word))
            {
                int index = 0;
                Trie root = this;
                while (index < word.Length)
                {
                    char character = word[index];
                    var kid = root.children[character - 'a'];
                    if (kid == null)
                        return false;
                    root = kid;
                    ++index;
                }

                return root.endOfWord;
            }

            return false;
        }

        public bool SearchRecursive(string word, int index, bool expectEnd)
        {
            if (index == word.Length)
                return expectEnd ? this.endOfWord : true;

            char character = word[index];
            var kid = this.children[character - 'a'];
            if (kid == null)
            {
                return false;
            }

            return kid.SearchRecursive(word, index + 1, expectEnd);
        }

        public bool StartsWith(string prefix)
        {
            //if (!string.IsNullOrEmpty(prefix))
            //    return this.SearchRecursive(prefix, 0, false);

            if (!string.IsNullOrEmpty(prefix))
            {
                int index = 0;
                Trie root = this;
                while (index < prefix.Length)
                {
                    char character = prefix[index];
                    var kid = root.children[character - 'a'];
                    if (kid == null)
                        return false;
                    root = kid;
                    ++index;
                }

                return true;
            }

            return false;
        }


        public bool StartsWith(char ch)
        {
            return this.children[ch - 'a'] != null;
        }
    }
}
