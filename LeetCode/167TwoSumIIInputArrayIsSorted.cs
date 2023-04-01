using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _167TwoSumIIInputArrayIsSorted
    {
        public int[] TwoSum(int[] numbers, int target)
        {
            int left = 0, right = numbers.Length - 1;
            int[] res = new int[2];
            while (left < right)
            {
                int sum = numbers[left] + numbers[right];
                if (sum == target)
                {
                    res[0] = left + 1;
                    res[1] = right + 1;
                    break;
                }
                else
                {
                    int mid = left + ((right - left) >> 1);
                    if (sum > target)
                    {
                        // try move right towards mid
                        if (numbers[left] + numbers[mid] >= target)
                            right = mid;
                        else
                            --right;
                    }
                    else // sum < target
                    {
                        // try move left towards mid
                        if (numbers[mid] + numbers[right] <= target)
                            left = mid;
                        else
                            ++left;
                    }
                }
            }

            return res;
        }

        public int[] TwoSumV3(int[] numbers, int target)
        {
            int left = 0, right = numbers.Length - 1;
            int[] res = new int[2];
            while (left < right)
            {
                int sum = numbers[left] + numbers[right];
                if (sum == target)
                {
                    res[0] = left + 1;
                    res[1] = right + 1;
                    break;
                }
                else if (sum > target)
                    --right;
                else // sum < target
                    ++left;
            }

            return res;
        }

        public int[] TwoSumV2(int[] numbers, int target)
        {
            int[] res = new int[2];
            Dictionary<int, int> seen = new Dictionary<int, int>(numbers.Length);
            for (int i = 0; i < numbers.Length; ++i)
            {
                int num = target - numbers[i];
                if (seen.TryGetValue(num, out int index))
                {
                    res[0] = index + 1;
                    res[1] = i + 1;
                    break;
                }

                if (!seen.ContainsKey(numbers[i]))
                    seen.Add(numbers[i], i);
            }

            return res;
        }

        public int[] TwoSumV1(int[] numbers, int target)
        {
            int[] res = new int[2];
            bool found = false;
            for (int i = 0; !found && i < numbers.Length; ++i)
            {
                int num = target - numbers[i];
                int left = i + 1, right = numbers.Length - 1;
                while (left <= right)
                {
                    int mid = left + ((right - left) >> 1);
                    if (numbers[mid] == num)
                    {
                        res[0] = i + 1;
                        res[1] = mid + 1;
                        found = true;
                        break;
                    }
                    else if (numbers[mid] > num)
                        right = mid - 1;
                    else // numbers[mid] < num
                        left = mid + 1;
                }
            }

            return res;
        }
    }
}
