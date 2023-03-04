using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _75SortColors
    {
        public void SortColors(int[] nums)
        {
            int[] counts = new int[] { 0, 0, 0 };
            foreach (int num in nums)
            {
                ++counts[num];
            }

            for (int i = 0; i < nums.Length; ++i)
            {
                if (counts[0]-- > 0)
                {
                    nums[i] = 0;
                }
                else if (counts[1]-- > 0)
                {
                    nums[i] = 1;
                }
                else if (counts[2]-- > 0)
                {
                    nums[i] = 2;
                }
            }
        }

        public void SortColorsV2(int[] nums)
        {
            // start is the last index of 0's
            // middle is the last index of 1's
            // end is the lat index of 2's
            int start = 0, middle = 0, end = nums.Length - 1;
            while(middle <= end)
            {
                switch(nums[middle])
                {
                    case 0:
                        // swap elements at start and middle
                        Swap(nums, start, middle);
                        ++start;
                        ++middle;
                        break;

                    case 1:
                        // do nothing if element at middle is already 1
                        ++middle;
                        break;

                    case 2:
                        // swap elements at middle and end
                        Swap(nums, middle, end);
                        --end;
                        break;
                }
            }
        }

        private void Swap(int[] nums, int index1, int index2)
        {
            int value = nums[index1];
            nums[index1] = nums[index2];
            nums[index2] = value;
        }
    }
}
