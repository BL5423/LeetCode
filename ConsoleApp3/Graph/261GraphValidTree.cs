using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _261GraphValidTree
    {
        public bool ValidTree_DFS(int n, int[][] edges)
        {
            if (n > 1)
            {
                var v2e = new HashSet<int>[n];
                foreach (var edge in edges)
                {
                    int s = edge[0];
                    int t = edge[1];
                    if (v2e[s] == null)
                    {
                        v2e[s] = new HashSet<int>();
                    }
                    v2e[s].Add(t);

                    if (v2e[t] == null)
                    {
                        v2e[t] = new HashSet<int>();
                    }
                    v2e[t].Add(s);
                }

                int seen = 0;
                if (edges.Length > 0)
                {
                    bool[] visited = new bool[n];
                    Stack<int> stack = new Stack<int>(n);
                    int source = edges[0][0];
                    stack.Push(source);
                    visited[source] = true;
                    ++seen;

                    while (stack.Count != 0)
                    {
                        var node = stack.Pop();
                        if (v2e[node] != null)
                        {
                            foreach (var adjNode in v2e[node])
                            {
                                if (visited[adjNode])
                                    return false;

                                ++seen;
                                visited[adjNode] = true;
                                v2e[adjNode].Remove(node);
                                stack.Push(adjNode);
                            }
                        }
                    }
                }

                return seen == n;
            }

            return true;
        }

        public bool ValidTree_BFS(int n, int[][] edges)
        {
            if (n > 1)
            {
                var v2e = new HashSet<int>[n];
                foreach(var edge in edges)
                {
                    int s = edge[0];
                    int t = edge[1];
                    if (v2e[s] == null)
                    {
                        v2e[s] = new HashSet<int>();
                    }
                    v2e[s].Add(t);

                    if (v2e[t] == null)
                    {
                        v2e[t] = new HashSet<int>();
                    }
                    v2e[t].Add(s);
                }

                int seen = 0;
                if (edges.Length > 0)
                {
                    bool[] visited = new bool[n];
                    Queue<int> queue = new Queue<int>(n);
                    int source = edges[0][0];
                    queue.Enqueue(source);
                    visited[source] = true;
                    ++seen;

                    while (queue.Count != 0)
                    {
                        var node = queue.Dequeue();
                        if (v2e[node] != null)
                        {
                            foreach (var adjNode in v2e[node])
                            {
                                if (visited[adjNode])
                                    return false;

                                ++seen;
                                visited[adjNode] = true;
                                v2e[adjNode].Remove(node);
                                queue.Enqueue(adjNode);
                            }
                        }
                    }
                }

                return seen == n;
            }

            return true;
        }

        public bool ValidTree_DJU(int n, int[][] edges)
        {
            int connected = 0;
            if (n > 1)
            {
                var dju = new DisjointUnion(n);
                for(int i = 0; i < edges.Length; ++i)
                {
                    if (!dju.Union(edges[i][0], edges[i][1]))
                    {
                        return false;
                    }
                    else
                    {
                        ++connected;
                    }
                }
            }

            return connected == n - 1;
        }
    }

    public class DisjointUnion
    {
        private int[] parents, ranks;

        public DisjointUnion(int n)
        {
            this.parents = new int[n];
            this.ranks = new int[n];
            for(int i = 0; i < n; ++i)
            {
                this.parents[i] = i;
                this.ranks[i] = i;
            }
        }

        public int GetParent(int index)
        {
            if (this.parents[index] == index)
            {
                return index;
            }

            return this.parents[index] = this.GetParent(this.parents[index]);
        }

        public bool Union(int index1, int index2)
        {
            int parent1 = this.GetParent(index1);
            int parent2 = this.GetParent(index2);

            if (parent1 == parent2)
                return false;

            if (this.ranks[parent1] > this.ranks[parent2])
            {
                this.parents[parent2] = parent1;
            }
            else if (this.ranks[parent1] < this.ranks[parent2])
            {
                this.parents[parent1] = parent2;
            }
            else
            {
                this.parents[parent2] = parent1;
                ++this.ranks[parent1];
            }

            return true;
        }
    }
}
