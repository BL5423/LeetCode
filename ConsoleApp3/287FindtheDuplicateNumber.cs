using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _287FindtheDuplicateNumber
    {
        public int FindDuplicate(int[] nums)
        {
            int low = 1, high = nums.Length - 1;
            int dup = -1;
            while (low <= high)
            {
                int middle = low + ((high - low) >> 1);
                int count = nums.Count(num => num <= middle);

                // potential dups found, but we don't know which exact num has dups
                if (count > middle)
                {
                    // keep the current num as a candidate
                    dup = middle;
                    // keep looking for the minimal/first num that has dups
                    high = middle - 1;
                }
                else
                {
                    low = middle + 1;
                }
            }

            return dup;
        }
        
        public int FindDuplicateTwoPointers(int[] nums)
        {
            // two pointers to find the first node on a list with cycle
            int slowIndex = nums[0];
            int fastIndex = nums[slowIndex];
            while (fastIndex != slowIndex)
            {
                slowIndex = nums[slowIndex];
                fastIndex = nums[nums[fastIndex]];
            }

            // reset fastIndex to the begining and slow it down
            fastIndex = 0;
            while(fastIndex != slowIndex)
            {
                slowIndex = nums[slowIndex];
                fastIndex = nums[fastIndex];
            }

            return fastIndex;
        }

        public int FindDuplicateNegative(int[] nums)
        {
            int result = -1;
            foreach(int num in nums)
            {
                int value = num;
                if (value < 0)
                {
                    value = -value;
                }

                if (nums[value - 1] < 0)
                {
                    result = value;
                    break;
                }

                nums[value - 1] *= -1;
            }

            for (int index = 0; index < nums.Length; ++index)
            {
                if (nums[index] < 0)
                {
                    nums[index] *= -1;
                }
            }

            return result;
        }
    }
}
