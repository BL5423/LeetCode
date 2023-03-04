using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Graph
{
    public class _1631PathWithMinimumEffort
    {
        private readonly int[,] directions = new int[,] { { 0, 1 }, { 1, 0 }, { -1, 0 }, { 0, -1 } };

        public int MinimumEffortPath_BellMan_Ford(int[][] heights)
        {
            int row = heights.Length;
            int col = heights[0].Length;
            int[,] maxHeightDiffFromSource = new int[row, col];
            for(int r = 0; r < row; ++r)
            {
                for(int c = 0; c < col; ++c)
                {
                    maxHeightDiffFromSource[r, c] = int.MaxValue;
                }
            }
            maxHeightDiffFromSource[0, 0] = 0;

            int posRow = 0, posCol = 0;
            Queue<(int, int)> queue = new Queue<(int, int)>(row * col);
            bool[,] inQueue = new bool[row, col];
            queue.Enqueue((posRow, posCol));
            inQueue[posRow, posCol] = true;
            while (queue.Count > 0)
            {
                var pos = queue.Dequeue();
                int curRow = pos.Item1;
                int curCol = pos.Item2;
                inQueue[curRow, curCol] = false;
                if (curRow == row - 1 && curCol == col - 1)
                    continue;

                for(int d = 0; d < directions.GetLength(0); ++d)
                {
                    int nextRow = curRow + directions[d, 0];
                    int nextCol = curCol + directions[d, 1];
                    if (nextRow < 0 || nextRow >= row || nextCol < 0 || nextCol >= col)
                        continue;

                    int heightDiff = Math.Abs(heights[curRow][curCol] - heights[nextRow][nextCol]);
                    int curMaxHeightDiff = maxHeightDiffFromSource[curRow, curCol];
                    int nextPotentialHeightDiff = Math.Max(heightDiff, curMaxHeightDiff);
                    if (maxHeightDiffFromSource[nextRow, nextCol] > nextPotentialHeightDiff)
                    {
                        maxHeightDiffFromSource[nextRow, nextCol] = nextPotentialHeightDiff;

                        if (!inQueue[nextRow, nextCol])
                        {
                            queue.Enqueue((nextRow, nextCol));
                            inQueue[nextRow, nextCol] = true;
                        }
                    }
                }
            }

            return maxHeightDiffFromSource[row - 1, col - 1];
        }

        public int MinimumEffortPath_Dijkstra(int[][] heights)
        {
            int row = heights.Length;
            int col = heights[0].Length;
            int[,] maxHeightDiff = new int[row, col];
            for(int r = 0; r < row; ++r)
            {
                for(int c = 0; c < col; ++c)
                {
                    maxHeightDiff[r, c] = int.MaxValue;
                }
            }
            maxHeightDiff[0, 0] = 0;

            MinHeap<Next> minHeap = new MinHeap<Next>(row * col);
            minHeap.Push(new Next(0, 0, 0));
            while (minHeap.Size() != 0)
            {
                var next = minHeap.Pop();
                int curRow = next.row;
                int curCol = next.col;
                if (curRow == row - 1 && curCol == col - 1)
                    break;

                for(int d = 0; d < directions.GetLength(0); ++d)
                {
                    int nextRow = curRow + directions[d, 0];
                    int nextCol = curCol + directions[d, 1];
                    if (nextRow < 0 || nextRow >= row || nextCol < 0 || nextCol >= col)
                        continue;

                    int nextDiff = Math.Abs(heights[curRow][curCol] - heights[nextRow][nextCol]);
                    int curMaxDiff = maxHeightDiff[curRow, curCol];
                    int potentialNextMaxDiff = Math.Max(nextDiff, curMaxDiff);
                    if (maxHeightDiff[nextRow, nextCol] > potentialNextMaxDiff)
                    {
                        maxHeightDiff[nextRow, nextCol] = potentialNextMaxDiff;
                        minHeap.Push(new Next(nextRow, nextCol, potentialNextMaxDiff));
                    }
                }
            }

            return maxHeightDiff[row - 1, col - 1];
        }

        public int MinimumEffortPath_DisjointSet(int[][] heights)
        {
            int row = heights.Length;
            int col = heights[0].Length;
            int[] parents = new int[row * col];
            int[] ranks = new int[row * col];
            for(int i = 0; i < row * col; ++i)
            {
                parents[i] = i;
                ranks[i] = 1;
            }

            var edges = this.BuildEdges(heights).OrderBy(e => e.diff);
            foreach(var edge in edges)
            {
                int from = edge.from;
                int to = edge.to;
                if (this.Union(from, to, parents, ranks))
                {
                    if (GetParent(0, parents) == GetParent(row * col - 1, parents))
                        return edge.diff;
                }
            }

            return 0;
        }

        private LinkedList<Edge> BuildEdges(int[][] heights)
        {
            int row = heights.Length;
            int col = heights[0].Length;
            var edges = new LinkedList<Edge>();
            for(int r = 0; r < row; ++r)
            {
                for(int c = 0; c < col; ++c)
                {
                    int from = r * col + c;
                    if (r > 0)
                    {
                        int to = (r - 1) * col + c;
                        var edge = new Edge(from, to, Math.Abs(heights[r][c] - heights[r - 1][c]));
                        edges.AddLast(edge);
                    }
                    if (c > 0)
                    {
                        int to = r * col + c - 1;
                        var edge = new Edge(from, to, Math.Abs(heights[r][c] - heights[r][c - 1]));
                        edges.AddLast(edge);
                    }
                }
            }

            return edges;
        }

        private bool Union(int node1, int node2, int[] parents, int[] ranks)
        {
            int parent1 = this.GetParent(node1, parents);
            int parent2 = this.GetParent(node2, parents);
            if (parent1 == parent2)
                return false;

            if (ranks[parent1] > ranks[parent2])
            {
                parents[parent2] = parent1;
                ranks[parent1] += ranks[parent2];
            }
            else if (ranks[parent1] < ranks[parent2])
            {
                parents[parent1] = parent2;
                ranks[parent2] += ranks[parent1];
            }
            else
            {
                parents[parent2] = parent1;
                ranks[parent1] += 1;
            }

            return true;
        }

        private int GetParent(int node, int[] parents)
        {
            if (parents[node] != node) 
                return (parents[node] = GetParent(parents[node], parents));

            return parents[node];
        }
    }

    public class Next : IComparable<Next>
    {
        public int row, col, diff;

        public Next(int r, int c, int diff)
        {
            this.row = r;
            this.col = c;
            this.diff = diff;
        }

        public int CompareTo(Next other)
        {
            return this.diff - other.diff;
        }
    }

    public class Edge
    {
        public int from, to, diff;

        public Edge(int f, int t, int d)
        {
            this.from = f;
            this.to = t;
            this.diff = d;
        }
    }
}
