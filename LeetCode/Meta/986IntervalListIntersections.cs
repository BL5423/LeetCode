using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _986IntervalListIntersections
    {
        public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
        {
            var res = new List<int[]>();
            int index1 = 0, index2 = 0;
            while (index1 < firstList.Length && index2 < secondList.Length)
            {
                int left = Math.Max(firstList[index1][0], secondList[index2][0]);
                int right = Math.Min(firstList[index1][1], secondList[index2][1]);
                if (left <= right)
                {
                    res.Add(new int[2] { left, right });
                }

                if (firstList[index1][1] <= secondList[index2][1])
                    ++index1;
                else
                    ++index2;
            }

            return res.ToArray();
        }
    }
}
