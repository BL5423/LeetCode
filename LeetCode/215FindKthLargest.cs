using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _215FindKthLargest
    {
        public int FindKthLargest(int[] nums, int k)
        {
            return Find(nums, 0, nums.Length - 1, nums.Length - k);
        }

        private int Find(int[] nums, int start, int end, int targetIndex)
        {
            while (true)
            {
                int left = start, right = end;
                //int pivotIndex = (start + end) / 2;
                int pivotIndex = Random.Shared.Next(left, right + 1);
                int pivot = nums[pivotIndex];

                // swap
                int temp = nums[right];
                nums[right] = nums[pivotIndex];
                nums[pivotIndex] = temp;
                pivotIndex = right;
                --right;

                // partition by pivot
                while (left <= right)
                {
                    if (nums[left] <= pivot)
                    {
                        ++left;
                    }
                    else // nums[left] > pivot
                    {
                        temp = nums[left];
                        nums[left] = nums[right];
                        nums[right--] = temp;
                    }
                }

                // left > right
                // restore
                temp = nums[left];
                nums[left] = pivot;
                nums[pivotIndex] = temp;

                pivotIndex = left;
                if (pivotIndex == targetIndex)
                    return pivot;
                else if (pivotIndex > targetIndex)
                //return Find(nums, start, pivotIndex - 1, targetIndex);
                {
                    end = pivotIndex - 1;
                    while (end - 1 > targetIndex && nums[end] == nums[end - 1] && end > start && nums[end - 1] >= pivot)
                        --end;
                }
                else // pivotIndex < targetIndex
                     //return Find(nums, pivotIndex + 1, end, targetIndex);
                {
                    start = pivotIndex + 1;
                    while (start + 1 < targetIndex && nums[start] == nums[start + 1] && end > start && nums[start + 1] <= pivot) // skip dup numbers while maintaining the condition, especially any number on the left of pivot should be equal to or smaller than pivot
                        ++start;
                }
            }

            return -1;
        }

        public int FindKthLargestCountSorting(int[] nums, int k)
        {
            int max = int.MinValue, min = int.MaxValue;
            var counters = new Dictionary<int, int>(nums.Length);
            foreach (var num in nums)
            {
                if (max < num)
                    max = num;
                if (min > num)
                    min = num;

                if (!counters.ContainsKey(num))
                {
                    counters.Add(num, 0);
                }

                ++counters[num];
            }

            int[] sortedNums = new int[max - min + 1];
            foreach (var pair in counters)
            {
                sortedNums[pair.Key - min] += pair.Value;
            }

            for (int i = max; i >= min; --i)
            {
                k -= sortedNums[i - min];
                if (k <= 0)
                    return i;
            }

            return int.MinValue;
        }

        public int FindKthLargestQuickSelect(int[] nums, int k)
        {
            int targetIndex = nums.Length - k;
            int left = 0, right = nums.Length - 1;
            while (true)
            {
                // always use the right most num as pivot
                // int pivotIndex = right;

                // randomly choose pivot
                int pivotIndex = left + (int)(Random.Shared.NextDouble() * (right - left + 1));
                this.Swap(nums, right, pivotIndex);
                pivotIndex = right;

                int start = left, end = right;
                for (int i = start; i < end; ++i)
                {
                    if (nums[i] <= nums[pivotIndex])
                    {
                        this.Swap(nums, i, start++);
                    }
                }

                this.Swap(nums, start, pivotIndex);
                pivotIndex = start;
                if (pivotIndex == targetIndex)
                    return nums[pivotIndex];
                else if (pivotIndex < targetIndex)
                    left = pivotIndex + 1;
                else // pivotIndex > targetIndex
                    right = pivotIndex - 1;
            }

            return -1;
        }

        public int FindKthLargestV5(int[] nums, int k)
        {
            int pivotIndex = -1, start = 0, end = nums.Length - 1;
            while (pivotIndex != k - 1)
            {
                pivotIndex = this.Split(nums, start, end);

                if (pivotIndex + 1 > k)
                {
                    // look into left part
                    end = pivotIndex - 1;
                }
                else if (pivotIndex + 1 < k)
                {
                    // turn to right
                    start = pivotIndex + 1;
                }
            }

            return nums[pivotIndex];
        }

        private int Split(int[] nums, int start, int end)
        {
            int pivot = nums[start], left = start + 1, right = end;
            while (left <= right)
            {
                if (nums[left] < pivot && nums[right] > pivot)
                {
                    Swap(nums, left++, right--);
                }
                else
                {
                    if (nums[left] >= pivot)
                    {
                        ++left;
                    }
                    if (nums[right] <= pivot)
                    {
                        --right;
                    }
                }
            }

            Swap(nums, start, right);
            return right;
        }

        private int SplitV1(int[] nums, int start, int end)
        {
            int pivot = nums[start], left = start, right = end;
            while (left < right)
            {
                if (nums[left] >= pivot)
                {
                    ++left;
                }
                else
                {
                    // do not change left which will be compare with pivot on next iteration since it holds a new number swapped from right
                    Swap(nums, left, right--);
                }
            }

            if (nums[left] >= pivot)
            {
                Swap(nums, left, start);
                return left;
            }
            else if (left > 0 && left - 1 >= start && nums[left - 1] >= pivot)
            {
                Swap(nums, left - 1, start);
                return left - 1;
            }

            return start;
        }

        public int FindKthLargestV4(int[] nums, int k)
        {
            var minHeap = new MinHeap(nums.Length);
            foreach(int num in nums)
                minHeap.Push(num);

            int i = nums.Length - k;
            while (i-- > 0)
                minHeap.Pop();

            return minHeap.Peek();
        }

        public int FindKthLargestV3(int[] nums, int k)
        {
            int left = 0, right = nums.Length - 1;
            while (left <= right)
            {
                int pivotIndex = left + ((right - left) >> 1);
                int pivot = nums[pivotIndex];
                Swap(nums, pivotIndex, right);
                int start = left;
                for(int i = start; i < right; ++i)
                {
                    if (nums[i] >= pivot)
                    {
                        Swap(nums, i, start++);
                    }
                }
                Swap(nums, right, start);

                if (start == k - 1)
                {
                    return nums[start];
                }
                else if (start > k - 1)
                {
                    right = start - 1;
                }
                else
                {
                    left = start + 1;
                }
            }

            return -1;
        }

        public int FindKthLargestV2(int[] nums, int k)
        {
            // randomly select a pivot to
            // split the input nums into 3 parts
            // part1 items > pivot
            // part2 items == pivot
            // part3 item < pivot
            // if k is among part1, then only look into part1
            // if k is among part3, then only look into part3
            // else k is among part2, then return first item in part2, exit
            // repeat above steps

            int left = 0, right = nums.Length - 1;
            while (left <= right)
            {
                int pivotNum = nums[new Random((int)DateTime.Now.Ticks).Next(left, right + 1)];
                int start = left, middle = left;
                int end = right;
                while (middle <= end)
                {
                    if (nums[middle] > pivotNum)
                    {
                        // if start == middle, nothing changes
                        // if start < middle
                        //      then start must point to an element that equals to pivotNum, so swap that element to middle is ok
                        //      all we need to do is moving middle to next position.
                        // start will not move ahead of middle in any case because middle always get moved forward if any element in middle equals to pivotNum.
                        Swap(nums, start++, middle++);
                        //if (start > middle)
                        //    middle = start;
                    }
                    else if (nums[middle] < pivotNum)
                    {
                        // After swap element at position end, we don't know the relationship between element in middle(comes from end) and pivotNum
                        // so we only move end one step ahead and leave middle unchanged
                        Swap(nums, end--, middle);
                    }
                    else
                    {
                        ++middle;
                    }
                }
                //Console.WriteLine("pivotNum: {0}, nums: {1}", pivotNum, string.Join(" ", Print(nums, left, right)));

                int leftLength = start - left;
                int middleLength = middle - start;
                //Console.WriteLine("left part: {0}, middle part: {1}, right part: {2}", 
                //    string.Join(" ", Print(nums, left, start - 1)),
                //    string.Join(" ", Print(nums, start, middle - 1)),
                //    string.Join(" ", Print(nums, middle, right)));
                if (k <= leftLength)
                {
                    //Console.WriteLine("go to left part, k: {0}", k);
                    right = start - 1;
                }
                else if (k > leftLength + middleLength)
                {
                    left = middle;
                    k -= (leftLength + middleLength);

                    //Console.WriteLine("go to right part, k: {0}", k);
                }
                else
                {
                    //Console.WriteLine("return kth number, k: {0}", k);
                    return nums[start];
                }
            }

            return -1;
        }

        public void Swap(int[] nums, int index1, int index2)
        {
            int num = nums[index1];
            nums[index1] = nums[index2];
            nums[index2] = num;
        }
    }
}
