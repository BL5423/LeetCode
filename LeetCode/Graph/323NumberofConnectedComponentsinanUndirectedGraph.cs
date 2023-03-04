using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _323NumberofConnectedComponentsinanUndirectedGraph
    {
        public int CountComponents(int n, int[][] edges)
        {
            int connected = 0;
            if (n > 1)
            {
                var dju = new DisjointUnion(n);
                foreach(var edge in edges)
                {
                    if (dju.Union(edge[0], edge[1]))
                    {
                        ++connected;
                    }
                }
            }

            return n - connected;
        }
    }
}
