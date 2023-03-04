using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _33SearchInRotatedArray
    {
        public int SearchV3(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;
            while (left <= right)
            {
                int mid = left + ((right - left) >> 1);
                if (nums[mid] == target)
                    return mid;

                // no rotate
                if (nums[left] <= nums[mid] && nums[mid] < nums[right])
                {
                    if (nums[mid] > target)
                    {                    
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }

                // with rotate
                else if (nums[left] <= nums[mid])
                {
                    // mid is in the left part
                    if (nums[mid] < target)
                    {
                        left = mid + 1;
                    }
                    else if (nums[left] > target)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
                else
                {
                    // mid is in the right part
                    if (nums[mid] > target)
                    {
                        right = mid - 1;
                    }
                    else if (nums[right] < target)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
            }

            return -1;
        }

        public int SearchV2(int[] nums, int target)
        {
            int s = 0, e = nums.Length - 1;
            while (s <= e)
            {
                int middle = s + ((e - s) >> 1);
                int m = nums[middle];
                if (m == target)
                {
                    return middle;
                }
                else /*if (nums[s] >= nums[e])*/
                {
                    // rotation confirmed
                    // and m is in the bigger part
                    if (m >= nums[s])
                    {
                        // if target is between s and m
                        if (nums[s] <= target && m > target)
                        {
                            // move left
                            e = middle - 1;
                        }
                        else
                        {
                            // move right
                            s = middle + 1;
                        }
                    }
                    else
                    {
                        // m is in the smaller part
                        // if m is between target and e
                        if (m < target && nums[e] >= target)
                        {
                            // move right
                            s = middle + 1;
                        }
                        else
                        {
                            // move left
                            e = middle - 1;
                        }
                    }
                }
                /*else // no rotate
                {
                    if (m < target)
                        s = middle + 1;
                    else
                        e = middle - 1;
                }
                */
            }

            return -1;
        }

        public int SearchV1(int[] nums, int target)
        {
            return BinarySearchOnRotatedArray(nums, target, 0, nums.Length - 1);
            /*
            int indexOfMax = -1;
            int max = int.MinValue;
            FindTheIndexOfMax(nums, 0, nums.Length - 1, ref indexOfMax, ref max);
            int index = BinarySearch(nums, target, 0, indexOfMax);
            if (index >= 0)
                return index;
            return BinarySearch(nums, target, indexOfMax + 1, nums.Length - 1);
            */
        }

        private int BinarySearchOnRotatedArray(int[] nums, int target, int start, int end)
        {
            while (start <= end)
            {
                int middle = (start + end) / 2;
                if (nums[middle] == target)
                {
                    return middle;
                }

                if (nums[middle] >= nums[start])
                {
                    if (target >= nums[start] && target < nums[middle])
                    {
                        end = middle - 1;
                    }
                    else
                    {
                        start = middle + 1;
                    }
                }
                else if (nums[middle] <= nums[end])
                {
                    if (target <= nums[end] && target > nums[middle])
                    {
                        start = middle + 1;
                    }
                    else
                    {
                        end = middle - 1;
                    }
                }
            }

            return -1;
        }

        private int BinarySearch(int[] nums, int target, int start, int end)
        {
            while (start <= end)
            {
                int middle = (start + end) / 2;
                if (nums[middle] == target)
                {
                    return middle;
                }
                else if (nums[middle] > target)
                {
                    end = middle - 1;
                }
                else // nums[middle] < target
                {
                    start = middle + 1;
                }
            }

            return -1;
        }

        private void FindTheIndexOfMax(int[] nums, int start, int end, ref int index, ref int max)
        {
            //if (start > end)
            //    return;

            //if (start == end || nums[start] < nums[end])
            //{
            //    if (nums[end] > max)
            //    {
            //        index = end;
            //        max = nums[end];
            //    }
            //    return;
            //}

            //int middle = (start + end) / 2;
            //if (nums[middle] > nums[start])
            //    FindTheIndexOfMax(nums, middle, end, ref index, ref max);
            //else
            //    FindTheIndexOfMax(nums, start, middle, ref index, ref max);

            while (start <= end)
            {
                if (nums[start] <= nums[end])
                {
                    if (nums[end] > max)
                    {
                        index = end;
                        max = nums[end];
                    }

                    if (start == end)
                        return;
                }

                int middle = (start + end) / 2;
                if (nums[middle] > nums[start])
                {
                    //FindTheIndexOfMax(nums, middle, end, ref index, ref max);
                    start = middle;
                }
                else
                {
                    //FindTheIndexOfMax(nums, start, middle, ref index, ref max);
                    end = middle;
                }
            }
        }
    }
}
