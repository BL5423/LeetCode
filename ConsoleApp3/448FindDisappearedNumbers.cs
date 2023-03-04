using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _448FindDisappearedNumbers
    {
        public IList<int> FindDisappearedNumbers(int[] nums)
        {
            var result = new List<int>();
            for (int index = 0; index < nums.Length; ++index)
            {
                int targetIndex = Math.Abs(nums[index]) - 1;
                if (nums[targetIndex] > 0)
                {
                    // put a flag indicates the number(targetIndex) exists.
                    nums[targetIndex] *= -1;
                }
            }

            for(int index = 0; index < nums.Length; ++index)
            {
                if (nums[index] > 0)
                {
                    result.Add(index + 1);
                }
            }

            return result;
        }
    }
}
