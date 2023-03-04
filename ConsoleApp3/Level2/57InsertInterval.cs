using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _57InsertInterval
    {
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            List<int[]> res = new List<int[]>();
            if (intervals.Length == 0)
            {
                res.Add(newInterval);
            }
            else
            {
                int leftIndex = FindPosition(intervals, newInterval[0]);
                int rightIndex = FindPosition(intervals, newInterval[1]);

                for (int index = 0; index < leftIndex; ++index)
                {
                    res.Add(intervals[index]);
                }

                var left = intervals[leftIndex];
                if (AheadOf(newInterval, left))
                {
                    res.Add(newInterval);
                    res.Add(left);
                }
                else
                {
                    var right = intervals[rightIndex];
                    int leftBound = left[0];
                    if (!HasOverlap(left, newInterval))
                    {
                        res.Add(left);
                        leftBound = newInterval[0];
                    }
                    else
                    {
                        leftBound = Math.Min(left[0], newInterval[0]);
                    }

                    var rightBound = Math.Max(right[1], newInterval[1]);
                    res.Add(new int[2] { leftBound, rightBound });
                }

                for (int index = rightIndex + 1; index < intervals.Length; ++index)
                {
                    res.Add(intervals[index]);
                }
            }

            return res.ToArray();
        }

        private bool AheadOf(int[] interval1, int[] interval2)
        {
            if (interval1[1] < interval2[0])
                return true;

            return false;
        }

        private bool HasOverlap(int[] interval1, int[] interval2)
        {
            if (interval1[1] < interval2[0] || interval2[1] < interval1[0])
                return false;

            return true;
        }

        private int FindPosition(int[][] intervals, int value)
        {
            int index = 0;
            int left = 0, right = intervals.Length - 1;
            while (left <= right)
            {
                int middle = left + ((right - left) >> 1);
                if (intervals[middle][0] <= value)
                {
                    index = middle;
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }

            return index;
        }
    }
}
