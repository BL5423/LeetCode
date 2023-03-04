using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _329LongestIncreasingPathinaMatrix
    {
        public int LongestIncreasingPath(int[][] matrix)
        {
            int row = matrix.Length, col = matrix[0].Length, maxLength = 0;
            int[,] dp1 = new int[row, col], dp2 = new int[row, col];
            for(int r = 0; r < row; r++) 
            {
                for(int c = 0; c < col; c++)
                {
                    dp1[r, c] = 1;
                    if (r > 0 && matrix[r][c] > matrix[r - 1][c])
                        dp1[r, c] = Math.Max(dp1[r, c], dp1[r - 1, c] + 1);
                    if (c > 0 && matrix[r][c] > matrix[r][c - 1])
                        dp1[r, c] = Math.Max(dp1[r, c], dp1[r, c - 1] + 1);
                }
            }

            for(int r = row - 1; r >= 0; --r)
            {
                for(int c = col - 1; c >= 0; --c)
                {
                    dp2[r, c] = 1;
                    if (r < row - 1 && matrix[r][c] < matrix[r + 1][c])
                        dp2[r, c] = Math.Max(dp2[r, c], dp2[r + 1, c] + 1);
                    if (c < col - 1 && matrix[r][c] < matrix[r][c + 1])
                        dp2[r, c] = Math.Max(dp2[r, c], dp2[r, c + 1] + 1);

                    if (dp1[r, c] + dp2[r, c] > maxLength)
                        maxLength = dp1[r, c] + dp2[r, c];
                }
            }

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    dp1[r, c] = 1;
                    if (r > 0 && matrix[r][c] < matrix[r - 1][c])
                        dp1[r, c] = Math.Max(dp1[r, c], dp1[r - 1, c] + 1);
                    if (c > 0 && matrix[r][c] < matrix[r][c - 1])
                        dp1[r, c] = Math.Max(dp1[r, c], dp1[r, c - 1] + 1);
                }
            }

            for (int r = row - 1; r >= 0; --r)
            {
                for (int c = col - 1; c >= 0; --c)
                {
                    dp2[r, c] = 1;
                    if (r < row - 1 && matrix[r][c] > matrix[r + 1][c])
                        dp2[r, c] = Math.Max(dp2[r, c], dp2[r + 1, c] + 1);
                    if (c < col - 1 && matrix[r][c] > matrix[r][c + 1])
                        dp2[r, c] = Math.Max(dp2[r, c], dp2[r, c + 1] + 1);

                    if (dp1[r, c] + dp2[r, c] > maxLength)
                        maxLength = dp1[r, c] + dp2[r, c];
                }
            }

            return maxLength - 1;
        }

        private static int[,] directions = new int[,] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };

        public int LongestIncreasingPath_DFS(int[][] matrix)
        {
            HashSet<(int, int)> inPath = new HashSet<(int, int)>();
            Dictionary<(int, int), int> cache = new Dictionary<(int, int), int>();
            int maxLength = 1;
            for(int r = 0; r < matrix.Length; ++r)
            {
                for(int c = 0; c < matrix[r].Length; ++c)
                {
                    //maxLength = Math.Max(maxLength, this.LongestIncreasingPath_Recursive(matrix, r, c, cache, inPath));
                    maxLength = Math.Max(maxLength, this.LongestIncreasingPath_Iterative(matrix, r, c, cache, inPath));
                }
            }

            return maxLength;
        }

        private int LongestIncreasingPath_Recursive(int[][] matrix, int r, int c, IDictionary<(int, int), int> cache, HashSet<(int, int)> inPath)
        {
            inPath.Add((r, c));
            if (!cache.TryGetValue((r, c), out int length))
            {
                int maxLength = 1;
                for (int d = 0; d < directions.GetLength(0); ++d)
                {
                    int nextR = r + directions[d, 0];
                    int nextC = c + directions[d, 1];
                    if (nextR < 0 || nextR >= matrix.Length || nextC < 0 || nextC >= matrix[nextR].Length ||
                        matrix[nextR][nextC] <= matrix[r][c] || inPath.Contains((nextR, nextC)))
                        continue;

                    int len = LongestIncreasingPath_Recursive(matrix, nextR, nextC, cache, inPath);
                    maxLength = Math.Max(maxLength, len + 1);
                }

                cache.Add((r, c), maxLength);
            }

            inPath.Remove((r, c));
            return cache[(r, c)];
        }

        private int LongestIncreasingPath_Iterative(int[][] matrix, int r, int c, IDictionary<(int, int), int> cache, HashSet<(int, int)> inPath)
        {
            if (!cache.TryGetValue((r, c), out int length))
            {
                length = 1;
                Stack<Position> stack = new Stack<Position>();
                stack.Push(new Position(r, c, 0, 1));
                inPath.Add((r, c));

                while (stack.Count != 0)
                {
                    var pos = stack.Peek();
                    if (pos.dir < directions.GetLength(0))
                    {
                        int nextR = pos.row + directions[pos.dir, 0];
                        int nextC = pos.col + directions[pos.dir, 1];
                        ++pos.dir;
                        if (nextR < 0 || nextR >= matrix.Length || nextC < 0 || nextC >= matrix[nextR].Length ||
                            inPath.Contains((nextR, nextC)) || matrix[nextR][nextC] <= matrix[pos.row][pos.col])
                            continue;

                        if (!cache.TryGetValue((nextR, nextC), out int len))
                        {
                            stack.Push(new Position(nextR, nextC, 0, pos.len + 1));
                            inPath.Add((nextR, nextC));
                        }
                        else
                        {
                            // if the value has been cached for next cell, then use it directly
                            if (pos.len + len > length)
                                length = pos.len + len;
                        }
                    }
                    else
                    {
                        stack.Pop();
                        inPath.Remove((pos.row, pos.col));
                        if (pos.len > length)
                            length = pos.len;

                        // When we pop out some cell from the stack, it is guranteed that all it neighbors have been explored
                        // so we can update the length of this cell with values from its neighbors
                        if (stack.Count > 0)
                        {
                            int len = 1;
                            for (int d = 0; d < pos.dir; ++d)
                            {
                                int adjR = pos.row + directions[d, 0];
                                int adjC = pos.col + directions[d, 1];

                                // the only neighbor still in stack is where we came from and the value of that neighbor must below the current one(that's why we can move to the current one)
                                if (!(adjR < 0 || adjR >= matrix.Length || adjC < 0 || adjC >= matrix[adjR].Length ||
                                      matrix[adjR][adjC] <= matrix[pos.row][pos.col]))
                                {
                                    len = Math.Max(len, cache[(adjR, adjC)] + 1);
                                }
                            }

                            cache.Add((pos.row, pos.col), len);
                        }
                    }
                }

                cache.Add((r, c), length);
            }

            return length;
        }
    }

    public class Position
    {
        public int row, col, dir, len;

        public Position(int r, int c, int d, int l)
        {
            this.row = r;
            this.col = c;
            this.dir = d;
            this.len = l;
        }
    }
}
