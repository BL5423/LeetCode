using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Trie
{
    /**
     * Your WordDictionary object will be instantiated and called as such:
     * WordDictionary obj = new WordDictionary();
     * obj.AddWord(word);
     * bool param_2 = obj.Search(word);
     */

    public class TrieNode
    {
        private const int Size = 'z' - 'a' + 1;
        public TrieNode[] children;

        public bool IsLeaf;

        public TrieNode()
        {
            this.children = new TrieNode[Size];
        }

        public TrieNode Add(char c)
        {
            if (this.children[c - 'a'] == null)
            {
                var node = new TrieNode();
                this.children[c - 'a'] = node;

                return node;
            }
            else
            {
                // do not create child again if already exists
                return this.children[c - 'a'];
            }
        }
    }

    public class WordDictionary
    {
        private TrieNode root;

        public WordDictionary()
        {
            this.root = new TrieNode();
        }

        public void AddWord(string word)
        {
            var node = this.root;
            foreach(char c in word)
            {
                node = node.Add(c);
            }

            node.IsLeaf = true;
        }

        public bool Search(string word)
        {
            var nodes = new List<TrieNode>();
            nodes.Add(this.root);
            return this.SearchIterative(word, nodes);
        }

        private bool SearchIterative(string word, IList<TrieNode> nodes)
        {
            var parentNodes = nodes;
            for (int i = 0; i < word.Length && parentNodes.Count != 0; ++i)
            {
                var childNodes = new List<TrieNode>(nodes.Count);
                char c = word[i];
                if (c != '.')
                {
                    foreach (var node in parentNodes)
                    {
                        // go to next level
                        var childNode = node.children[c - 'a'];
                        if (childNode != null)
                            childNodes.Add(childNode);
                    }
                }
                else // c == '.'
                {
                    foreach (var node in parentNodes)
                    {
                        foreach (var childNode in node.children)
                        {
                            // '.' matches any existing child node so skip to grand child
                            if (childNode != null)
                            {
                                childNodes.Add(childNode);
                            }
                        }
                    }
                }

                parentNodes = childNodes;
            }

            return parentNodes.Any(node => node.IsLeaf);
        }

        private bool SearchRecursive(string word, int index, TrieNode node)
        {
            for (int i = index; i < word.Length && node != null; ++i)
            {
                char c = word[i];
                if (c != '.')
                {
                    // go to next level
                    node = node.children[c - 'a'];
                }
                else // c == '.'
                {
                    foreach (var childNode in node.children)
                    {
                        // '.' matches any existing child node so skip to grand child
                        if (childNode != null && this.SearchRecursive(word, i + 1, childNode))
                        {
                            return true;
                        }
                    }

                    // no match
                    return false;
                }
            }

            return node != null ? node.IsLeaf : false;
        }
    }

    public class WordDictionaryV1
    {
        bool endOfWord;

        int N = 26;

        WordDictionaryV1[] children;

        public WordDictionaryV1()
        {
            this.children = new WordDictionaryV1[N];
            this.endOfWord = false;
        }

        public void AddWord(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                int index = 0;
                WordDictionaryV1 root = this;
                while (index < word.Length)
                {
                    char character = word[index];
                    var kid = root.children[character - 'a'];
                    if (kid == null)
                    {
                        root.children[character - 'a'] = kid = new WordDictionaryV1();
                    }
                    root = kid;
                    ++index;
                }

                root.endOfWord = true;
            }
        }

        public bool Search(string word)
        {
            //if (!string.IsNullOrEmpty(word))
            {
                int index = 0;
                WordDictionaryV1 root = this;
                while (index < word.Length)
                {
                    char character = word[index];
                    if (character != '.')
                    {
                        var kid = root.children[character - 'a'];
                        if (kid == null)
                            return false;

                        root = kid;
                        ++index;
                    }
                    else
                    {
                        // '.' is the last character then it is matched
                        //if (++index >= word.Length)
                        //{
                        //    return root.children.Any(child => child != null && child.endOfWord);
                        //}

                        // Find any potential match of next character in word
                        foreach (var child in root.children)
                        {
                            if (child != null && child.Search(word.Substring(index + 1)))
                            {
                                return true;
                            }
                        }

                        return false;
                    }
                }

                return root.endOfWord;
            }

            return false;
        }
    }
}
