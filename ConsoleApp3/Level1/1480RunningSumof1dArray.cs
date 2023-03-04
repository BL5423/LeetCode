using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _1480RunningSumof1dArray
    {
        public int[] RunningSum(int[] nums)
        {
            int sum = 0, index = 0;
            foreach(int num in nums)
            {
                sum += num;
                nums[index++] = sum;
            }

            return nums;
        }
    }
}
