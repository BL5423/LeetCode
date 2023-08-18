using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _1482MinimumNumberofDaystoMakemBouquets
    {
        public int MinDays(int[] bloomDay, int m, int k)
        {
            if (m * k > bloomDay.Length)
            {
                return -1;
            }

            int left = int.MaxValue, right = int.MinValue;
            foreach(var day in bloomDay)
            {
                if (left > day)
                    left = day;
                if (right < day)
                    right = day;
            }

            int res = -1;
            while (left <= right)
            {
                int mid = left + ((right - left) >> 1);
                int bouquets = 0, flowers = 0;
                foreach (var day in bloomDay)
                {
                    if (day <= mid)
                    {
                        if (++flowers == k)
                        {
                            ++bouquets;
                            flowers = 0;
                        }
                    }
                    else // day > mid
                    {
                        // reset the flowers picked so far
                        flowers = 0;
                    }
                }

                // too many bouquets means the mid might be too big
                if (bouquets >= m)
                {
                    res = mid;
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return res;
        }
    }
}
