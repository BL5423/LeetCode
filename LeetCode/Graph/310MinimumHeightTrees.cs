using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _310MinimumHeightTrees
    {
        public IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            var v2e = new Dictionary<int, HashSet<int>>(n);
            foreach(var edge in edges)
            {
                int v1 = edge[0];
                int v2 = edge[1];
                if (!v2e.TryGetValue(v1, out HashSet<int> e1))
                {
                    e1 = new HashSet<int>(2);
                    v2e[v1] = e1;
                }
                e1.Add(v2);

                if (!v2e.TryGetValue(v2, out HashSet<int> e2))
                {
                    e2 = new HashSet<int>(2);
                    v2e[v2] = e2;
                }
                e2.Add(v1);
            }

            int totalLeft = n;
            Queue<int> queue = new Queue<int>();
            for(int i = 0; i < n; ++i)
            {
                if (!v2e.TryGetValue(i, out HashSet<int> e) || e.Count == 1)
                {
                    queue.Enqueue(i);
                }
            }

            while (queue.Count > 0)
            {
                int count = queue.Count;
                totalLeft -= count;
                if (totalLeft == 0)
                    break;

                for (int c = 0; c < count; ++c)
                {
                    var node = queue.Dequeue();
                    var enumerator = v2e[node].GetEnumerator();
                    enumerator.MoveNext();
                    var nextNode = enumerator.Current;
                    v2e[nextNode].Remove(node);
                    if (v2e[nextNode].Count == 1)
                    {
                        queue.Enqueue(nextNode);
                    }
                }
            }

            return new List<int>(queue);
        }

        public IList<int> FindMinHeightTrees_BFS(int n, int[][] edges)
        {
            // time outs
            Dictionary<int, LinkedList<int>> v2e = new Dictionary<int, LinkedList<int>>(n);
            foreach(var edge in edges)
            {
                int v1 = edge[0];
                int v2 = edge[1];
                if (!v2e.TryGetValue(v1, out LinkedList<int> e1))
                {
                    e1 = new LinkedList<int>();
                    v2e[v1] = e1;
                }
                e1.AddLast(v2);

                if (!v2e.TryGetValue(v2, out LinkedList<int> e2))
                {
                    e2 = new LinkedList<int>();
                    v2e[v2] = e2;
                }
                e2.AddLast(v1);
            }
            
            int minHeight = int.MaxValue;
            int[] heights = new int[n];
            for(int i = 0; i < n; ++i)
            {
                heights[i] = int.MaxValue;
            }
            int[] visited = new int[n];
            Queue<int> queue = new Queue<int>(n);
            for (int i = 1; i <= n; ++i)
            {
                int index = i - 1;
                int root = index;
                queue.Enqueue(root);
                visited[index] = i;
                int layers = 0;
                while (queue.Count > 0 && layers < minHeight)
                {
                    ++layers;
                    int count = queue.Count;
                    for (int c = 0; c < count && layers < minHeight; ++c)
                    {
                        var node = queue.Dequeue();
                        if (v2e.TryGetValue(node, out LinkedList<int> nextNodes))
                        {
                            foreach(var nextNode in nextNodes)
                            {
                                if (visited[nextNode] != i)
                                {
                                    if (layers <= heights[nextNode])
                                    {
                                        visited[nextNode] = i;
                                        queue.Enqueue(nextNode);
                                    }
                                    else
                                    {
                                        // donot have to go deeper
                                        layers = int.MaxValue;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (queue.Count == 0)
                {
                    heights[index] = layers;
                    if (layers < minHeight)
                    {
                        minHeight = layers;
                    }
                }
                else
                {
                    heights[index] = int.MaxValue;
                    queue.Clear();
                }                
            }

            var res = new List<int>(n);
            for(int i = 0; i < n; ++i)
            {
                if (heights[i] == minHeight)
                    res.Add(i);
            }

            return res;
        }
    }
}
