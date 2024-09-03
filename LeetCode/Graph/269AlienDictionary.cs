using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Graph
{
    public class _269AlienDictionary
    {
        public string AlienOrder(string[] words)
        {
            int uniqueChars = 0;
            int[] ingress = new int[26];
            Dictionary<char, HashSet<char>> node2edges = new Dictionary<char, HashSet<char>>(26);
            foreach(var word in words)
            {
                foreach(var ch in  word)
                { 
                    if (!node2edges.ContainsKey(ch))
                    {
                        ++uniqueChars;
                        node2edges.Add(ch, new HashSet<char>(26));
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < words.Length - 1; ++i)
            {
                // word1 is ahead of word2
                var word1 = words[i];
                var word2 = words[i + 1];
                bool match = false;
                for (int j = 0; j < Math.Min(word1.Length, word2.Length); ++j)
                {
                    if (word1[j] != word2[j])
                    {
                        if (node2edges[word2[j]].Contains(word1[j]))
                            return string.Empty;

                        var edges = node2edges[word1[j]];
                        if (edges.Add(word2[j]))
                        {
                            ++ingress[word2[j] - 'a'];
                        }

                        match = true;
                        break;
                    }
                }

                if (!match && word1.Length > word2.Length)
                    return string.Empty;
            }

            var starters = new LinkedList<char>();
            for (int i = 0; i < 26; ++i)
            {
                if (ingress[i] == 0 && node2edges.ContainsKey((char)(i + 'a')))
                    starters.AddLast((char)(i + 'a'));
            }

            while (starters.Count != 0)
            {
                var next = new LinkedList<char>();
                foreach (var starter in starters)
                {
                    sb.Append(starter);
                    if (node2edges.TryGetValue(starter, out HashSet<char> edges))
                    {
                        foreach (var edge in edges)
                        {
                            if (--ingress[edge - 'a'] == 0)
                                next.AddLast(edge);
                        }

                        node2edges.Remove(starter);
                    }
                }

                starters = next;
            }

            return sb.Length == uniqueChars ? sb.ToString() : string.Empty;
        }

        const int SIZE = 'z' - 'a' + 1;

        public string AlienOrderV1(string[] words)
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
