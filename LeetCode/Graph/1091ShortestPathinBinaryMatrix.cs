using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _1091ShortestPathinBinaryMatrix
    {
        private int[,] directions = new int[,] { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 } };

        public int ShortestPathBinaryMatrix_Dijkstra(int[][] grid)
        {
            // https://leetcode.com/problems/shortest-path-in-binary-matrix/solutions/934747/shortest-path-in-a-binary-matrix/
            if (grid[0][0] != 1)
            {
                int rows = grid.Length, cols = grid[0].Length;
                int[,] dis = new int[rows, cols];
                PriorityQueue<Candidate, int> heap = new(); // min heap
                heap.Enqueue(new Candidate(0, 0, 1, this.Estimate(0, 0, rows, cols)), 1);
                dis[0, 0] = 1;

                while (heap.Count != 0)
                {
                    var candidate = heap.Dequeue();

                    if (candidate.row == rows - 1 && candidate.col == cols - 1)
                        return candidate.disSoFar;

                    // not the shortest path for candidate anymore
                    if (candidate.disSoFar != dis[candidate.row, candidate.col])
                        continue;

                    for (int d = 0; d < directions.GetLength(0); ++d)
                    {
                        int nextRow = candidate.row + directions[d, 0];
                        int nextCol = candidate.col + directions[d, 1];
                        if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols || grid[nextRow][nextCol] != 0)
                            continue;

                        int nextDis = candidate.disSoFar + 1;
                        if (dis[nextRow, nextCol] == 0 || dis[nextRow, nextCol] > nextDis)
                        {
                            dis[nextRow, nextCol] = nextDis; // a shorter path
                            int nextEstimate = nextDis + this.Estimate(nextRow, nextCol, rows, cols);
                            heap.Enqueue(new Candidate(nextRow, nextCol, nextDis, nextEstimate), nextDis);
                        }
                    }
                }
            }

            return -1;
        }

        public int ShortestPathBinaryMatrix_AStar(int[][] grid)
        {
            // https://leetcode.com/problems/shortest-path-in-binary-matrix/solutions/934747/shortest-path-in-a-binary-matrix/
            if (grid[0][0] != 1)
            {
                int rows = grid.Length, cols = grid[0].Length;
                PriorityQueue<Candidate, int> heap = new (); // min heap
                int[,] dis = new int[rows, cols];
                heap.Enqueue(new Candidate(0, 0, 1, this.Estimate(0, 0, rows, cols)), 1 + this.Estimate(0, 0, rows, cols));
                dis[0, 0] = 1;

                while (heap.Count != 0)
                {
                    var candidate = heap.Dequeue();

                    if (candidate.row == rows - 1 && candidate.col == cols - 1)
                        return candidate.disSoFar;

                    // not the shortest path for candidate anymore
                    if (dis[candidate.row, candidate.col] > candidate.disSoFar)
                        continue;

                    for (int d = 0; d < directions.GetLength(0); ++d)
                    {
                        int nextRow = candidate.row + directions[d, 0];
                        int nextCol = candidate.col + directions[d, 1];
                        if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols || grid[nextRow][nextCol] != 0)
                            continue;

                        int nextDis = candidate.disSoFar + 1;
                        if (dis[nextRow, nextCol] > nextDis || dis[nextRow, nextCol] == 0)
                        {
                            dis[nextRow, nextCol] = nextDis;
                            int nextEstimate = nextDis + this.Estimate(nextRow, nextCol, rows, cols);
                            heap.Enqueue(new Candidate(nextRow, nextCol, nextDis, nextEstimate), nextEstimate);
                        }
                    }
                }
            }

            return -1;
        }

        public int ShortestPathBinaryMatrix_AStarV1(int[][] grid)
        {
            // https://leetcode.com/problems/shortest-path-in-binary-matrix/solutions/934747/shortest-path-in-a-binary-matrix/
            if (grid[0][0] != 1)
            {
                int rows = grid.Length, cols = grid[0].Length;
                MinHeap<Candidate> heap = new MinHeap<Candidate>(rows * cols * rows * cols);
                bool[,] visited = new bool[rows, cols];
                heap.Push(new Candidate(0, 0, 1, this.Estimate(0, 0, rows, cols)));

                while (heap.Size() != 0)
                {
                    var candidate = heap.Pop();

                    if (candidate.row == rows - 1 && candidate.col == cols - 1)
                        return candidate.disSoFar;

                    if (visited[candidate.row, candidate.col])
                        continue;

                    visited[candidate.row, candidate.col] = true;

                    for (int d = 0; d < directions.GetLength(0); ++d)
                    {
                        int nextRow = candidate.row + directions[d, 0];
                        int nextCol = candidate.col + directions[d, 1];
                        if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols || grid[nextRow][nextCol] != 0)
                            continue;

                        int nextDis = candidate.disSoFar + 1;
                        int nextEstimate = nextDis + this.Estimate(nextRow, nextCol, rows, cols);
                        heap.Push(new Candidate(nextRow, nextCol, nextDis, nextEstimate));
                    }
                }
            }

            return -1;
        }

        private int Estimate(int row, int col, int rows, int cols)
        {
            return Math.Max(rows - row, cols - col);
        }

        public int ShortestPathBinaryMatrix_BFS(int[][] grid)
        {
            if (grid[0][0] == 0)
            {
                int rows = grid.Length, cols = grid[0].Length;
                bool[,] visited = new bool[rows, cols];
                Queue<(int, int, int)> queue = new Queue<(int, int, int)>(rows * cols);
                queue.Enqueue((0, 0, 1));
                visited[0, 0] = true;

                while (queue.Count != 0)
                {
                    var item = queue.Dequeue();
                    var row = item.Item1;
                    var col = item.Item2;
                    var length = item.Item3;

                    if (row == rows - 1 && col == cols - 1)
                    {
                        return length;
                    }
                    else
                    {
                        for (int d = 0; d < directions.GetLength(0); ++d)
                        {
                            int nextRow = row + directions[d, 0];
                            int nextCol = col + directions[d, 1];
                            if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols || visited[nextRow, nextCol] || grid[nextRow][nextCol] != 0)
                                continue;

                            visited[nextRow, nextCol] = true;
                            queue.Enqueue((nextRow, nextCol, length + 1));
                        }
                    }
                }
            }

            return -1;
        }
    }

    public class Candidate : IComparable<Candidate>
    {
        public int row, col, disSoFar, estimateDis;

        public Candidate(int r, int c, int dis, int estimate)
        {
            this.row = r;
            this.col = c;
            this.disSoFar = dis;
            this.estimateDis = estimate;
        }

        public int CompareTo(Candidate other)
        {
            return this.estimateDis - other.estimateDis;
        }
    }
}
