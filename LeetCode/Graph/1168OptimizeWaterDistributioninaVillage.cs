using ConsoleApp2.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Graph
{
    public class _1168OptimizeWaterDistributioninaVillage
    {
        public int MinCostToSupplyWater_Prim(int n, int[] wells, int[][] pipes)
        {      
            Dictionary<int, LinkedList<(int, int)>> v2v = new Dictionary<int, LinkedList<(int, int)>>(n * 2);
            foreach(var pipe in pipes)
            {
                int v1 = pipe[0] - 1;
                int v2 = pipe[1] - 1;
                if (!v2v.TryGetValue(v1, out LinkedList<(int, int)> list1))
                {
                    list1 = new LinkedList<(int, int)>();
                    v2v.Add(v1, list1);
                }
                list1.AddLast((v2, pipe[2]));

                if (!v2v.TryGetValue(v2, out LinkedList<(int, int)> list2))
                {
                    list2 = new LinkedList<(int, int)>();
                    v2v.Add(v2, list2);
                }
                list2.AddLast((v1, pipe[2]));
            }

            var heap = new PriorityQueue<(int, int), int>();
            for (int i = 0; i < n; ++i)
            {
                // virtual node n
                heap.Enqueue((i, wells[i]), wells[i]);
            }

            HashSet<int> visited = new HashSet<int>(n);
            int cost = 0;
            while (visited.Count < n)
            {
                var item = heap.Dequeue();
                if (visited.Add(item.Item1))
                {
                    cost += item.Item2;
                    if (v2v.TryGetValue(item.Item1, out LinkedList<(int, int)> adjs)) 
                    {
                        // O(N*logE), N is the # of vertices and E is # of edges
                        foreach(var adj in adjs)
                        {
                            if (!visited.Contains(adj.Item1))
                            {
                                heap.Enqueue((adj.Item1, adj.Item2), adj.Item2);
                            }
                        }
                    }
                }

                /* O(N*N) no matter how many edges are
                // update minDis for each node based on the nextNode
                for (int i = 0; i <= n; ++i)
                {
                    if (!visited.Contains(i) && dis.TryGetValue((nextNode, i), out int value) && value < minDis[i])
                    {
                        minDis[i] = value;
                    }
                }

                int minDisForSource = int.MaxValue;

                // select the node with min dis to the group of visited nodes
                for (int i = 0; i <= n; ++i)
                {
                    if (!visited.Contains(i) && minDisForSource > minDis[i])
                    {
                        minDisForSource = minDis[i];
                        nextNode = i;
                    }
                }

                cost += minDisForSource;
                */
            }

            return cost;
        }

        public int MinCostToSupplyWater_Kruskal(int n, int[] wells, int[][] pipes)
        {
            var queue = new PriorityQueue<(int, int, int), int>();
            foreach(var pipe in pipes)
            {
                queue.Enqueue((pipe[0] - 1, pipe[1] - 1, pipe[2]), pipe[2]);
            }

            for(int i = 0; i < n; ++i)
            {
                // n is the virtual node which connects to all other nodes with cost of wells
                queue.Enqueue((i, n, wells[i]), wells[i]);
            }

            int cost = 0, edges = 0;
            var dj = new DisjointUnion(n + 1);
            while (queue.Count != 0 && edges < n)
            {
                var item = queue.Dequeue();
                var h1 = item.Item1;
                var h2 = item.Item2;
                if (dj.Union(h1, h2))
                {
                    cost += item.Item3;
                    ++edges;
                }
            }

            return cost;
        }

        public int MinCostToSupplyWaterV1(int n, int[] wells, int[][] pipes)
        {
            var uf = new DisjointUnion(n * 2);
            var queue = new PriorityQueue<(int, int, int), int>();
            for(int i = 0; i < n; i++)
            {
                queue.Enqueue((i, i, wells[i]), wells[i]);
            }

            foreach(var pipe in pipes)
            {
                queue.Enqueue((pipe[0] - 1, pipe[1] - 1, pipe[2]), pipe[2]);
            }

            HashSet<int> hasWell = new HashSet<int>(n);
            int cost = 0;
            while (queue.Count > 0)
            {
                var sol = queue.Dequeue();
                var h1 = sol.Item1;
                var h2 = sol.Item2;
                if (h1 == h2)
                {
                    // try to build well for the group
                    int parent = uf.GetParent(h1);
                    if (!hasWell.Contains(parent))
                    {
                        hasWell.Add(parent);
                        cost += sol.Item3;
                    }
                }
                else
                {
                    int parent1 = uf.GetParent(h1);
                    int parent2 = uf.GetParent(h2);
                    if (!(hasWell.Contains(parent1) && hasWell.Contains(parent2)))
                    {
                        // build pipe
                        if (uf.Union(h1, h2))
                        {
                            if (hasWell.Contains(parent1) || hasWell.Contains(parent2))
                            {
                                hasWell.Add(uf.GetParent(h1));
                            }

                            cost += sol.Item3;
                        }
                    }
                }
            }

            return cost;
        }
    }

    public class DisjointUnion
    {
        private int[] parents, ranks;

        public DisjointUnion(int n)
        {
            this.parents = new int[n];
            this.ranks = new int[n];
            for (int i = 0; i < n; ++i)
            {
                this.parents[i] = i;
                this.ranks[i] = 1;
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
