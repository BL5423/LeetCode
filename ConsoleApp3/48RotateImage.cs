using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _48RotateImage
    {
        public void Rotate(int[][] matrix)
        {
            int n = matrix.Length;
            if (n > 1)
            {
                for (int index = 0; index < n; ++index)
                {
                    RotateRectangle(matrix, index, n - 1 - index);
                }
            }
        }

        private void RotateRectangle(int[][] matrix, int start, int end)
        {
            // There are four edges of the rectangle:
            // (start, start)  -1-   (start, end)
            //       |                  |
            //       4                  2
            //       |                  |   
            // (end,   start)  -3-   (end,   end)
            // we're going to rotate items on edge1 to edge2
            // and then edge2 to edge3
            // and then edge3 to edge4
            // and last edge4 to edge1
            for (int index = start; index < end; ++index)
            {
                // edge1 to edge2
                int i1 = start, j1 = index;
                int i2 = index, j2 = end;
                int e2 = matrix[i2][j2];
                matrix[i2][j2] = matrix[i1][j1];

                // edge2 to edge3
                int i3 = end, j3 = end - (index - start);
                int e3 = matrix[i3][j3];
                matrix[i3][j3] = e2;

                // edge3 to edge4
                int i4 = end - (index - start), j4 = start;
                int e4 = matrix[i4][j4];
                matrix[i4][j4] = e3;

                // edge4 to edge1
                matrix[i1][j1] = e4;
            }
        }
    }
}
