using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _658FindKClosestElements
    {
        public IList<int> FindClosestElementsV2(int[] arr, int k, int x)
        {
            int left = 0, right = arr.Length - 1;
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);         
                if (arr[mid] >= x)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }

            // right is the index of the first num in arr that is larger or equal to x
            if (right == 0)
            {
                var res = new List<int>(k);
                for (int i = 0; i < k && i < arr.Length; ++i)
                    res.Add(arr[i]);

                return res;
            }
            else if (right == arr.Length - 1 && arr[right] < x)
            {
                var res = new List<int>(k);
                for (int i = Math.Max(0, arr.Length - k); res.Count != k && i < arr.Length; ++i)
                    res.Add(arr[i]);

                return res;
            }
            else
            {
                var res = new LinkedList<int>();
                int startIndex = right, endIndex = right + 1;
                if (arr[right] != x)
                {
                    startIndex = right - 1;
                    endIndex = right;
                }

                while (res.Count != k && res.Count < arr.Length)
                {
                    int startVal = -100000, endVal = 100000;
                    if (startIndex >= 0)
                    {
                        startVal = arr[startIndex];
                    }
                    if (endIndex < arr.Length)
                    {
                        endVal = arr[endIndex];
                    }

                    if (x - startVal <= endVal - x)
                    {
                        res.AddFirst(startVal);
                        --startIndex;
                    }
                    else
                    {
                        res.AddLast(endVal);
                        ++endIndex;
                    }
                }

                return res.ToList();
            }
        }

        public IList<int> FindClosestElementsBinarySearch(int[] arr, int k, int x)
        {
            int start = 0, end = arr.Length - k;
            while (start < end)
            {
                int middle = (start + end) / 2;
                if (x * 2 > arr[middle + k] + arr[middle])
                {
                    // if x is more closer to middle + k, then we lowest bound/index we can move to left is mid + 1, aka k elemets left from middle + k
                    start = middle + 1;
                }
                else
                {
                    // if x is more closer to middle, then middle might be one of the k closest elements, so move end to middle
                    end = middle;
                }
            }

            // start will be the lower bound of the k elements
            var res = new int[k];
            Array.Copy(arr, start, res, 0, k);
            return res.ToList();
        }

        public IList<int> FindClosestElementsV1(int[] arr, int k, int x)
        {
            // Find index of x or its closet in the array
            //var index1 = Array.BinarySearch(arr, x);
            var index = this.BinarySearch(arr, x);
            //if (index1 < 0)
            //    index1 = ~index1;
            //Console.WriteLine("index1: {0}, index: {1}.", index1, index);
            //if (index == arr.Length || arr[index] != x)
            //{
            //    --index;
            //}

            LinkedList<int> res = new LinkedList<int>();
            int left = index;
            int right = left + 1;
            while (res.Count < k)
            {
                int leftValue = int.MinValue;
                if (left >= 0)
                {
                    leftValue = arr[left];
                    if (right >= arr.Length)
                    {
                        res.AddFirst(leftValue);
                        --left;
                        continue;
                    }
                }
                int rightValue = int.MaxValue;
                if (right < arr.Length)
                {
                    rightValue = arr[right];
                    if (left < 0)
                    {
                        res.AddLast(rightValue);
                        ++right;
                        continue;
                    }
                }

                int leftDiff = Math.Abs(x - leftValue);
                int rightDiff = Math.Abs(rightValue - x);
                if (leftDiff < rightDiff)
                {
                    res.AddFirst(leftValue);
                    --left;
                }
                else if (leftDiff > rightDiff)
                {
                    res.AddLast(rightValue);
                    ++right;
                }
                else
                {
                    if (leftValue < rightValue)
                    {
                        res.AddFirst(leftValue);
                        --left;
                    }
                    else
                    {
                        res.AddLast(rightValue);
                        ++right;
                    }
                }
            }

            return res.ToList();
        }

        private int BinarySearch(int[] arr, int value)
        {
            int start = 0, end = arr.Length - 1;
            while (start <= end)
            {
                int middle = (start + end) / 2;
                if (arr[middle] > value)
                {
                    end = middle - 1;
                }
                else if (arr[middle] < value)
                {
                    start = middle + 1;
                }
                else
                {
                    return middle;
                }
            }

            return end;
        }
    }
}
