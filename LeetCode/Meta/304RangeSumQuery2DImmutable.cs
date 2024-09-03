using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class NumMatrix
    {
        private int[,] sums; // sum[i, j] is the sum of all nums from sum[0, 0] to sum[i, j]

        public NumMatrix(int[][] matrix)
        {
            int rows = matrix.Length, cols = matrix[0].Length;
            this.sums = new int[rows, cols];
            for (int r = 0; r < rows; ++r)
            {
                int sum = 0;
                for (int c = 0; c < cols; ++c)
                {
                    this.sums[r, c] = sum + matrix[r][c] + (r - 1 >= 0 ? this.sums[r - 1, c] : 0);
                    sum += matrix[r][c];
                }
            }
        }

        public int SumRegion(int row1, int col1, int row2, int col2)
        {
            int total = this.sums[row2, col2];
            int above = 0;
            if (row1 > 0)
                above = this.sums[row1 - 1, col2];

            int left = 0;
            if (col1 > 0)
                left = this.sums[row2, col1 - 1];

            return total - above - left + (row1 > 0 && col1 > 0 ? this.sums[row1 - 1, col1 - 1] : 0);
        }
    }
}
