using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Top100Liked
{
    public class _1TwoSum
    {
        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> set = new Dictionary<int, int>(nums.Length);
            for(int index = 0; index < nums.Length; ++index)
            {
                if (set.TryGetValue(target - nums[index], out int i))
                {
                    return new int[] { i, index };
                }
                else
                {
                    set[nums[index]] = index;
                }
            }

            return null;
        }

        public int[] TwoSum2(int[] numbers, int target)
        {
            int start = 0, end = numbers.Length - 1;
            while (start < end)
            {
                int sum = numbers[start] + numbers[end];
                if (sum == target)
                {
                    return new int[] { start + 1, end + 1 };
                }
                else if (sum < target)
                {
                    ++start;
                }
                else
                {
                    --end;
                }
            }

            return null;
        }
    }
}
