using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _704BinarySearch
    {
        public int Search(int[] nums, int target)
        {
            int start = 0, end = nums.Length - 1;
            int index = -1;
            while (start <= end)
            {
                int middle = (start + end) / 2;
                if (nums[middle] >= target)
                {
                    if (nums[middle] == target)
                    {
                        index = middle;
                    }

                    end = (middle != end ? middle : middle - 1);
                }
                else
                {
                    start = middle + 1;
                }
            }

            return index;
        }
    }
}
