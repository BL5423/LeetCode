using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _849MaximizeDistancetoClosestPerson
    {
        public int MaxDistToClosest(int[] seats)
        {
            int maxDis = 0, curLen = 0, index = -1;
            for (int i = 0; i < seats.Length; ++i)
            {
                if (seats[i] == 0)
                {
                    if (index == -1)
                        index = i;
                    ++curLen;
                }
                else
                {
                    if (curLen != 0)
                    {
                        int left = index, right = index + curLen - 1;
                        if (left > 0 && right < seats.Length - 1)
                            maxDis = Math.Max(maxDis, (curLen / 2) + (curLen % 2));
                        else
                        {
                            maxDis = Math.Max(maxDis, curLen);
                        }
                    }

                    index = -1;
                    curLen = 0;
                }
            }

            if (curLen != 0)
            {
                int left = index, right = index + curLen - 1;
                if (left > 0 && right < seats.Length - 1)
                    maxDis = Math.Max(maxDis, (curLen / 2) + (curLen % 2));
                else
                {
                    maxDis = Math.Max(maxDis, curLen);
                }
            }

            return maxDis;
        }

        public int MaxDistToClosestV1(int[] seats)
        {
            int[] minDisToPersonOnLeft = new int[seats.Length];
            int[] minDisToPersonOnRight = new int[seats.Length];
            for (int i = 0, j = seats.Length - 1; i < seats.Length && j >= 0; ++i, --j)
            {
                if (seats[i] == 1)
                {
                    minDisToPersonOnLeft[i] = 0;
                }
                else
                {
                    minDisToPersonOnLeft[i] = 1 + (i > 0 ? minDisToPersonOnLeft[i - 1] : int.MaxValue - 1);
                }

                if (seats[j] == 1)
                {
                    minDisToPersonOnRight[j] = 0;
                }
                else
                {
                    minDisToPersonOnRight[j] = 1 + (j < seats.Length - 1 ? minDisToPersonOnRight[j + 1] : int.MaxValue - 1);
                }
            }

            int maxDis = 0;
            for (int i = 0; i < seats.Length; ++i)
            {
                if (seats[i] == 0)
                {
                    maxDis = Math.Max(maxDis, Math.Min(minDisToPersonOnLeft[i], minDisToPersonOnRight[i]));
                }
            }

            return maxDis;
        }
    }
}
