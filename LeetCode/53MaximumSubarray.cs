using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _53MaximumSubarray
    {
        public int MaxSubArray(int[] nums)
        {
            int sum = 0, maxSum = int.MinValue;
            foreach(int num in nums)
            {
                sum += num;
                if (sum > maxSum)
                {
                    maxSum = sum;
                }
                if (sum < 0)
                {
                    sum = 0;
                }
            }

            return maxSum;
        }
    }
}
