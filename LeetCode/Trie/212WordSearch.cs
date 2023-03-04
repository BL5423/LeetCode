using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Trie
{
    public class _212WordSearch
    {
        public IList<string> FindWords(char[][] board, string[] words)
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
}
