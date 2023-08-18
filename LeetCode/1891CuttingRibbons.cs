using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _1891CuttingRibbons
    {
        public int MaxLength(int[] ribbons, int k)
        {
            int left = 1, right = ribbons.Max(), res = 0;
            while (left <= right)
            {
                int mid = left + ((right - left) >> 1);
                int cuts = 0;
                foreach(var ribbon in ribbons)
                {
                    cuts += (ribbon / mid);
                }

                if (cuts >= k)
                {
                    res = mid;
                    left = mid + 1;
                }
                else // cuts < k
                {
                    right = mid - 1;
                }
            }

            // left > right
            return res;
        }
    }
}
