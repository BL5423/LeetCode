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
            int left = -1, right = nums.Length;
            int index = 0;
            while (index < right)
            {
                if (nums[index] == 1)
                {
                    ++index;
                }
                else if (nums[index] == 2)
                {
                    int temp = nums[right - 1];
                    nums[right - 1] = nums[index];
                    nums[index] = temp;

                    --right;
                }
                else // nums[index] == 0
                {
                    int temp = nums[left + 1];
                    nums[left + 1] = nums[index];
                    nums[index] = temp;

                    if (++left == index)
                        ++index;
                }
            }
        }

        public void SortColorsV3(int[] nums)
        {
            // move all '0' to the left sub-array
            int left = this.Move(nums, -1, 0);

            // move all '1' to the left sub-array
            this.Move(nums, left, 1);
        }

        private int Move(int[] nums, int left, int target)
        {
            // move all targets to the left sub-array
            int right = left + 1;
            while (right < nums.Length)
            {
                if (nums[right] == target)
                {
                    int temp = nums[left + 1];
                    nums[left + 1] = nums[right];
                    nums[right] = temp;

                    if (++left == right)
                        ++right;
                }
                else
                {
                    ++right;
                }
            }

            return left;
        }

        public void SortColorsV2(int[] nums)
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

        public void SortColorsV1(int[] nums)
        {
            // start is the prev index of the latest 0 from the beginning
            // middle is the last index of 1's
            // end is the prior index of the latest 2 from the end
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
