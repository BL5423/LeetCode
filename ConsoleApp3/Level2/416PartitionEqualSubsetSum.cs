using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _416PartitionEqualSubsetSum
    {
        public bool CanPartition1D_V2(int[] nums)
        {
            int sum = nums.Sum();
            if (sum % 2 != 0)
                return false;

            int targetSum = sum >> 1;
            bool[] dp = new bool[targetSum + 1];
            dp[0] = true;
            for(int index = 1; index <= nums.Length; ++index)
            {
                int num = nums[index - 1];
                for(int t = targetSum; t >= num; --t)
                {
                    dp[t] |= dp[t - num];
                }

                if (dp[targetSum])
                    return true;
            }

            return dp[targetSum];
        }

        public bool CanPartition2D_V2(int[] nums)
        {
            if (nums.Length <= 0)
                return false;
            int sum = nums.Sum();
            if ((sum & 1) == 1)
                return false;

            int targetSum = sum >> 1;
            bool[,] dp = new bool[nums.Length, targetSum + 1];
            if (nums[0] <= targetSum)
                dp[0, nums[0]] = true;

            for (int i = 0; i < nums.Length; ++i)
            {
                int num = nums[i];
                for (int k = 0; k <= targetSum; ++k)
                {
                    if (i > 0)
                    {
                        dp[i, k] |= dp[i - 1, k];
                        if (k >= num)
                        {
                            dp[i, k] |= dp[i - 1, k - num];
                        }
                    }
                }
            }

            return dp[nums.Length - 1, targetSum];
        }

        public bool CanPartition2D(int[] nums)
        {
            int sum = nums.Sum();
            if (sum % 2 != 0)
                return false;

            int targetSum = sum >> 1;
            bool[,] dp = new bool[nums.Length + 1, targetSum + 1];
            dp[0, 0] = true;
            for(int index = 1; index <= nums.Length; ++index)
            {
                int num = nums[index - 1];
                for(int t = 0; t <= targetSum; ++t)
                {
                    dp[index, t] |= dp[index - 1, t];
                    if (t >= num)
                    {
                        // index - 1 guarantees that dp[index - 1, t - num] does not include any result that uses nums[index])
                        // since when dp[index - 1, t - num] was calculated, nums[index] is not available at all(given the iterations on index)
                        dp[index, t] |= dp[index - 1, t - num];
                    }
                }

                if (dp[index, targetSum])
                    return true;
            }

            return dp[nums.Length, targetSum];
        }

        public bool CanPartition(int[] nums)
        {
            int sum = nums.Sum();
            if (sum % 2 == 1)
                return false;

            int targetSum = sum >> 1;
            bool[,] results = new bool[nums.Length, targetSum + 1];
            // for results[i, sum], it means if we can get sum from adding any elements from nums[0] to nums[i]. Given a num, it is either added or not added.
            results[0, 0] = true;
            if (nums[0] <= targetSum)
                results[0, nums[0]] = true;
            for(int index = 1; index < nums.Length; ++index)
            {
                for(int currentSum = 0; currentSum <= targetSum; ++currentSum)
                {
                    results[index, currentSum] = results[index - 1, currentSum];
                    if (currentSum >= nums[index])
                        results[index, currentSum] = results[index , currentSum] || results[index - 1, currentSum - nums[index]];
                }
            }

            return results[nums.Length - 1, targetSum];
        }

        public bool CanPartition1D(int[] nums)
        {
            // Bottom up
            int sum = nums.Sum();
            if (sum % 2 != 0)
                return false;

            int half = sum >> 1;
            bool[] resultsPrev = new bool[half + 1];
            bool[] results = new bool[half + 1];
            // base case
            resultsPrev[0] = true;
            foreach (var num in nums)
            {
                //for (int targetSum = num; targetSum <= half; ++targetSum)
                //for (int targetSum = half; targetSum >= num; --targetSum)
                
                //for (int targetSum = half; targetSum >= 0; --targetSum)
                for (int targetSum = 0; targetSum <= half; ++targetSum)
                {
                    if (targetSum >= num)
                        results[targetSum] = resultsPrev[targetSum - num] || resultsPrev[targetSum];
                    else
                        results[targetSum] = resultsPrev[targetSum];
                }

                var res = resultsPrev;
                resultsPrev = results;
                results = res;
            }

            return resultsPrev[half];
        }

        public bool CanPartitionBottomUp(int[] nums)
        {
            // Bottom up
            int sum = nums.Sum();
            if (sum % 2 != 0)
                return false;

            int half = sum >> 1;
            bool[,] results = new bool[nums.Length, half + 1];
            // base case
            results[0, 0] = true;
            for(int index = 1; index < nums.Length; ++index)
            {
                int prevNum = nums[index - 1];
                //for(int targetSum = 0; targetSum <= half; ++targetSum)
                for (int targetSum = half; targetSum >= 0; --targetSum)
                {
                    if (targetSum < prevNum)
                    {
                        results[index, targetSum] = results[index - 1, targetSum];
                    }
                    else
                    {
                        results[index, targetSum] = results[index - 1, targetSum] || results[index - 1, targetSum - prevNum];
                    }
                }
            }

            return results[nums.Length - 1, half];
        }

        public bool CanPartitionTopDown(int[] nums)
        {
            int sum = nums.Sum();
            if (sum % 2 != 0)
                return false;

            int half = sum >> 1;
            var results = new bool?[nums.Length, half + 1];
            return PartitionRecursively(nums, 0, targetSum: half, results);
        }

        private bool PartitionRecursively(int[] nums, int index, int targetSum, bool?[,] results)
        {
            if (index == nums.Length || targetSum < 0)
                return false;

            if (targetSum == 0)
                return true;

            if (results[index, targetSum] != null)
                return results[index, targetSum].Value;

            var result = PartitionRecursively(nums, index + 1, targetSum, results) ||
                         PartitionRecursively(nums, index + 1, targetSum - nums[index], results);

            results[index, targetSum] = result;
            return result;
        }

        public bool CanPartitionV1(int[] nums)
        {
            // exceed time limit
            int sum = nums.Sum();
            if (sum % 2 != 0)
                return false;

            int half = sum >> 1;
            Queue<(int, HashSet<int>)> queue = new Queue<(int, HashSet<int>)>();
            queue.Enqueue((half, new HashSet<int>()));
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                if (head.Item1 == 0)
                    return true;

                for(int index = 0; index < nums.Length; ++index)
                {
                    if (head.Item2.Contains(index))
                        continue;

                    head.Item2.Add(index);
                    int num = nums[index];
                    if (head.Item1 >= num)
                    {
                        queue.Enqueue((head.Item1 - num, new HashSet<int>(head.Item2.AsEnumerable())));
                    }
                }
            }

            return false;
        }
    }
}
