using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _525ContiguousArray
    {
        public int FindMaxLength(int[] nums)
        {
            Dictionary<int, int> prevSums = new Dictionary<int, int>(nums.Length);
            prevSums[0] = -1;
            int sum = 0, maxLength = int.MinValue;
            for(int index = 0; index < nums.Length; ++index)
            {
                sum += (nums[index] == 0 ? -1 : 1);
                if (prevSums.TryGetValue(sum, out int prevIndex))
                {
                    maxLength = Math.Max(maxLength, index - prevIndex);
                }
                else
                {
                    prevSums[sum] = index;
                }
            }

            return maxLength != int.MinValue ? maxLength : 0;
        }

        public int FindMaxLengthV1(int[] nums)
        {
            int count = 0, length = 0;
            Dictionary<int, int> countToIndex = new Dictionary<int, int>();
            // count equals to 0 means all the 0s and 1s we encouters since the begining of the array are with equal numbers
            // and the index of the first 0 is set to -1 so that the length(difference) between the actual index of an element in array well be correctly measured(i.e. last element at n - 1 - (-1) = n).
            countToIndex[0] = -1;
            for(int i = 0; i < nums.Length; ++i)
            {
                count += (nums[i] == 0 ? -1 : 1);
                if (countToIndex.TryGetValue(count, out int index))
                {
                    length = Math.Max(length, i - index);
                }
                else
                {
                    countToIndex[count] = i;
                }
            }

            return length;
        }
    }
}
