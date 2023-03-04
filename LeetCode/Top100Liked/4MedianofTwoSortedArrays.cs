using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Top100Liked
{
    public class _4MedianofTwoSortedArrays
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length == 0 && nums2.Length == 0)
            {
                return 0;
            }

            if (nums1.Length > nums2.Length)
            {
                // search in the shorter array
                return FindMedianSortedArrays(nums2, nums1);
            }

            if (nums1.Length == 0)
            {
                if (nums2.Length % 2 == 1)
                {
                    return nums2[nums2.Length / 2];
                }
                else
                {
                    return nums2[nums2.Length / 2 - 1] * 0.5 + nums2[nums2.Length / 2] * 0.5;
                }
            }

            int m = nums1.Length, n = nums2.Length;
            int start1 = 0, end1 = nums1.Length - 1;
            while (true)
            {
                // the pos in nums1 is determined by mid1(which indicates the # of numbers we have selected from nums1)
                int mid1 = start1 + ((end1 - start1) >> 1);

                // once mid1 is decided, we need to find the pos for mid2, which should contribute the numbers left, i.e. the half of total - (mid1 + 1)
                // so that sum of mid1 and mid2 will be half of total
                //          total        # is nums1   convert to index in nums2
                int mid2 = (m + n) / 2 - (mid1 + 1) - 1;

                int left1 = GetNum(mid1, nums1);
                int right1 = GetNum(mid1 + 1, nums1);
                int left2 = GetNum(mid2, nums2);
                int right2 = GetNum(mid2 + 1, nums2);
                if (left1 <= right2 && left2 <= right1)
                {
                    // if total is odd, then the median would locate on right sides of mid1 and mid2
                    if ((m + n) % 2 == 1)
                    {
                        return Math.Min(right1, right2);
                    }
                    else
                    {
                        // if total is even, then there are two medians
                        return Math.Max(left1, left2) * 0.5 + Math.Min(right1, right2) * 0.5;
                    }
                }
                else if (left1 > right2)
                {
                    end1 = mid1 - 1;
                }
                else // left2 > right1
                {
                    start1 = mid1 + 1;
                }
            }
        }

        private static int GetNum(int index, int[] nums)
        {
            return index >= 0 && index < nums.Length ? nums[index] : (index < 0 ? int.MinValue : int.MaxValue);
        }

        public double FindMedianSortedArraysV1(int[] nums1, int[] nums2)
        {
            if (nums1.Length < nums2.Length)
                return FindMedianSortedArraysV1(nums2, nums1);

            int start = 0, end = nums2.Length - 1;
            while (true)
            {
                int middle2 = start + ((end - start) >> 1);
                //int middle2 = (int)Math.Floor((end + start) / 2.0);
                //int middle2 = (start + end) >> 1;
                int middle1 = (nums1.Length + nums2.Length) / 2 - 2 - middle2;

                int left2 = middle2 >= 0 ? nums2[middle2] : int.MinValue;
                int right2 = middle2 + 1 < nums2.Length ? nums2[middle2 + 1] : int.MaxValue;
                int left1 = middle1 >= 0 ? nums1[middle1] : int.MinValue;
                int right1 = middle1 + 1 < nums1.Length ? nums1[middle1 + 1] : int.MaxValue;
                if (left2 <= right1 && left1 <= right2)
                {
                    // found the right split pos
                    if ((nums1.Length + nums2.Length) % 2 == 1)
                    {
                        return Math.Min(right1, right2);
                    }
                    else
                    {
                        return (Math.Max(left1, left2) + Math.Min(right1, right2)) / 2.0;
                    }
                }
                else if (left2 > right1)
                {
                    end = middle2 - 1;
                }
                else // left1 > right2
                {
                    start = middle2 + 1;
                }
            }
        }

        public double FindMedianSortedArraysV2(int[] nums1, int[] nums2)
        {
            int extraNumberForMedians = (nums1.Length + nums2.Length) % 2 == 0 ? 1 : 0;
            int[] medians = new int[extraNumberForMedians + 1];
            int res = 0;

            res = LookForMedian(nums1, nums2, medians, res);
            if (res != medians.Length)
            {
                LookForMedian(nums2, nums1, medians, res);
            }

            return medians.Length == 1 ? medians[0] : medians.Sum() / 2.0;
        }

        private int LookForMedian(int[] source, int[] target, int[] medians, int res)
        {
            int found = 0;
            int length = source.Length;
            int start = 0, end = length - 1;
            while (start <= end)
            {
                int middle = start + (end - start) / 2;
                int index = FindToLeftMost(target, source[middle], out bool multiple);
                int index2 = index;
                if (multiple)
                {
                    index2 = FindToRightMost(target, source[middle]);
                }

                int leftLength = middle + index;
                int rightLength = length - 1 - middle + (target.Length - index);
                if (Math.Abs(leftLength - rightLength) == (medians.Length - 1) ||
                    Math.Abs((middle + index2) - (length - 1 - middle + (target.Length - index2))) == (medians.Length - 1))
                {
                    if (medians.Length == 1)
                    {
                        // middle is median of source and target
                        medians[0] = source[middle];
                        return 1;
                    }
                    else
                    {
                        medians[res++] = source[middle];
                        ++found;
                        if (res == medians.Length)
                        {
                            return found;
                        }
                    }
                }

                if (leftLength > rightLength)
                {
                    end = middle - 1;
                }
                else
                {
                    start = middle + 1;
                }
            }

            return found;
        }

        private int FindToLeftMost(int[] nums, int target, out bool multiple)
        {
            multiple = false;
            int start = 0, end = nums.Length - 1;
            int index = -1;
            while (start <= end)
            {
                int middle = start + (end - start) / 2;
                if (nums[middle] >= target)
                {
                    if (nums[middle] == target)
                    {
                        index = middle;
                        multiple = true;
                    }

                    end = middle - 1;
                }
                else
                {
                    start = middle + 1;
                }
            }

            return index != - 1 ? index : start;
        }

        private int FindToRightMost(int[] nums, int target)
        {
            int start = 0, end = nums.Length - 1;
            int index = -1;
            while (start <= end)
            {
                int middle = start + (end - start) / 2;
                if (nums[middle] > target)
                {
                    end = middle - 1;
                }
                else // nums[middle] <= target
                {
                    if (nums[middle] == target)
                    {
                        index = middle + 1;
                    }

                    start = middle + 1;
                }
            }

            return index != -1 ? index : start;
        }
    }
}
