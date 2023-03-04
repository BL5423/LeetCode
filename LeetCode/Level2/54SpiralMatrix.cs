using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _54SpiralMatrix
    {
        public IList<int> SpiralOrder(int[][] matrix)
        {
            int width = matrix[0].Length;
            int height = matrix.Length;
            var res = new List<int>(width * height);
            int cStart = 0, lStart = 0;
            while (width > 0 && height > 0)
            {
                // top
                for (int t = cStart; t < cStart + width; ++t)
                {
                    res.Add(matrix[lStart][t]);
                }

                if (width > 0)
                {
                    // right
                    for (int r = lStart + 1; r < lStart + height; ++r)
                    {
                        res.Add(matrix[r][cStart + width - 1]);
                    }
                }

                if (height > 1)
                {
                    // bottom
                    for (int b = cStart + width - 2; b > cStart; --b)
                    {
                        res.Add(matrix[lStart + height - 1][b]);
                    }
                }

                if (width > 1)
                {
                    // left
                    for (int l = lStart + height - 1; l > lStart; --l)
                    {
                        res.Add(matrix[l][cStart]);
                    }
                }

                ++lStart;
                ++cStart;
                width -= 2;
                height -= 2;
            }

            return res;
        }

        public IList<int> SpiralOrderV1(int[][] matrix)
        {
            int width = matrix[0].Length;
            int height = matrix.Length;
            var res = new List<int>(width * height);
            SpiralOrderRecursively(matrix, 0, 0, width, height, res);
            return res;
        }

        private void SpiralOrderRecursively(int[][] matrix, int lStart, int cStart, int width, int height, IList<int> res)
        {
            if (width <= 0 || height <= 0)
                return;

            // top
            for(int t = cStart; t < cStart + width; ++t)
            {
                res.Add(matrix[lStart][t]);
            }

            if (width > 0)
            {
                // right
                for (int r = lStart + 1; r < lStart + height; ++r)
                {
                    res.Add(matrix[r][cStart + width - 1]);
                }
            }

            if (height > 1)
            {
                // bottom
                for (int b = cStart + width - 2; b > cStart; --b)
                {
                    res.Add(matrix[lStart + height - 1][b]);
                }
            }

            if (width > 1)
            {
                // left
                for (int l = lStart + height - 1; l > lStart; --l)
                {
                    res.Add(matrix[l][cStart]);
                }
            }

            SpiralOrderRecursively(matrix, lStart + 1, cStart + 1, width - 2, height - 2, res);
        }
    }
}
