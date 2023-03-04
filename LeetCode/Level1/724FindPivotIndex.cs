using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _724FindPivotIndex
    {
        public int PivotIndex(int[] nums)
        {
            int leftSum = 0;
            int rightSum = nums.Sum() - nums[0];
            int pivot = 0;
            for(; pivot < nums.Length; ++pivot)
            {
                rightSum -= nums[pivot];

                if (leftSum == rightSum)
                    return pivot;

                leftSum += nums[pivot];
            }

            return -1;
        }
    }
}
