using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _1584MinCosttoConnectAllPoints
    {
        private int GetRoot(int vertex, int[] roots)
        {
            var root = roots[vertex];
            if (root == vertex)
                return root;

            return roots[vertex] = this.GetRoot(root, roots);
        }

        private bool Union(int vertex1, int vertex2, int[] roots, int[] heights)
        {
            int root1 = this.GetRoot(vertex1, roots);
            int root2 = this.GetRoot(vertex2, roots);
            if (root1 == root2)
                return false;

            if (heights[root1] > heights[root2])
            {
                roots[root2] = root1;
            }
            else if (heights[root1] < heights[root2])
            {
                roots[root1] = root2;
            }
            else
            {
                roots[root2] = root1;
                heights[root1] += 1;
            }

            return true;
        }

        public int MinCostConnectPoints_K(int[][] points)
        {
            var lists = new LinkedList<Edge>();
            for(int i = 0; i < points.Length; ++i)
            {
                int[] point1 = points[i];
                for(int j = i + 1; j < points.Length; ++j)
                {
                    int[] point2 = points[j];
                    int distance = Math.Abs(point1[0] - point2[0]) + Math.Abs(point1[1] - point2[1]);
                    lists.AddLast(new Edge(i, j, distance));
                }
            }

            MinHeap<Edge> edges = new MinHeap<Edge>(lists.Count);
            foreach(var edge in lists)
            {
                edges.Push(edge);
            }

            int[] roots = new int[points.Length];
            int[] heights = new int[points.Length];
            for(int i = 0; i < points.Length; ++i)
            {
                roots[i] = i;
                heights[i] = 1;
            }

            int totalCost = 0, totalEdges = 0;
            while(edges.Size() > 0)
            {
                var edge = edges.Pop();
                if (this.Union(edge.vertex1, edge.vertex2, roots, heights))
                {
                    totalCost += edge.distance;
                    if (++totalEdges == points.Length - 1)
                        break;
                }
            }

            return totalCost;
        }

        public int MinCostConnectPoints_Prim(int[][] points)
        {
            HashSet<int> visitedVertice = new HashSet<int>(points.Length);
            HashSet<int> nonVistedVertice = new HashSet<int>(points.Length);
            visitedVertice.Add(0);
            for(int i = 1; i < points.Length; ++i)
            {
                nonVistedVertice.Add(i);
            }

            var lists = new LinkedList<Edge>();
            for (int i = 0; i < points.Length; ++i)
            {
                int[] point1 = points[i];
                for (int j = i + 1; j < points.Length; ++j)
                {
                    int[] point2 = points[j];
                    int distance = Math.Abs(point1[0] - point2[0]) + Math.Abs(point1[1] - point2[1]);
                    lists.AddLast(new Edge(i, j, distance));
                }
            }

            MinHeap<Edge> edges = new MinHeap<Edge>(lists.Count);
            foreach (var edge in lists)
            {
                edges.Push(edge);
            }

            int cost = 0;
            LinkedList<Edge> pending = new LinkedList<Edge>();
            while (nonVistedVertice.Count > 0)
            {
                var nextEdge = edges.Pop();
                bool visited1 = visitedVertice.Contains(nextEdge.vertex1);
                bool visited2 = visitedVertice.Contains(nextEdge.vertex2);
                if (visited1 && visited2)
                    continue;
                if (!visited1 && !visited2)
                {
                    pending.AddLast(nextEdge);
                    continue;
                }
                if (pending.Count > 0)
                {
                    foreach (var p in pending)
                    {
                        edges.Push(p);
                    }
                    pending.Clear();
                }

                int[] v1 = points[nextEdge.vertex1], v2 = points[nextEdge.vertex2];
                int dis = Math.Abs(v1[0] - v2[0]) + Math.Abs(v1[1] - v2[1]);
                if (visited1)
                {
                    nonVistedVertice.Remove(nextEdge.vertex2);
                    visitedVertice.Add(nextEdge.vertex2);
                }
                else
                {
                    nonVistedVertice.Remove(nextEdge.vertex1);
                    visitedVertice.Add(nextEdge.vertex1);
                }

                cost += dis;
            }

            return cost;
        }

        public int MinCostConnectPoints_Prim_V2(int[][] points)
        {
            bool[] visited = new bool[points.Length];
            // the min distance from any visited vertex to a given non-visited vertex
            int[] minDisFromVisitedToNonVisited = new int[points.Length];

            minDisFromVisitedToNonVisited[0] = 0;
            for (int i = 1; i < minDisFromVisitedToNonVisited.Length; ++i)
            {
                minDisFromVisitedToNonVisited[i] = int.MaxValue;
            }

            int verticesVisited = 0;
            int cost = 0;
            while (verticesVisited < points.Length)
            {
                // Find the next shortes edge from visited vertices to non-visited vertices
                int minDis = int.MaxValue;
                int nextVer = -1;
                for(int i = 0; i < minDisFromVisitedToNonVisited.Length; ++i)
                {
                    if (!visited[i] && minDis > minDisFromVisitedToNonVisited[i])
                    {
                        minDis = minDisFromVisitedToNonVisited[i];
                        nextVer = i;
                    }
                }

                cost += minDis;
                visited[nextVer] = true;
                ++verticesVisited;

                // update minDisFromVisitedToNonVisited
                for(int i = 0; i < minDisFromVisitedToNonVisited.Length; ++i)
                {
                    if (!visited[i])
                    {
                        int dis = Math.Abs(points[nextVer][0] - points[i][0]) + Math.Abs(points[nextVer][1] - points[i][1]);
                        if (dis < minDisFromVisitedToNonVisited[i])
                            minDisFromVisitedToNonVisited[i] = dis;
                    }
                }
            }

            return cost;
        }

        public int MinCostConnectPoints_Prim_V3(int[][] points)
        {
            //https://leetcode.com/problems/min-cost-to-connect-all-points/solutions/3060225/more-optimized-prim-algo-with-detailed-explanations/?orderBy=most_votes
            bool[] visited = new bool[points.Length];
            // the min distance from any visited vertex to a given non-visited vertex
            int[] minDisFromVisitedToNonVisited = new int[points.Length];

            minDisFromVisitedToNonVisited[0] = 0;
            for (int i = 1; i < minDisFromVisitedToNonVisited.Length; ++i)
            {
                minDisFromVisitedToNonVisited[i] = int.MaxValue;
            }

            int verticesVisited = 0;
            int cost = 0;
            int curVer = 0;
            int minDis = minDisFromVisitedToNonVisited[curVer];
            while (verticesVisited < points.Length)
            {
                // Find the next shortes edge from visited vertices to non-visited vertices
                cost += minDis;
                visited[curVer] = true;
                ++verticesVisited;

                // update minDisFromVisitedToNonVisited and find next vertex to visit
                minDis = int.MaxValue;
                int nextVer = -1;
                for (int i = 0; i < minDisFromVisitedToNonVisited.Length; ++i)
                {
                    if (!visited[i])
                    {
                        int dis = Math.Abs(points[curVer][0] - points[i][0]) + Math.Abs(points[curVer][1] - points[i][1]);
                        if (dis < minDisFromVisitedToNonVisited[i])
                        {
                            minDisFromVisitedToNonVisited[i] = dis;
                        }

                        if (minDisFromVisitedToNonVisited[i] < minDis)
                        {
                            minDis = minDisFromVisitedToNonVisited[i];
                            nextVer = i;
                        }
                    }
                }

                curVer = nextVer;
            }

            return cost;
        }
    }

    public class Edge : IComparable<Edge>
    {
        public int vertex1, vertex2, distance;

        public Edge(int v1, int v2, int dis)
        {
            this.vertex1 = v1;
            this.vertex2 = v2;
            this.distance = dis;
        }

        public int CompareTo(Edge other)
        {
            return this.distance - other.distance;
        }
    }

    public class MinHeap<T> where T : class, IComparable<T>
    {
        private T[] nums;

        private int last;

        public MinHeap(int size)
        {
            this.nums = new T[size + 1];
            this.last = 0;
        }

        public T Peek()
        {
            return this.nums[0];
        }

        public int Size()
        {
            return this.last;
        }

        public T Push(T val)
        {
            this.nums[this.last++] = val;

            // bubble up
            int lastChildIndex = this.last - 1;
            if (lastChildIndex > 0)
            {
                int parent = (lastChildIndex - 1) / 2;
                while (parent >= 0 && parent < lastChildIndex)
                {
                    if (this.nums[parent].CompareTo(this.nums[lastChildIndex]) > 0)
                    {
                        // swap
                        T parentValue = this.nums[parent];
                        this.nums[parent] = this.nums[lastChildIndex];
                        this.nums[lastChildIndex] = parentValue;
                    }
                    else
                    {
                        break;
                    }

                    // parent becomes new child
                    lastChildIndex = parent;
                    parent = (lastChildIndex - 1) / 2;
                }
            }

            if (this.Size() == this.nums.Length)
            {
                this.Pop();
            }

            return this.Peek();
        }

        public T Pop()
        {
            if (this.Size() <= 0)
            {
                return null;
            }

            var currentParentValue = this.Peek();

            T lastNumber = this.nums[--this.last];
            this.nums[0] = lastNumber;

            // sink down
            int parentIndex = 0;
            int leftChildIndex = parentIndex * 2 + 1;
            int rightChildIndex = parentIndex * 2 + 2;
            while (leftChildIndex <= this.last || rightChildIndex <= this.last)
            {
                int smallestIndex = parentIndex;
                if (leftChildIndex <= this.last && this.nums[parentIndex].CompareTo(this.nums[leftChildIndex]) > 0)
                {
                    smallestIndex = leftChildIndex;
                }
                if (rightChildIndex <= this.last && this.nums[smallestIndex].CompareTo(this.nums[rightChildIndex]) > 0)
                {
                    smallestIndex = rightChildIndex;
                }

                if (smallestIndex != parentIndex)
                {
                    // sawp parent and the biggest node(either left or right)
                    T parentValue = this.nums[parentIndex];
                    this.nums[parentIndex] = this.nums[smallestIndex];
                    this.nums[smallestIndex] = parentValue;

                    parentIndex = smallestIndex;
                    leftChildIndex = parentIndex * 2 + 1;
                    rightChildIndex = parentIndex * 2 + 2;
                }
                else
                {
                    break;
                }
            }

            return currentParentValue;
        }
    }
}
