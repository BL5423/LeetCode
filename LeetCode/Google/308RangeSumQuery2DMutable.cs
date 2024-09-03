using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _308RangeSumQuery2DMutable
    {
        public class NumMatrix
        {
            private int[][] mCopy;

            private BIT2D tree;

            public NumMatrix(int[][] matrix)
            {
                this.tree = new BIT2D(matrix);
                this.mCopy = matrix;
            }

            public void Update(int row, int col, int val)
            {
                int delta = val - this.mCopy[row][col];
                this.mCopy[row][col] = val;
                this.tree.Update(row, col, delta);
            }

            public int SumRegion(int row1, int col1, int row2, int col2)
            {
                return this.tree.Query(row2, col2) -
                       this.tree.Query(row1 - 1, col2) -
                       this.tree.Query(row2, col1 - 1) +
                       this.tree.Query(row1 - 1, col1 - 1);
            }
        }
    }

    public class BIT2D
    {
        private int[,] buffer;

        private int rows, cols;

        public BIT2D(int[][] m)
        {
            this.rows = m.Length + 1;
            this.cols = m[0].Length + 1;
            this.buffer = new int[rows, cols];
            for (int r = 0; r < m.Length; ++r)
            {
                for (int c = 0; c < m[0].Length; ++c)
                    this.Update(r, c, m[r][c]);
            }
        }

        public void Update(int row, int col, int val)
        {
            for (int r = row + 1; r < this.rows; r += r & (-r))
            {
                for (int c = col + 1; c < this.cols; c += c & (-c))
                    this.buffer[r, c] += val;
            }
        }

        public int Query(int row, int col)
        {
            int res = 0;
            for (int r = row + 1; r > 0; r -= r & (-r))
            {
                for (int c = col + 1; c > 0; c -= c & (-c))
                    res += this.buffer[r, c];
            }
            return res;
        }
    }
}
