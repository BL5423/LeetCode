using ConsoleApp2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _542_01Matrix
    {
        public int[][] UpdateMatrix_DP(int[][] mat)
        {
            int max = mat.Length + mat[0].Length;
            int[][] res = new int[mat.Length][];

            // from top to bottom and left to right
            for(int row = 0; row < mat.Length; ++row)
            {
                res[row] = new int[mat[row].Length];
                for(int col = 0; col < mat[row].Length; ++col)
                {
                    res[row][col] = max;

                    if (mat[row][col] == 0)
                        res[row][col] = 0;
                    else
                    {
                        // only consider the results(above and left) that have been computed and potentially better
                        if (row > 0)
                            res[row][col] = Math.Min(res[row][col], res[row - 1][col] + 1);
                        if (col > 0)
                            res[row][col] = Math.Min(res[row][col], res[row][col - 1] + 1);
                    }
                }
            }

            // from bottom to top and right to left
            for(int row = mat.Length - 1; row >= 0; --row)
            {
                for(int col = mat[row].Length - 1; col >= 0; --col)
                {
                    if (res[row][col] != 0)
                    {
                        // only consider the results(below and right) that have been computed and potentially better
                        if (row < mat.Length - 1)
                            res[row][col] = Math.Min(res[row][col], res[row + 1][col] + 1);
                        if (col < mat[row].Length - 1)
                            res[row][col] = Math.Min(res[row][col], res[row][col + 1] + 1);
                    }
                }
            }

            return res;
        }

        private static int[,] directions = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

        public int[][] UpdateMatrix_BFS(int[][] mat)
        {
            bool[,] visited = new bool[mat.Length, mat[0].Length];
            int[][] res = new int[mat.Length][];
            Queue<(int, int)> queue = new Queue<(int, int)>();
            for(int i = 0; i < mat.Length; ++i)
            {
                res[i] = new int[mat[i].Length];
                for(int j = 0; j < mat[i].Length; ++j)
                {
                    if (mat[i][j] == 0)
                    {
                        queue.Enqueue((i, j));
                        visited[i, j] = true;
                    }
                }
            }

            int distance = 0;
            while (queue.Count > 0)
            {
                ++distance;
                int count = queue.Count;
                for (int c = 0; c < count; ++c)
                {
                    var pos = queue.Dequeue();
                    int row = pos.Item1;
                    int col = pos.Item2;

                    for (int d = 0; d < directions.GetLength(0); ++d)
                    {
                        int nextRow = row + directions[d, 0];
                        int nextCol = col + directions[d, 1];
                        if (nextRow < 0 || nextRow >= res.Length || nextCol < 0 || nextCol >= res[nextRow].Length || visited[nextRow, nextCol])
                            continue;

                        res[nextRow][nextCol] = distance;
                        queue.Enqueue((nextRow, nextCol));
                        visited[nextRow, nextCol] = true;
                    }
                }
            }

            return res;
        }
    }
}
