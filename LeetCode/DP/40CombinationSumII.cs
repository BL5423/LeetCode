using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _40CombinationSumII
    {
        public IList<IList<int>> CombinationSum2_2DBackwards(int[] candidates, int targetSum)
        {
            var dp = new List<IList<int>>[candidates.Length + 1, targetSum + 1];
            HashSet<string> seen = new HashSet<string>();
            for (int row = 0; row <= candidates.Length; ++row)
            {
                dp[row, 0] = new List<IList<int>>(1);
                dp[row, 0].Add(new List<int>(0));
            }
            Array.Sort(candidates);

            for (int index = 1; index <= candidates.Length; ++index)
            {
                int candidate = candidates[index - 1];
                for (int target = targetSum; target >= 0; --target)
                {
                    if (dp[index - 1, target] != null)
                    {
                        foreach (var comb in dp[index - 1, target])
                        {
                            if (comb.Count > 0)
                            {
                                if (dp[index, target] == null)
                                {
                                    dp[index, target] = new List<IList<int>>();
                                }

                                dp[index, target].Add(new List<int>(comb));
                            }
                        }
                    }

                    if (target >= candidate && dp[index - 1, target - candidate] != null)
                    {
                        foreach (var comb in dp[index - 1, target - candidate])
                        {
                            var str = string.Concat(string.Join(" ", comb), " ", candidate);
                            if (seen.Add(str))
                            {
                                var copy = new List<int>(comb);
                                copy.Add(candidate);
                                if (dp[index, target] == null)
                                {
                                    dp[index, target] = new List<IList<int>>();
                                }

                                dp[index, target].Add(copy);
                            }
                        }
                    }
                }
            }

            return dp[candidates.Length, targetSum] != null ? dp[candidates.Length, targetSum] : new List<IList<int>>(0);
        }

        public IList<IList<int>> CombinationSum2_2DForwards(int[] candidates, int targetSum)
        {
            var dp = new List<IList<int>>[candidates.Length + 1, targetSum + 1];
            HashSet<string> seen = new HashSet<string>();
            for(int row = 0; row <= candidates.Length; ++row)
            {
                dp[row, 0] = new List<IList<int>>(1);
                dp[row, 0].Add(new List<int>(0));
            }
            Array.Sort(candidates);

            for(int index = 1; index <= candidates.Length; ++index)
            {
                int candidate = candidates[index - 1];
                for(int target = 0; target <= targetSum; ++target)
                {
                    if (dp[index - 1, target] != null)
                    {
                        foreach (var comb in dp[index - 1, target])
                        {
                            if (comb.Count > 0)
                            {
                                if (dp[index, target] == null)
                                {
                                    dp[index, target] = new List<IList<int>>();
                                }

                                dp[index, target].Add(new List<int>(comb));
                            }
                        }
                    }

                    if (target >= candidate && dp[index - 1, target - candidate] != null)
                    {
                        foreach(var comb in dp[index - 1, target - candidate])
                        {
                            var str = string.Concat(string.Join(" ", comb), " ", candidate);
                            if (seen.Add(str))
                            {
                                var copy = new List<int>(comb);
                                copy.Add(candidate);
                                if (dp[index, target] == null)
                                {
                                    dp[index, target] = new List<IList<int>>();
                                }

                                dp[index, target].Add(copy);
                            }
                        }
                    }
                }
            }

            return dp[candidates.Length, targetSum] != null ? dp[candidates.Length, targetSum] : new List<IList<int>>(0);
        }

        public IList<IList<int>> CombinationSum2_1DBackwards(int[] candidates, int target)
        {
            var dp = new List<IList<int>>[target + 1];
            dp[0] = new List<IList<int>>(1);
            dp[0].Add(new List<int>(0));
            Array.Sort(candidates);
            HashSet<string> seen = new HashSet<string>();

            foreach (int candidate in candidates)
            {
                // For Knapsack, we usually loop from candidate(smaller) to target(bigger), thus we can use candidate multiple times accumulatively:
                // Say for value V, we use candidate once, and then for value V + candidate(assume it is less than target), we can use candidate again.
                // However, given this problem, we need to avoid dups(each candidate is used only once).
                // By iterating from target to candidate, we can make sure for any given value V, dp[i-1][V] and dp[i][V-candidate] will not have the candidate.
                // This is because candidate(n[i]) is not visible to dp[i-1][V] when dp[i-1][V] was computed, and dp[i][V-candidate] has not been computed yet.
                // Actually, value of d[i][V - candidate] is still dp[i-1][V - candidate] since we use 1D array.
                for (int value = target; value >= candidate; --value)
                {
                    if (dp[value - candidate] != null)
                    {
                        List<IList<int>> combinations = null;
                        foreach (var comb in dp[value - candidate])
                        {
                            var copy = new List<int>(comb);
                            copy.Add(candidate);
                            if (seen.Add(string.Join(" ", copy)))
                            {
                                if (combinations == null)
                                    combinations = new List<IList<int>>(dp[value - candidate].Count);
                                combinations.Add(copy);
                            }
                        }

                        if (combinations != null)
                        {
                            if (dp[value] != null)
                                dp[value].AddRange(combinations);
                            else
                                dp[value] = combinations;
                        }
                    }
                }
            }

            if (dp[target] != null)
            {
                return dp[target];
            }

            return new List<IList<int>>(0);
        }

        public IList<IList<int>> CombinationSum2_1DForwards(int[] candidates, int target)
        {
            var res = new IList<IList<(int, int)>>[target + 1];
            res[0] = new List<IList<(int, int)>>(0);
            res[0].Add(new List<(int, int)>(0));
            Array.Sort(candidates);
            HashSet<string> seen = new HashSet<string>();

            for(int index = 0; index < candidates.Length; ++index)
            {
                int candidate = candidates[index];
                for(int t = candidate; t <= target; ++t)
                {
                    if (res[t - candidate] != null)
                    {
                        foreach(var comb in res[t - candidate])
                        {
                            // use index of each candidate to dedup
                            if (comb.Count == 0 || comb.Last().Item1 != index)
                            {
                                if (seen.Add(string.Concat(string.Join(" ", comb.Select(c => c.Item2)), " ", candidate)))
                                {
                                    var copy = new List<(int, int)>(comb);
                                    copy.Add((index, candidate));

                                    if (res[t] == null)
                                        res[t] = new List<IList<(int, int)>>();

                                    res[t].Add(copy);
                                }
                            }
                        }
                    }
                }
            }

            if (res[target] != null)
            {
                var ret = new List<IList<int>>(res[target].Count);
                foreach(var comb in res[target])
                {
                    var copy = comb.Select(item => item.Item2).ToList();
                    ret.Add(copy);
                }

                return ret;
            }

            return new List<IList<int>>(0);
        }
    }
}
