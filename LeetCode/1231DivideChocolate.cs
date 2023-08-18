using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _1231DivideChocolate
    {
        public int MaximizeSweetness(int[] sweetness, int k)
        {
            int left = int.MaxValue, right = 0, peopleInTotal = k + 1;
            foreach (var s in sweetness)
            {
                right += s;
                if (left > s)
                    left = s;
            }

            right = right / peopleInTotal;
            while (left < right)
            {
                int mid = left + ((right - left + 1) >> 1);
                int index = 0, totalSweetness = 0, peopleHaveSweet = 0;
                while (index < sweetness.Length)
                {
                    totalSweetness += sweetness[index++];
                    if (totalSweetness >= mid)
                    {
                        ++peopleHaveSweet;
                        totalSweetness = 0;
                    }
                }

                if (peopleHaveSweet >= peopleInTotal)
                {
                    left = mid;
                }
                else
                {
                    right = mid - 1;
                }
            }

            // left == right
            return left;
        }

        public int MaximizeSweetnessV1(int[] sweetness, int k)
        {
            int left = int.MaxValue, right = 0, target = -1;
            foreach(var s in sweetness)
            {
                right += s;
                if (left > s)
                    left = s;
            }

            while (left <= right)
            {
                int mid = left + ((right - left) >> 1);
                int smallest = 0;
                int splits = this.Split(sweetness, mid, out smallest);

                if (splits > k + 1)
                {
                    // more splits means the mid might be too small
                    left = mid + 1;
                }
                else if (splits < k + 1)
                {
                    // less splits means the mid might be too big
                    right = mid - 1;
                }
                else // splits == k + 1
                {
                    if (smallest > mid)
                    {
                        // every split is bigger than mid
                        // try bigger mid greedily
                        left = mid + 1;
                    }
                    else if (smallest < mid)
                    {
                        // mid is too big, try smaller value
                        right = mid - 1;
                    }
                    else // smallest == mid
                    {
                        target = mid;
                        left = mid + 1;
                    }
                }
            }

            // left > right
            return target != -1 ? target : right;
        }

        private int Split(int[] sweetness, int target, out int smallest)
        {
            // can we split sweetness into parts and each of them is larger than target but at least one equals to target
            int index = 0, totalSweetness = 0, splits = 0;
            smallest = int.MaxValue;
            while (index < sweetness.Length)
            {
                totalSweetness += sweetness[index++];
                if (totalSweetness >= target)
                {
                    ++splits;
                    if (totalSweetness < smallest)
                        smallest = totalSweetness;
                    totalSweetness = 0;
                }
            }

            if (totalSweetness != 0)
            {
                ++splits;
                if (totalSweetness < smallest)
                    smallest = totalSweetness;
            }

            return splits;
        }
    }
}
