using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _994RottingOranges
    {
        public int OrangesRotting(int[][] grid)
        {
            return BFS(grid);
        }

        private static int[,] directions = new int[4, 2] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

        private int BFS(int[][] grid)
        {
            int fresh = 0;
            bool[,] rotten = new bool[grid.Length, grid[0].Length];
            Queue<(int, int)> queue = new Queue<(int, int)>();
            for (int i = 0; i < grid.Length; ++i)
            {
                for (int j = 0; j < grid[i].Length; ++j)
                {
                    if (grid[i][j] == 2)
                    {
                        queue.Enqueue((i, j));
                        rotten[i, j] = true;
                    }
                    else if (grid[i][j] == 1)
                    {
                        ++fresh;
                    }
                }
            }

            if (fresh != 0)
            {
                int minutes = 0;
                while (queue.Count > 0 && fresh != 0)
                {
                    ++minutes;
                    int count = queue.Count;
                    for (int c = 0; c < count && fresh != 0; ++c)
                    {
                        var head = queue.Dequeue();
                        int i = head.Item1;
                        int j = head.Item2;
                        for (int d = 0; d < directions.GetLength(0); ++d)
                        {
                            int nextI = i + directions[d, 0];
                            int nextJ = j + directions[d, 1];
                            if (nextI >= 0 && nextI < grid.Length &&
                                nextJ >= 0 && nextJ < grid[i].Length &&
                                grid[nextI][nextJ] == 1 &&
                                rotten[nextI, nextJ] == false)
                            {
                                --fresh;
                                rotten[nextI, nextJ] = true;
                                queue.Enqueue((nextI, nextJ));
                            }
                        }
                    }
                }

                return fresh == 0 ? minutes : -1;
            }

            return 0;
        }

        private int BFSv1(int[][] grid)
        {
            int minute = 0;
            Queue<(int, int)> queue = new Queue<(int, int)>();
            for (int i = 0; i < grid.Length; ++i)
            {
                for (int j = 0; j < grid[i].Length; ++j)
                {
                    if (grid[i][j] == 2)
                    {
                        queue.Enqueue((i, j));
                    }
                }
            }

            bool changed = false;
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                int i = head.Item1;
                int j = head.Item2;
                for (int d = 0; d < directions.GetLength(0); ++d)
                {
                    int nextI = i + directions[d, 0];
                    int nextJ = j + directions[d, 1];
                    if (nextI >= 0 && nextI < grid.Length &&
                        nextJ >= 0 && nextJ < grid[i].Length &&
                        grid[nextI][nextJ] == 1)
                    {
                        grid[nextI][nextJ] = grid[i][j] + 1;
                        queue.Enqueue((nextI, nextJ));
                        changed = true;
                    }
                }
            }

            for (int i = 0; i < grid.Length; ++i)
            {
                for (int j = 0; j < grid[i].Length; ++j)
                {
                    int val = grid[i][j];
                    minute = Math.Max(val, minute);
                    if (val == 1)
                    {
                        return -1;
                    }
                }
            }

            return changed ? minute - 2 : 0;
        }
    }
}
