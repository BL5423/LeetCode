using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Graph
{
    public class _269AlienDictionary
    {
        const int SIZE = 'z' - 'a' + 1;

        public string AlienOrder(string[] words)
        {
            int[] indegree = new int[SIZE];
            for(int i = 0; i < SIZE; ++i)
            {
                indegree[i] = -1;
            }

            int uniqueChars = 0;
            for (int i = 0; i < words.Length; ++i)
            {
                foreach(var ch in words[i])
                {
                    if (indegree[ch - 'a'] != 0)
                    {
                        ++uniqueChars;
                    }
                        
                    indegree[ch - 'a'] = 0;
                }
            }

            var adjacencies = new LinkedList<char>[SIZE];
            for (int i = 1; i < words.Length; ++i)
            {
                string prev = words[i - 1];
                string cur = words[i];
                int index1 = 0, index2 = 0;
                bool match = false;
                while (index1 < prev.Length && index2 < cur.Length)
                {
                    if (prev[index1] != cur[index2])
                    {
                        var outdegree = adjacencies[prev[index1] - 'a'];
                        if (outdegree == null)
                        {
                            outdegree = new LinkedList<char>();
                            adjacencies[prev[index1] - 'a'] = outdegree;
                        }
                        outdegree.AddLast(cur[index2]);
                        ++indegree[cur[index2] - 'a'];
                        match = true;
                        break;
                    }

                    ++index1;
                    ++index2;
                }

                if (!match && prev.Length > cur.Length)
                {
                    return string.Empty;
                }
            }

            Queue<char> queue = new Queue<char>();
            for(int i = 0; i < SIZE; ++i)
            {
                if (indegree[i] == 0)
                {
                    queue.Enqueue((char)(i + 'a'));
                }
            }

            StringBuilder sb = new StringBuilder(SIZE);
            while (queue.Count != 0)
            {
                var ch = queue.Dequeue();
                sb.Append(ch);

                if (adjacencies[ch - 'a'] != null)
                {
                    foreach(var nextCh in adjacencies[ch - 'a'])
                    {
                        if (--indegree[nextCh - 'a'] == 0)
                        {
                            queue.Enqueue(nextCh);
                        }
                    }
                }
            }

            return sb.Length == uniqueChars ? sb.ToString() : string.Empty;
        }
    }
}
