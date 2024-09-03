using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _957PrisonCellsAfterNDays
    {
        public int[] PrisonAfterNDays(int[] cells, int n)
        {
            var val2Iteration = new Dictionary<int, int>();
            var iteration2Val = new Dictionary<int, int>();
            int k = 0;
            for (int c = cells.Length - 1; c >= 0; --c)
                k |= cells[c] << (cells.Length - 1 - c);

            for (int i = 0; i < n; ++i)
            {
                if (val2Iteration.TryGetValue(k, out int iteration))
                {
                    int v = iteration2Val[((n - i) % (i - iteration)) + iteration];
                    for (int c = cells.Length - 1; c >= 0; --c)
                        cells[c] = (v & (1 << (cells.Length - 1 - c))) != 0 ? 1 : 0;
                    return cells;
                }
                else
                {
                    val2Iteration.Add(k, i);
                }
                iteration2Val.Add(i, k);

                int leftShift = k << 1, rightShift = k >> 1;
                k = ~(leftShift ^ rightShift);
                k &= 0x7e;
            }

            for (int c = cells.Length - 1; c >= 0; --c)
                cells[c] = (k & (1 << (cells.Length - 1 - c))) != 0 ? 1 : 0;

            return cells;
        }

        public int[] PrisonAfterNDaysV1(int[] cells, int n)
        {
            var val2Iteration = new Dictionary<int, int>();
            var iteration2Val = new Dictionary<int, int>();

            int k = 0;
            for (int c = cells.Length - 1; c >= 0; --c)
                k |= cells[c] << (cells.Length - 1 - c);

            for (int i = 0; i < n; ++i)
            {
                if (val2Iteration.TryGetValue(k, out int iteration))
                {
                    int v = iteration2Val[((n - i) % (i - iteration)) + iteration];
                    for (int c = cells.Length - 1; c >= 0; --c)
                        cells[c] = (v & (1 << (cells.Length - 1 - c))) != 0 ? 1 : 0;
                    return cells;
                }
                else
                {
                    val2Iteration.Add(k, i);
                }
                iteration2Val.Add(i, k);

                int priorCell = cells[0];
                k = cells[0] = 0;
                for (int c = 1; c < cells.Length - 1; ++c)
                {
                    int status = 0;
                    if (cells[c + 1] == priorCell)
                        status = 1;

                    priorCell = cells[c];
                    cells[c] = status;
                    k |= status << (cells.Length - 1 - c);
                }

                cells[cells.Length - 1] = 0;
            }

            return cells;
        }
    }
}
