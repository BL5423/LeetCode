using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _973KClosestPointstoOrigin
    {
        public int[][] KClosest(int[][] points, int k)
        {
            int index = -1, left = 0, right = points.Length - 1;
            while (index != k)
            {
                int pivotIndex = Random.Shared.Next(left, right + 1);
                int[] pivot = points[pivotIndex];
                int pivotDistance = Distance(pivot);

                // swap pivot with points[right]
                var temp = points[right];
                points[right] = pivot;
                points[pivotIndex] = temp;

                // any point before start is closer to origin than pivot, any point after end is farther to origin than pivot
                int start = left, end = right - 1;
                while (start <= end)
                {
                    if (Distance(points[start]) <= pivotDistance)
                        ++start;
                    else // Distance(points[start]) > pivotDistance
                    {
                        // swap start and end
                        var p = points[start];
                        points[start] = points[end];
                        points[end] = p;
                        --end;
                    }
                }

                // start > end
                // swap pivot(points[right]) back to points[start]
                temp = points[start];
                points[start] = pivot;
                points[right] = temp;

                if (start == k - 1)
                    break;
                else if (start > k - 1)
                {
                    // skip 'same' points
                    while (start > 0 && Distance(points[start]) == Distance(points[start - 1]))
                        --start;
                    right = start;
                }
                else // start < k - 1
                {
                    while (start < points.Length - 1 && Distance(points[start]) == Distance(points[start + 1]))
                        ++start;
                    left = start;
                }
            }

            int[][] res = new int[k][];
            for (int i = 0; i < k; ++i)
                res[i] = points[i];

            return res;
        }

        private static int Distance(int[] point)
        {
            return point[0] * point[0] + point[1] * point[1];
        }

        public int[][] KClosestHeap(int[][] points, int k)
        {
            var maxHeap = new PriorityQueue<int, int>(k + 1);
            for (int i = 0; i < points.Length; ++i)
            {
                int[] point = points[i];
                maxHeap.Enqueue(i, -Distance(point));

                if (maxHeap.Count == k + 1)
                    maxHeap.Dequeue();
            }

            int[][] res = new int[k][];
            for (int i = 0; i < k; ++i)
                res[i] = points[maxHeap.Dequeue()];

            return res;
        }
    }
}
