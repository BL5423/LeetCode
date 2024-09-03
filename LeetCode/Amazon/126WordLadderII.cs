using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _126WordLadderII
    {
        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            var res = new List<IList<string>>();
            var dic = new HashSet<string>(wordList);
            if (!dic.Contains(endWord))
                return res;

            var adj = new Dictionary<string, LinkedList<string>>(dic.Count + 1);
            var queue = new Queue<string>();
            var visited = new Dictionary<string, int>(dic.Count + 1);
            queue.Enqueue(beginWord);
            visited.Add(beginWord, 0);
            bool reached = false;
            while (queue.Count != 0 && !reached)
            {
                for (int c = queue.Count; c > 0; --c)
                {
                    var word = queue.Dequeue();
                    if (word == endWord)
                    {
                        reached = true;
                        break;
                    }
                    else
                    {
                        foreach (var nextWord in GetNextWords(word))
                        {
                            if (nextWord != word && dic.Contains(nextWord))
                            {
                                if (!visited.TryGetValue(nextWord, out int layer))
                                {
                                    visited.Add(nextWord, visited[word] + 1);
                                    var neighbors = new LinkedList<string>();
                                    neighbors.AddLast(word);
                                    adj.Add(nextWord, neighbors);

                                    queue.Enqueue(nextWord);
                                }
                                else if (layer == visited[word] + 1) // is nextWord reachable by multiple words on the prior layer
                                {
                                    adj[nextWord].AddLast(word);
                                }
                            }
                        }
                    }
                }
            }

            if (reached)
            {
                // dfs
                Stack<(string, LinkedListNode<string>)> stack = new Stack<(string, LinkedListNode<string>)>();
                var list = new LinkedList<string>();
                stack.Push((endWord, adj[endWord].First));
                list.AddLast(endWord);
                while (stack.Count != 0)
                {
                    var item = stack.Pop();
                    var node = item.Item2;
                    if (node != null)
                    {
                        stack.Push((item.Item1, node.Next));
                        if (node.Value != beginWord)
                        {
                            stack.Push((node.Value, adj[node.Value].First));
                            list.AddLast(node.Value);
                        }
                        else
                        {
                            list.AddLast(node.Value);
                            res.Add(list.Reverse().ToList());
                            list.RemoveLast();

                            stack.Pop();
                            list.RemoveLast();
                        }
                    }
                    else
                    {
                        list.RemoveLast();
                    }
                }
            }

            return res;
        }

        private static IEnumerable<string> GetNextWords(string word)
        {
            for (int i = 0; i < word.Length; ++i)
            {
                for (int j = 0; j < 26; ++j)
                {
                    yield return string.Concat(word.Substring(0, i), (char)('a' + j), word.Substring(i + 1));
                }
            }
        }
    }
}
