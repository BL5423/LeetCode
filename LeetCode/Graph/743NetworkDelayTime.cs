using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _743NetworkDelayTime
    {
        public int NetworkDelayTime_SPFA(int[][] times, int n, int k)
        {
            Dictionary<int, LinkedList<(int, int)>> v2e = new Dictionary<int, LinkedList<(int, int)>>(times.Length);
            foreach(var time in times)
            {
                if (!v2e.TryGetValue(time[0], out LinkedList<(int, int)> edges))
                {
                    edges = new LinkedList<(int, int)>();
                    v2e.Add(time[0], edges);
                }
                edges.AddLast((time[1], time[2]));
            }

            int[] distanceFromK = new int[n + 1];
            for (int i = 0; i <= n; ++i)
                distanceFromK[i] = int.MaxValue;
            distanceFromK[k] = 0;

            bool[] inQueue = new bool[n + 1];
            Queue<int> queue = new Queue<int>(n);
            queue.Enqueue(k);
            inQueue[k] = true;
            while (queue.Count != 0)
            {
                var vertex = queue.Dequeue();
                inQueue[vertex] = false;
                if (v2e.TryGetValue(vertex, out LinkedList<(int, int)> edges))
                {
                    foreach(var edge in edges)
                    {
                        int nextVer = edge.Item1;
                        int dis = edge.Item2;
                        int newCost = distanceFromK[vertex] + dis;
                        if (distanceFromK[nextVer] > newCost)
                        {
                            distanceFromK[nextVer] = newCost;
                            if (!inQueue[nextVer])
                            {
                                queue.Enqueue(nextVer);
                                inQueue[nextVer] = true;
                            }
                        }
                    }
                }
            }

            int maxDis = int.MinValue;
            for(int i = 1; i <= n; ++i)
            {
                if (maxDis < distanceFromK[i])
                    maxDis = distanceFromK[i];
            }
            return maxDis != int.MaxValue ? maxDis : -1;
        }

        public int NetworkDelayTime_Heap(int[][] times, int n, int k)
        {
            var v2e = new Dictionary<int, LinkedList<Edge>>(n * 2);
            foreach (var time in times)
            {
                int v1 = time[0];
                int v2 = time[1];
                int dis = time[2];
                var edge = new Edge(v1, v2, dis);
                if (!v2e.TryGetValue(v1, out LinkedList<Edge> edges))
                {
                    edges = new LinkedList<Edge>();
                    v2e.Add(v1, edges);
                }
                edges.AddLast(edge);
            }

            int[] distanceFromK = new int[n + 1];
            for(int i = 1; i <= n; ++i)
            {
                distanceFromK[i] = int.MaxValue;
            }
            distanceFromK[k] = 0;
            bool[] visited = new bool[n + 1];
            MinHeap<Distance> heap = new MinHeap<Distance>(n * n);
            heap.Push(new Distance(k, 0));
            while (heap.Size() > 0)
            {
                var node = heap.Pop();
                int vertex = node.vertex;
                var dis = node.dis;
                if (visited[vertex])
                    continue;
                visited[vertex] = true;

                // if there is a better option to reach vertex
                if (distanceFromK[vertex] < dis)
                    continue;
                
                distanceFromK[vertex] = dis;
                if (v2e.TryGetValue(vertex, out LinkedList<Edge> edges))
                {
                    foreach(var edge in edges)
                    {
                        int toVertex = edge.toVertex;
                        int toDis = edge.dis;
                        if (!visited[toVertex] && distanceFromK[toVertex] > dis + toDis)
                        {
                            heap.Push(new Distance(toVertex, dis + toDis));
                        }
                    }
                }
            }

            int maxDis = int.MinValue;
            for(int i = 1; i <= n; ++i)
            {
                if (maxDis < distanceFromK[i])
                    maxDis = distanceFromK[i];
            }
            return maxDis != int.MaxValue ? maxDis : -1;
        }

        public int NetworkDelayTime(int[][] times, int n, int k)
        {
            var v2e = new Dictionary<int, LinkedList<Edge>>(n * 2);
            foreach(var time in times)
            {
                int v1 = time[0];
                int v2 = time[1];
                int dis = time[2];
                var edge = new Edge(v1, v2, dis);
                if (!v2e.TryGetValue(v1, out LinkedList<Edge> edges))
                {
                    edges = new LinkedList<Edge>();
                    v2e.Add(v1, edges);
                }
                edges.AddLast(edge);
            }

            var processedFromVerties = new HashSet<int>(n);
            int[] distanceFromK = new int[n + 1];
            for (int i = 1; i <= n; ++i)
                distanceFromK[i] = int.MaxValue;
            distanceFromK[k] = 0;
            int nextVertex = k;

            while (processedFromVerties.Count != n)
            {
                processedFromVerties.Add(nextVertex);
                int disFromK = distanceFromK[nextVertex];
                if (v2e.TryGetValue(nextVertex, out LinkedList<Edge> edges))
                {
                    foreach (var edge in edges)
                    {
                        if (!processedFromVerties.Contains(edge.toVertex))
                        {
                            // The difference between Dijkstra and MST(minimum-spanning-tree) is that Dijkstra uses accumulative dis from source to other vertices
                            // while MST uses the dis between the newly added vertex to the non-visited vertex
                            int dis = disFromK + edge.dis;
                            if (distanceFromK[edge.toVertex] > dis)
                            {
                                distanceFromK[edge.toVertex] = dis;
                            }
                        }
                    }
                }

                int minDis = int.MaxValue;
                for (int i = 1; i <= n; ++i)
                {
                    if (distanceFromK[i] < minDis && !processedFromVerties.Contains(i))
                    {
                        minDis = distanceFromK[i];
                        nextVertex = i;
                    }
                }

                if (minDis == int.MaxValue)
                    break;
            }

            return processedFromVerties.Count == n ? distanceFromK.Max() : -1;
        }
    }

    public class Distance : IComparable<Distance>
    {
        public int vertex, dis;

        public Distance(int vertex, int dis)
        {
            this.vertex = vertex;
            this.dis = dis;
        }
        public int CompareTo(Distance other)
        {
            return this.dis - other.dis;
        }
    }

    public class Edge : IComparable<Edge>
    {
        public int fromVertex, toVertex, dis;

        public Edge(int v1, int v2, int dis)
        {
            this.fromVertex = v1;
            this.toVertex = v2;
            this.dis = dis;
        }

        public int CompareTo(Edge other)
        {
            return this.dis - other.dis;
        }
    }
}
