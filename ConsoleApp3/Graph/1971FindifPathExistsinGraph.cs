using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _1971FindifPathExistsinGraph
    {
        public bool ValidPath_DSU(int n, int[][] edges, int source, int destination)
        {
            if (source == destination)
                return true;

            DSU dsu = new DSU(n);
            foreach(var edge in edges)
            {
                dsu.Union(edge[0], edge[1]);
            }

            return dsu.GetParent(source) == dsu.GetParent(destination);
        }
                
        public bool ValidPath_BFS(int n, int[][] edges, int source, int destination)
        {
            if (source == destination)
                return true;

            var v2v = new Dictionary<int, LinkedList<int>>(n);
            foreach (var edge in edges)
            {
                int s = edge[0];
                int t = edge[1];
                if (!v2v.TryGetValue(s, out LinkedList<int> v1))
                {
                    v1 = new LinkedList<int>();
                    v2v.Add(s, v1);
                }
                v1.AddLast(t);

                if (!v2v.TryGetValue(t, out LinkedList<int> v2))
                {
                    v2 = new LinkedList<int>();
                    v2v.Add(t, v2);
                }
                v2.AddLast(s);
            }

            bool[] seen = new bool[n];
            Queue<int> queue = new Queue<int>(n);
            queue.Enqueue(source);
            seen[source] = true;

            while (queue.Count != 0)
            {
                var node = queue.Dequeue();

                if (node == destination)
                    return true;

                if (v2v.TryGetValue(node, out LinkedList<int> adjs))
                {
                    foreach(var adj in adjs)
                    {
                        if (seen[adj] == false)
                        {
                            queue.Enqueue(adj);
                            seen[adj] = true;
                        }
                    }
                }
            }

            return false;
        }

        public bool ValidPath_DFS(int n, int[][] edges, int source, int destination)
        {
            if (source == destination)
                return true;

            var v2v = new Dictionary<int, LinkedList<int>>(n);
            foreach(var edge in edges)
            {
                int s = edge[0];
                int t = edge[1];
                if (!v2v.TryGetValue(s, out LinkedList<int> v1))
                {
                    v1 = new LinkedList<int>();
                    v2v.Add(s, v1);
                }
                v1.AddLast(t);

                if (!v2v.TryGetValue(t, out LinkedList<int> v2))
                {
                    v2 = new LinkedList<int>();
                    v2v.Add(t, v2);
                }
                v2.AddLast(s);
            }

            bool[] visited = new bool[n];
            Stack<int> stack = new Stack<int>(n);
            stack.Push(source);
            visited[source] = true;
            while(stack.Count != 0)
            {
                var vertex = stack.Pop();
                if (v2v.TryGetValue(vertex, out LinkedList<int> adjVers))
                {
                    foreach(var adjVer in adjVers)
                    {
                        if (adjVer == destination)
                            return true;

                        if (visited[adjVer] == false)
                        {
                            stack.Push(adjVer);
                            visited[adjVer] = true;
                        }
                    }
                }
            }

            return false;
        }
    }

    public class DSU
    {
        private int[] parents, ranks;

        public DSU(int n)
        {
            this.parents = new int[n];
            this.ranks = new int[n];
            for(int i = 0; i < n; ++i)
            {
                this.parents[i] = i;
                this.ranks[i] = 1;
            }
        }

        public int GetParent(int node)
        {
            int parent = this.parents[node];
            if (parent == node)
                return parent;

            // path compress
            return this.parents[node] = this.GetParent(parent);
        }

        public bool Union(int node1, int node2)
        {
            int parent1 = this.GetParent(node1);
            int parent2 = this.GetParent(node2);
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
                this.ranks[parent1] += 1;
            }

            return true;
        }
    }
}
