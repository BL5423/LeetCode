using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _548SplitArraywithEqualSum
    {
        public bool SplitArray(int[] nums)
        {
            if (nums.Length < 7)
                return false;

            // sums[i] is the sum of nums[0] to nums[i]
            int[] sums = new int[nums.Length];
            sums[0] = nums[0];
            for(int i = 1; i < nums.Length; ++i)
            {
                sums[i] = nums[i] + sums[i - 1];
            }

            HashSet<(int, int)> js = new HashSet<(int, int)>();
            for(int j = nums.Length - 4; j >= 3; --j)
            {
                for(int i = 1; i <= j - 2; ++i)
                {
                    if (sums[i - 1] == sums[j - 1] - sums[i])
                    {
                        // found a potential i and j
                        js.Add((i ,j));
                    }
                }
            }

            foreach(var ij in js)
            {
                for(int k = nums.Length - 2; k >= ij.Item2 + 2; --k)
                {
                    int sumAfterK = sums[nums.Length - 1] - sums[k];
                    if (sumAfterK == sums[k - 1] - sums[ij.Item2] &&
                        sumAfterK == sums[ij.Item2 - 1] - sums[ij.Item1])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool SplitArrayV1(int[] nums)
        {
            if (nums.Length < 5)
                return false;

            // find sums of all sub arrays
            Dictionary<int, List<(int, int)>> counts = new Dictionary<int, List<(int, int)>>();
            for(int i = 0; i < nums.Length; ++i)
            {
                int sum = 0;
                for(int j = i; j < nums.Length; ++j)
                {
                    sum += nums[j];

                    if (!counts.TryGetValue(sum, out List<(int, int)> ranges))
                    {
                        ranges = new List<(int, int)>();
                        counts[sum] = ranges;
                    }
                                            
                    ranges.Add((i, j));
                }
            }

            // the candidates are only those sums appear at least 4 times
            var candidates = counts.Where(kv => kv.Value.Count >= 4 && kv.Value.First().Item1 == 0 && kv.Value.Last().Item2 == nums.Length - 1);            
            foreach(var candidate in candidates)
            {
                HashSet<int> js = new HashSet<int>();
                HashSet<int> hs = new HashSet<int>();
                for (int i = 0; i <= candidate.Value.Count - 3; ++i)
                {
                    var firstIndex = candidate.Value[i];
                    if (firstIndex.Item1 != 0)
                        break;

                    for (int j = i + 1; j <= candidate.Value.Count - 2; ++j)
                    {
                        var secIndex = candidate.Value[j];
                        if (firstIndex.Item2 + 2 == secIndex.Item1)
                        {
                            // found a potential j
                            js.Add(secIndex.Item2 + 1);

                            /*
                            for (int k = j + 1; k <= candidate.Value.Count - 1; ++k)
                            {
                                var thridIndex = candidate.Value[k];
                                if (thridIndex.Item1 == secIndex.Item2 + 2)
                                {
                                    for (int h = k + 1; h < candidate.Value.Count; ++h)
                                    {
                                        var lastIndex = candidate.Value[h];
                                        if (lastIndex.Item1 == thridIndex.Item2 + 2 && lastIndex.Item2 == nums.Length - 1)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                            */
                        }
                    }
                }

                for (int k = candidate.Value.Count - 1; k >= 3; --k)
                {
                    var lastIndex = candidate.Value[k];
                    if (lastIndex.Item2 != nums.Length - 1)
                        continue;

                    for (int h = k - 1; h >= 2; --h)
                    {
                        var thirdIndex = candidate.Value[h];
                        if (lastIndex.Item1 == thirdIndex.Item2 + 2)
                        {
                            // found a potential h
                            hs.Add(thirdIndex.Item1 - 1);
                        }
                    }
                }

                foreach(var j in js)
                {
                    if (hs.Contains(j))
                        return true;
                }
            }

            return false;
        }
    }
}
