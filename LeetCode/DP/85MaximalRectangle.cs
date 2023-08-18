using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _85MaximalRectangle
    {
        public int LargestRectangleArea(char[][] matrix)
        {
            int res = 0;
            int[] heightsPerRow = new int[matrix[0].Length];
            for (int r = 0; r < matrix.Length; ++r)
            {
                for (int c = 0; c < matrix[r].Length; ++c)
                {
                    // accumulative increase the height on each cell of the current row
                    heightsPerRow[c] = matrix[r][c] == '1' ? heightsPerRow[c] + 1 : 0;
                }

                res = Math.Max(res, this.MaxRectangleInHistogram(heightsPerRow));
            }

            return res;
        }

        private int MaxRectangleInHistogram(int[] heights)
        {
            int res = 0;
            Stack<int> increasingStack = new Stack<int>(heights.Length);
            increasingStack.Push(-1);
            for (int i = 0; i < heights.Length; ++i)
            {
                while (increasingStack.Peek() != -1 && heights[increasingStack.Peek()] > heights[i])
                {
                    int index = increasingStack.Pop();
                    int height = heights[index];
                    res = Math.Max(res, height * (i - 1 - increasingStack.Peek()));
                }

                increasingStack.Push(i);
            }

            while (increasingStack.Peek() != -1)
            {
                int index = increasingStack.Pop();
                int height = heights[index];
                res = Math.Max(res, height * (heights.Length - 1 - increasingStack.Peek()));
            }

            return res;
        }

        public int MaximalRectangle_Column_Histogram(char[][] matrix)
        {
            int res = 0;
            int rows = matrix.Length, cols = matrix[0].Length;
            int[,] widths = new int[rows, cols];
            for(int r = 0; r < rows; r++)
            {
                int ones = 0;
                for (int c = 0; c < cols; c++)
                {
                    if (matrix[r][c] == '1')
                    {
                        widths[r, c] = ++ones;

                        int width = widths[r, c];
                        for (int row = r; row >= 0; --row)
                        {
                            width = Math.Min(width, widths[row, c]);
                            res = Math.Max(res, width * (r - row + 1));
                        }
                    }
                    else
                    {
                        ones = 0;
                    }
                }
            }

            return res;
        }

        private int maxRectangle = 0;

        public int MaximalRectangle_TopDown(char[][] matrix)
        {
            // not work
            int[,][] cache = new int[matrix.Length, matrix[0].Length][];
            for(int r = 0; r < matrix.Length; ++r)
            {
                for(int c = 0; c < matrix[r].Length; ++c)
                {
                    int[] dimenions = MaximalRectangle(matrix, r, c, cache);
                    int maxArea = Math.Max(dimenions[0] * dimenions[1], dimenions[2] * dimenions[3]);
                    if (maxArea > maxRectangle)
                        maxRectangle = maxArea;
                }
            }

            return this.maxRectangle;
        }

        private int[] MaximalRectangle(char[][] matrix, int row, int col, int[,][] cache)
        {
            if (row >= matrix.Length || row < 0 || col >= matrix[row].Length || col < 0)
                return new int[] { 0, 0, 0, 0 };

            if (cache[row, col] == null)
            {
                cache[row, col] = new int[4];
                if (matrix[row][col] == '1')
                {
                    var right = MaximalRectangle(matrix, row, col + 1, cache);
                    var bottom = MaximalRectangle(matrix, row + 1, col, cache);

                    int right1_width = right[0], right1_height = right[1];
                    int right2_width = right[2], right2_height = right[3];
                    int bottom1_width = bottom[0], bottom1_height = bottom[1];
                    int bottom2_width = bottom[2], bottom2_height = bottom[3];

                    if (right1_width + 1 <= bottom1_width)
                    {
                        cache[row, col][0] = right1_width + 1;
                    }
                    else
                    {
                        cache[row, col][0] = Math.Max(bottom1_width, 1);
                    }

                    if (bottom1_height + 1 <= right1_height)
                    {
                        cache[row, col][1] = bottom1_height + 1;
                    }
                    else
                    {
                        cache[row, col][1] = Math.Max(right1_height, 1);
                    }

                    if (right2_width + 1 <= bottom2_width)
                    {
                        cache[row, col][2] = right2_width + 1;
                    }
                    else
                    {
                        cache[row, col][2] = Math.Max(bottom2_width, 1);
                    }

                    if (bottom2_height + 1 <= right2_height)
                    {
                        cache[row, col][3] = bottom2_height + 1;
                    }
                    else
                    {
                        cache[row, col][3] = Math.Max(right2_height, 1);
                    }
                }
            }

            return cache[row, col];
        }
    }
}
