using LC.Top100Liked;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _785IsGraphBipartite
    {
        public bool IsBipartite(int[][] graph)
        {
            int[] colors = new int[graph.Length];
            var queue = new Queue<int>();
            for (int v = 0; v < graph.Length; ++v)
            {
                if (colors[v] == 0)
                {
                    colors[v] = 1;
                    queue.Enqueue(v);
                    while (queue.Count != 0)
                    {
                        for (int c = queue.Count; c > 0; --c)
                        {
                            int n = queue.Dequeue();
                            foreach (var u in graph[n])
                            {
                                if (colors[u] == colors[n])
                                    return false;

                                if (colors[u] == 0)
                                {
                                    colors[u] = colors[n] == 1 ? 2 : 1;
                                    queue.Enqueue(u);
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public bool IsBipartite_UnionFind(int[][] graph)
        {
            var uf = new UnionFind(graph.Length);
            for (int v = 0; v < graph.Length; ++v)
            {
                if (graph[v].Length > 0)
                {
                    int firstU = graph[v][0];
                    for (int u = 1; u < graph[v].Length; ++u)
                    {
                        uf.Union(firstU, graph[v][u]);
                    }

                    if (uf.GetParent(v) == uf.GetParent(firstU))
                        return false;
                }
            }

            return true;
        }
    }
}
