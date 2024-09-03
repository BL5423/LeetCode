using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _31NextPermutation
    {
        public void NextPermutation(int[] nums)
        {
            bool found = false;
            for (int i = nums.Length - 2; !found && i >= 0; --i)
            {
                if (nums[i] < nums[i + 1])
                {
                    // binary search to find the minimal num after i that is larger than i
                    int j = BinarySearch(nums, nums[i], i + 1, nums.Length - 1);

                    // swap
                    int temp = nums[j];
                    nums[j] = nums[i];
                    nums[i] = temp;

                    Array.Reverse(nums, i + 1, nums.Length - 1 - i);
                    //Array.Sort(nums, i + 1, nums.Length - i - 1);

                    found = true;
                }
            }

            if (!found)
            {
                Array.Reverse(nums);
            }
        }

        private int BinarySearch(int[] nums, int target, int start, int end)
        {
            int index = end;
            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (nums[mid] <= target)
                {
                    end = mid - 1;
                }
                else // nums[mid] > target
                {
                    index = mid;
                    start = mid + 1;
                }
            }

            // start > end
            return index;
        }

        public void NextPermutationV3(int[] nums)
        {
            int index = nums.Length - 1;
            while (index > 0)
            {
                if (nums[index] <= nums[index - 1])
                    --index;
                else
                    break;
            }

            if (index > 0)
            {
                int targetIndex = nums.Length - 1;
                while (targetIndex >= index)
                {
                    if (nums[targetIndex] <= nums[index - 1])
                        --targetIndex;
                    else
                        break;
                }

                if (targetIndex > index - 1)
                {
                    // swap
                    int temp = nums[index - 1];
                    nums[index - 1] = nums[targetIndex];
                    nums[targetIndex] = temp;
                }
                else // targetIndex == index
                {
                    int temp = nums[index - 1];
                    nums[index - 1] = nums[targetIndex];
                    nums[targetIndex] = temp;

                    index = nums.Length;
                }
            }
            else
            {
                // nums are already in descenting order
                index = 0;
            }

            // reverse the numbers right to index
            int left = index, right = nums.Length - 1;
            while (left < right)
            {
                int temp = nums[left];
                nums[left] = nums[right];
                nums[right] = temp;

                ++left;
                --right;
            }
        }

        public void NextPermutationV2(int[] nums)
        {
            if (nums.Length <= 1)
                return;

            NextPermutation(nums, 0);
        }

        private bool NextPermutation(int[] nums, int startIndex)
        {
            int largers = 0, smallers = 0;
            for (int i = startIndex + 1; i < nums.Length; i++)
            {
                if (nums[i] >= nums[i - 1])
                    ++largers;
                if (nums[i] <= nums[i - 1])
                    ++smallers;
            }

            if (largers == nums.Length - 1 - startIndex)
            {
                int index = nums.Length - 1;
                while (index > startIndex && nums[index] == nums[index - 1])
                    --index;

                if (index > startIndex)
                {
                    // i.e. 1 2 3 then swap the last two numbers
                    int temp = nums[index];
                    nums[index] = nums[index - 1];
                    nums[index - 1] = temp;
                }
                else
                {
                    // for something like 3 3 3 return true to indicate a carry
                    return true;
                }
            }
            else if (smallers == nums.Length - 1 - startIndex)
            {
                // i.e. 3 2 1 then rotate the whole array
                int left = startIndex, right = nums.Length - 1;
                while (left < right)
                {
                    int temp = nums[left];
                    nums[left] = nums[right];
                    nums[right] = temp;

                    ++left;
                    --right;
                }

                return true;
            }
            else
            {
                // only need to find the next permutation of sub array
                if (NextPermutation(nums, startIndex + 1))
                {
                    // if we got a carry from sub array
                    int num0 = nums[startIndex];
                    int index = startIndex + 1;
                    while (nums[index] <= num0)
                    {
                        ++index;
                    }

                    nums[startIndex] = nums[index];
                    nums[index] = num0;
                }
            }

            return false;
        }

        public void NextPermutationV1(int[] nums)
        {
            /* not working */
            if (nums.Length <= 1)
                return;

            // look for the first number can be swapped with it precessor, we stop at index 2 and 1
            int index = -1;
            for(int i = nums.Length - 1; i > 1; --i)
            {
                if (nums[i] > nums[i - 1])
                {
                    // found potential numbers to swap
                    index = i;
                }
            }

            if (index != -1)
            {
                // nums[index] >= nums[index - 1], so just swap them
                // 1    2     3     4 -> 1 3 2 4
                //          index
                int temp = nums[index];
                nums[index] = nums[index - 1];
                nums[index - 1] = temp;

            }
            else
            {
                if (nums[1] >= nums[0])
                {
                    // by shifting all numbers one step to right
                    if (nums[nums.Length - 1] > nums[0])
                    {
                        // 1    3    2   -> 2   1   3
                        int num = nums[nums.Length - 1];
                        for(int i = nums.Length - 2; i >= 0; --i)
                        {
                            nums[i + 1] = nums[i];
                        }

                        nums[0] = num;
                    }
                    else
                    {
                        // 2    3    1   -> 3   1   2
                        int num = nums[0];
                        for (int i = 1; i < nums.Length; ++i)
                        {
                            nums[i - 1] = nums[i];
                        }

                        nums[nums.Length - 1] = num;
                    }
                }
                else
                {
                    // no potential numbers to swap
                    // 3 2 1
                    int left = 0, right = nums.Length - 1;
                    while (left < right)
                    {
                        // swap left and right
                        int temp = nums[left];
                        nums[left] = nums[right];
                        nums[right] = temp;

                        ++left;
                        --right;
                    }
                }
            }
        }
    }
}
