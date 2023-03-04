using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _1059AllPathsfromSourceLeadtoDestination
    {
        public bool LeadsToDestination(int n, int[][] edges, int source, int destination)
        {
            var v2e = new LinkedList<int>[n];
            foreach(var edge in edges)
            {
                var s = edge[0];
                var t = edge[1];
                if (v2e[s] == null)
                {
                    v2e[s] = new LinkedList<int>();
                }

                v2e[s].AddLast(t);
            }

            // there are outbounding edges from destionation
            if (v2e[destination] != null)
                return false;

            bool[] seen = new bool[n];
            seen[source] = true;
            //return this.DFS_Recursively(source, destination, v2e, seen);
            return this.DFS_Iteratively(source, destination, v2e, seen);
        }

        private bool DFS_Iteratively(int source, int destination, LinkedList<int>[] v2e, bool[] seen)
        {
            Stack<int> stack = new Stack<int>(seen.Length);
            stack.Push(source);
            seen[source] = true;

            while (stack.Count != 0)
            {
                var node = stack.Peek();
                if (node != destination)
                {
                    LinkedList<int> edges = v2e[node];
                    if (edges == null)
                    {
                        // stuck on a node without any outbounding edges
                        return false;
                    }
                    else if (edges.Count != 0)
                    {
                        var next = edges.First.Value;
                        edges.RemoveFirst();

                        // cycle is found
                        if (seen[next] == true)
                            return false;

                        seen[next] = true;
                        stack.Push(next);
                    }
                    else
                    {
                        seen[node] = false;
                        stack.Pop();
                    }
                }
                else
                {
                    // reached destination
                    seen[node] = false;
                    stack.Pop();
                }
            }

            return true;
        }

        private bool DFS_Recursively(int cur, int destination, LinkedList<int>[] v2e, bool[] seen)
        {
            if (cur == destination)
            {
                // found a path
                return true;
            }
            else
            {
                LinkedList<int> edges = v2e[cur];
                if (edges == null)
                {
                    // stuck on a node without any outbounding edges
                    return false;
                }

                while (edges.Count != 0)
                {
                    var next = edges.First.Value;
                    edges.RemoveFirst();

                    if (seen[next])
                    {
                        // cycle is found
                        return false;
                    }

                    seen[next] = true;
                    
                    if (!this.DFS_Recursively(next, destination, v2e, seen))
                        return false;

                    seen[next] = false;
                }
            }

            return true;
        }
    }
}
