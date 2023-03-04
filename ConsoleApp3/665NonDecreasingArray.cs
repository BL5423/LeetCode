using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _665NonDecreasingArray
    {
        public bool CheckPossibility(int[] nums)
        {
            int exceptions = 0;
            int previous = int.MinValue;
            int index = 0;
            while (index < nums.Length - 1)
            {
                if (nums[index] > nums[index + 1])
                {
                    if (exceptions == 1)
                        return false;

                    // try to set nums[index] to nums[index + 1]
                    if ((nums[index + 1] < previous) &&
                        // try to set nums[index + 1] to nums[index]
                        (index + 2 < nums.Length && nums[index] > nums[index + 2]))
                    {
                        return false;
                    }

                    ++exceptions;
                }

                previous = nums[index];
                ++index;
            }

            return exceptions < 2;
        }
    }
}
