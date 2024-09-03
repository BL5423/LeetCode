using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Trie
{
    public class _212WordSearch
    {
        private static int[,] dirs = new int[,] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };

        public IList<string> FindWords(char[][] board, string[] words)
        {
            var dictionary = new HashSet<string>(words);
            var trie = TrieV1.Build(words);
            int rows = board.Length, cols = board[0].Length;
            bool[,] inStack = new bool[rows, cols];

            var res = new HashSet<string>();
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < cols; ++c)
                {
                    inStack[r, c] = true;
                    var stack = new Stack<State>();
                    var sb = new LinkedList<char>();
                    sb.AddLast(board[r][c]);
                    stack.Push(new State(r, c));
                    while (stack.Count != 0)
                    {
                        var state = stack.Peek();
                        if (state.dir == 4 || !trie.Match(sb).Any())
                        {
                            if (state.dir == 4)
                            {
                                var str = string.Join("", sb);
                                if (dictionary.Contains(str))
                                {
                                    res.Add(str);
                                    trie.Remove(str);
                                }
                            }

                            sb.RemoveLast();
                            inStack[state.r, state.c] = false;
                            stack.Pop();
                        }
                        else
                        {
                            int nextR = state.r + dirs[state.dir, 0], nextC = state.c + dirs[state.dir, 1];
                            ++state.dir;
                            if (nextR < 0 || nextR >= rows || nextC < 0 || nextC >= cols ||
                               inStack[nextR, nextC])
                                continue;

                            inStack[nextR, nextC] = true;
                            sb.AddLast(board[nextR][nextC]);
                            stack.Push(new State(nextR, nextC));
                        }
                    }
                }
            }

            return res.ToList();
        }

        public IList<string> FindWordsV1(char[][] board, string[] words)
        {
            var dic = new Trie();
            foreach(var word in words)
            {
                dic.Insert(word);
            }

            return DFS(board, dic);
        }

        private IList<string> DFS(char[][] board, Trie dic)
        {
            LinkedList<char> list = new LinkedList<char>();
            HashSet<string> results = new HashSet<string>();
            for(int l = 0; l < board.Length; ++l)
            {
                for(int h = 0; h < board[l].Length; ++h)
                {
                    if (board[l][h] != '0')
                    {
                        DFSRecursive(board, l, h, board.Length, board[l].Length, list, dic, results);
                    }
                }
            }

            return results.ToList();
        }

        private void DFSRecursive(char[][] board, int l, int h, int lSize, int hSize, LinkedList<char> list, Trie dic, HashSet<string> results)
        {
            char ch = board[l][h];
            list.AddLast(ch);
            if (!dic.StartsWith(ch))
            {
                list.RemoveLast();
                return;
            }

            var nextTrieNode = dic.children[ch - 'a'];
            if (nextTrieNode.endOfWord)
            {
                var str = new string(list.ToArray());
                results.Add(str);
            }

            board[l][h] = '0';
            if (l - 1 >= 0 && board[l - 1][h] != '0')
            {
                DFSRecursive(board, l - 1, h, lSize, hSize, list, nextTrieNode, results);
            }
            if (l + 1 < lSize && board[l + 1][h] != '0')
            {
                DFSRecursive(board, l + 1, h, lSize, hSize, list, nextTrieNode, results);
            }
            if (h - 1 >= 0 && board[l][h - 1] != '0')
            {
                DFSRecursive(board, l, h - 1, lSize, hSize, list, nextTrieNode, results);
            }
            if (h + 1 < hSize && board[l][h + 1] != '0')
            {
                DFSRecursive(board, l, h + 1, lSize, hSize, list, nextTrieNode, results);
            }

            list.RemoveLast();
            board[l][h] = ch;
        }
    }


    public class State
    {
        public int r, c, dir;

        public State(int r, int c)
        {
            this.r = r;
            this.c = c;
            this.dir = 0;
        }
    }

    public class TrieV1
    {
        private LinkedList<string> words;

        private TrieV1[] children;

        private TrieV1()
        {
            this.words = new LinkedList<string>();
            this.children = new TrieV1['z' - 'a' + 1];
        }

        public static TrieV1 Build(string[] words)
        {
            var root = new TrieV1();
            foreach (var word in words)
                root.Add(word);

            return root;
        }

        private void Add(string word)
        {
            var node = this;
            foreach (var ch in word)
            {
                int index = ch - 'a';
                if (node.children[index] == null)
                    node.children[index] = new TrieV1();

                node = node.children[index];
                node.words.AddLast(word);
            }
        }

        public void Remove(string word)
        {
            TrieV1 node = this;
            foreach (var ch in word)
            {
                int index = ch - 'a';
                if (node.children[index] != null)
                {
                    var parent = node;
                    node = node.children[index];
                    node.words.Remove(word);
                    if (node.words.Count == 0)
                    {
                        parent.children[index] = null;
                    }
                }
                else
                    return;
            }
        }

        public IEnumerable<string> Match(IEnumerable<char> prefix)
        {
            var node = this;
            foreach (var ch in prefix)
            {
                int index = ch - 'a';
                if (node.children[index] != null)
                    node = node.children[index];
                else
                    return Array.Empty<string>();
            }

            return node.words;
        }
    }
}
