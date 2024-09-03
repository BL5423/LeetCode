using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _1762BuildingsWithanOceanView
    {
        public int[] FindBuildings(int[] heights)
        {
            var res = new LinkedList<int>();
            int hightsBuildingOnRight = 0; // the right most building always has ocean view
            for (int i = heights.Length - 1; i >= 0; --i)
            {
                if (heights[i] > hightsBuildingOnRight)
                {
                    hightsBuildingOnRight = heights[i];
                    res.AddFirst(i);
                }
            }

            return res.ToArray();
        }
    }
}
