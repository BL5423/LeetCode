using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _1011CapacityToShipPackagesWithinDDays
    {
        public int ShipWithinDays(int[] weights, int days)
        {
            int left = int.MinValue, right = 0;
            foreach(var weight in weights)
            {
                if (left < weight)
                    left = weight;
                right += weight;
            }

            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                int totalSoFar = 0, daysNeed = 0;
                foreach(var weight in weights)
                {
                    int total = totalSoFar + weight;
                    if (total > mid)
                    {
                        ++daysNeed;
                        totalSoFar = 0;
                    }

                    totalSoFar += weight;
                }

                if (totalSoFar != 0)
                {
                    ++daysNeed;
                }

                if (daysNeed > days)
                {
                    left = mid + 1;
                }
                else // daysNeed <= days
                {
                    right = mid;
                }
            }

            return right;
        }
    }
}
