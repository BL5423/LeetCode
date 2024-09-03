using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _642DesignSearchAutocompleteSystem
    {
        private const int N = 3;

        private TrieV2 root, curNode;

        private StringBuilder sb;

        public _642DesignSearchAutocompleteSystem(string[] sentences, int[] times)
        {
            this.root = TrieV2.Build(sentences, times);
            this.sb = new StringBuilder();
        }

        public IList<string> Input(char c)
        {
            if (this.curNode == null)
            {
                if (c != '#')
                {
                    if (this.sb.Length == 0)
                        this.curNode = this.root.Query(c);
                    this.sb.Append(c);
                }
                else
                {
                    this.root.Add(sb.ToString(), 1);
                    this.sb = new StringBuilder();
                }

                return this.Get(this.curNode);
            }
            else
            {
                if (c != '#')
                {
                    this.sb.Append(c);
                    this.curNode = this.curNode.Query(c);
                    return this.Get(this.curNode);
                }
                else
                {
                    this.root.Add(sb.ToString(), 1);
                    this.sb = new StringBuilder();
                    this.curNode = null;

                    return Array.Empty<string>();
                }
            }
        }

        private IList<string> Get(TrieV2 node)
        {
            if (node == null)
                return Array.Empty<string>();

            var dic = node.Words;
            var queue = new PriorityQueue<string, Weight>();
            foreach (var kv in dic)
            {
                queue.Enqueue(kv.Key, new Weight(kv.Key, -kv.Value));
            }

            var res = new List<string>(N);
            while (res.Count < N && queue.Count != 0)
            {
                res.Add(queue.Dequeue());
            }

            return res;
        }
    }

    public class Weight : IComparable<Weight>
    {
        private string word;

        private int count;

        public Weight(string word, int count)
        {
            this.word = word;
            this.count = count;
        }

        public int CompareTo(Weight? other)
        {
            if (this.count != other.count)
                return this.count.CompareTo(other.count);

            return this.word.CompareTo(other.word);
        }
    }
    /**
     * Your AutocompleteSystem object will be instantiated and called as such:
     * AutocompleteSystem obj = new AutocompleteSystem(sentences, times);
     * IList<string> param_1 = obj.Input(c);
     */

    public class TrieV2
    {
        private const int N = 33;

        private TrieV2[] children;

        private IDictionary<string, int> words;

        public IDictionary<string, int> Words { get { return words; } }

        private TrieV2()
        {
            this.children = new TrieV2[N];
            this.words = new Dictionary<string, int>();
        }

        public static TrieV2 Build(string[] words, int[] counts)
        {
            var root = new TrieV2();
            for (int i = 0; i < words.Length; ++i)
            {
                root.Add(words[i], counts[i]);
            }
            return root;
        }

        public void Add(string word, int count)
        {
            var node = this;
            foreach (var ch in word)
            {
                int index = GetIndex(ch);
                if (node.children[index] == null)
                {
                    node.children[index] = new TrieV2();
                }

                node = node.children[index];
                if (!node.words.ContainsKey(word))
                {
                    node.words.Add(word, 0);
                }

                node.words[word] += count;
            }
        }

        public TrieV2 Query(char c)
        {
            return this.children[GetIndex(c)];
        }

        private static int GetIndex(char ch)
        {
            if (ch >= 'a' && ch <= 'z')
                return ch - 'a';

            // ch == ' '
            return (int)ch;
        }
    }
}
