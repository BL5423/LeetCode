using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _698PartitiontoKEqualSumSubsets
    {
        public bool CanPartitionKSubsets_BottomUp(int[] nums, int k)
        {
            int sum = nums.Sum(), target = sum / k;
            if (target * k != sum)
                return false;

            int totalCombinations = (1 << nums.Length);
            int[] subsetSums = new int[totalCombinations];
            for(int mask = 0; mask < totalCombinations; ++mask) 
            {
                subsetSums[mask] = -1; // -1 means has not resolved yet
            }

            subsetSums[0] = 0; // nothing is picked yet
            for(int mask = 0; mask < totalCombinations; ++mask)
            {
                if (subsetSums[mask] == -1) // mask has not been resolved yet
                    continue;

                for(int i = 0; i < nums.Length; ++i)
                {
                    if ((mask & (1 << i)) == 0 && nums[i] + subsetSums[mask] <= target)
                    {
                        subsetSums[mask | (1 << i)] = (nums[i] + subsetSums[mask]) % target;// mod target since subsetSums[mask] is the container of all subsets(up to k) for mask
                    }
                }

                if (subsetSums[totalCombinations - 1] == 0)
                    return true;
            }

            // 0 means all the sums of subsets equal to target
            return subsetSums[totalCombinations - 1] == 0;
        }

        public bool CanPartitionKSubsets_Backtrack_Memoization(int[] nums, int k)
        {
            int sum = nums.Sum(), target = sum / k;
            if (target * k != sum)
                return false;

            return CanPartitionKSubsets(nums, 0, k, 0, target, 0, 0);
        }

        private Dictionary<int, bool> cache = new Dictionary<int, bool>();

        private bool CanPartitionKSubsets(int[] nums, int index, int k, int mask, int target, int sumSoFar, int subsetsFound)
        {
            if (subsetsFound == k - 1)
                return true;

            if (sumSoFar > target)
                return false;

            if (cache.TryGetValue(mask, out bool value))
                return value;

            bool possible = false;
            if (target == sumSoFar)
            {
                // found a new subset, start all over from the first number
                possible = CanPartitionKSubsets(nums, 0, k, mask, target, 0, subsetsFound + 1);
            }
            else
            {
                for (int i = index; i < nums.Length && !possible; ++i)
                {
                    if (0 == ((mask >> i) & 1))
                    {
                        mask |= (1 << i);
                        possible |= CanPartitionKSubsets(nums, i + 1, k, mask, target, sumSoFar + nums[i], subsetsFound);

                        mask &= (mask ^ (1 << i));
                    }
                }
            }

            cache.TryAdd(mask, possible);
            return possible;
        }

        public bool CanPartitionKSubsets_Backtrack(int[] nums, int k)
        {
            int sum = nums.Sum(), target = sum / k;
            if (target * k != sum)
                return false;

            // sort in descenting so that branches will be reduced
            Array.Sort(nums, (a, b) => b - a);
            var used = new bool[nums.Length];
            return CanPartitionKSubsets(nums, 0, k, used, target, 0, 0);
        }

        private bool CanPartitionKSubsets(int[] nums, int index, int k, bool[] used, int target, int sumSoFar, int subsetsFound)
        {
            if (subsetsFound == k - 1)
                return true;            

            if (sumSoFar > target)
                return false;

            bool possible = false;
            if (target == sumSoFar)
            {
                // found a new subset, start all over from the first number
                possible = CanPartitionKSubsets(nums, 0, k, used, target, 0, subsetsFound + 1);
            }
            else
            {
                for (int i = index; i < nums.Length && !possible; ++i)
                {
                    if (used[i] == false)
                    {
                        used[i] = true;
                        possible |= CanPartitionKSubsets(nums, i + 1, k, used, target, sumSoFar + nums[i], subsetsFound);

                        used[i] = false;
                    }
                }
            }

            return possible;
        }
    }
}
