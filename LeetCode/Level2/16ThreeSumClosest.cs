using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _16ThreeSumClosest
    {
        public int ThreeSumClosest(int[] nums, int target)
        {
            if (nums.Length <= 3)
                return nums.Sum();

            Array.Sort(nums);
            int min = int.MaxValue, result = -1;
            for(int anchor = 0; anchor < nums.Length - 2; ++anchor)
            {
                int anchorNum = nums[anchor];
                int start = anchor + 1, end = nums.Length - 1;
                while (start < end)
                {
                    int window = nums[start] + nums[end];
                    int diff = Math.Abs(target - (anchorNum + window));
                    if (min > diff)
                    {
                        min = diff;
                        result = anchorNum + window;
                    }

                    if (min == 0)
                        return target;

                    if (anchorNum + window < target)
                    {
                        ++start;
                    }
                    else
                    {
                        --end;
                    }
                }
            }

            return result;
        }
    }
}
