using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _1477FindTwoNon_overlappingSub_arraysEachWithTargetSum
    {
        public int MinSumOfLengths(int[] arr, int target)
        {
            int res = int.MaxValue, sum = 0, best = int.MaxValue;
            // bestTill[i] indicates the min length of subarray among arr[0, i] that has sum equal to target
            int[] bestTill = new int[arr.Length];
            Dictionary<int, int> prevSums = new Dictionary<int, int>(arr.Length);
            prevSums[0] = -1;
            for(int index = 0; index < arr.Length; ++index)
            {
                // initialize
                bestTill[index] = int.MaxValue;

                sum += arr[index];
                int prevSum = sum - target;
                if (prevSums.TryGetValue(prevSum, out int prevIndex))
                {
                    best = Math.Min(best, index - prevIndex);

                    // use prevIndex to find the previous result that has no overlap with [prevIndex + 1, index]
                    if (prevIndex >= 0 && bestTill[prevIndex] != int.MaxValue)
                    {
                        res = Math.Min(res, bestTill[prevIndex] + index - prevIndex);
                    }
                }

                bestTill[index] = best;
                prevSums[sum] = index;
            }

            return res != int.MaxValue ? res : -1;
        }

        public int MinSumOfLengths_PrefixSum_TwoPasses(int[] arr, int target)
        {
            if (arr.Length < 2)
                return -1;

            // prefix[i] tracks the min length of subarraies that sums to target of arr[0, i]
            int[] prefix = new int[arr.Length];
            Dictionary<int, int> prevSums = new Dictionary<int, int>(arr.Length);
            prevSums[0] = -1;
            int sum = 0, minLength = int.MaxValue;
            for(int index = 0; index < arr.Length; ++index)
            {
                sum += arr[index];
                if (prevSums.TryGetValue(sum - target, out int prevIndex))
                {
                    minLength = Math.Min(minLength, index - prevIndex);
                    prefix[index] = minLength;
                }
                else if (index > 0)
                {
                    prefix[index] = prefix[index - 1];
                }

                prevSums[sum] = index;
            }

            // suffix[i] tracks the min length of subarraies that sums to target of arr[i, arr.Length - 1]
            int[] suffix = new int[arr.Length];
            prevSums.Clear();
            prevSums[0] = arr.Length;
            sum = 0;
            minLength = int.MaxValue;
            for(int index = arr.Length - 1; index >= 0; --index)
            {
                sum += arr[index];
                if (prevSums.TryGetValue(sum - target, out int prevIndex))
                {
                    minLength = Math.Min(minLength, prevIndex - index);
                    suffix[index] = minLength;
                }
                else if (index < arr.Length - 1)
                {
                    suffix[index] = suffix[index + 1];
                }

                prevSums[sum] = index;
            }

            minLength = int.MaxValue;
            for(int index = 1; index < arr.Length; ++index)
            {
                if (prefix[index - 1] != 0 && suffix[index] != 0)
                {
                    minLength = Math.Min(minLength, prefix[index - 1] + suffix[index]);
                }
            }

            return minLength != int.MaxValue ? minLength : -1;
        }

        public int MinSumOfLengths_PositiveNumbers(int[] arr, int target)
        {
            if (arr.Length < 2)
                return -1;

            int[] prefix = new int[arr.Length + 1];
            int sum = 0;
            int left = 0, right = 0;
            int minLength = int.MaxValue;
            while (right < arr.Length)
            {
                sum += arr[right++];

                // this approach only works for positive numbers
                while (sum > target && left <= right)
                {
                    sum -= arr[left];
                    ++left;
                }

                if (sum == target)
                {
                    // found a potential shorter subarray
                    prefix[right] = Math.Min(minLength, right - left);
                    minLength = Math.Min(minLength, prefix[right]);
                }
                else
                {
                    // if not found, use the previous result
                    prefix[right] = prefix[right - 1];
                }
            }

            int[] suffix = new int[arr.Length + 1];
            minLength = int.MaxValue;
            sum = 0;
            left = arr.Length - 1;
            right = left;
            while (left >= 0)
            {
                sum += arr[left];

                // this approach only works for positive numbers
                while (sum > target && right >= left)
                {
                    sum -= arr[right];
                    --right;
                }

                if (sum == target)
                {
                    // found a potential shorter subarray
                    suffix[left] = Math.Min(minLength, right - left + 1);
                    minLength = Math.Min(minLength, suffix[left]);
                }
                else
                {
                    // if not found, use the previous result
                    suffix[left] = suffix[left + 1];
                }

                --left;
            }

            minLength = int.MaxValue;
            for(int index = 1; index < arr.Length; ++index)
            {
                if (prefix[index] != 0 && suffix[index] != 0)
                {
                    minLength = Math.Min(prefix[index] + suffix[index], minLength);
                }
            }

            return minLength != int.MaxValue ? minLength : -1;
        }
    }
}
