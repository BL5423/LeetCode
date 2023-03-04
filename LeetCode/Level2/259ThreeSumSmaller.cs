using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _259ThreeSumSmaller
    {
        public int ThreeSumSmaller(int[] nums, int target)
        {
            if (nums.Length < 3)
                return 0;

            Array.Sort(nums);
            int res = 0;
            for (int index = 0; index < nums.Length - 2; ++index)
            {
                int anchor = nums[index];
                int subTarget = target - anchor;
                int low = index + 1, high = nums.Length - 1;
                while (low < high)
                {
                    if (nums[low] + nums[high] >= subTarget)
                    {
                        --high;
                    }
                    else
                    {
                        res += (high - low);
                        ++low;
                    }
                }
            }

            return res;
        }

        public int ThreeSumSmallerBinarySearch(int[] nums, int target)
        {
            if (nums.Length < 3)
                return 0;

            Array.Sort(nums);
            int res = 0;
            for (int index = 0; index < nums.Length - 2; ++index)
            {
                int anchor = nums[index];
                int subTarget = target - anchor;
                int low = index + 1, high = nums.Length - 1;
                while (low < high)
                {
                    // binary search to find the maximal num that nums[low] + num < subTarget
                    int left = low + 1, end = high;
                    while (left <= end)
                    {
                        int middle = left + ((end - left) >> 1);
                        if (nums[middle] + nums[low] < subTarget)
                        {
                            high = middle;
                            left = middle + 1;
                        }
                        else
                        {
                            end = middle - 1;
                        }
                    }

                    if (nums[low] + nums[high] < subTarget)
                    {
                        res += (high - low);
                    }

                    ++low;
                }
            }

            return res;
        }

        private int Combinations(int n, int k)
        {
            int ns = 1, ks = 1, n_k = 1;
            for(int i = 1; i <= n; ++i)
            {
                ns *= i;
                if (i == k)
                    ks = ns;
                if (i == (n - k))
                    n_k = ns;
            }

            return ns / (ks * n_k);
        }
    }
}
