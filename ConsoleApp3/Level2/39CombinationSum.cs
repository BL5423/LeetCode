using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _39CombinationSum
    {
        public IList<IList<int>> CombinationSum_2D(int[] candidates, int target)
        {
            var res = new List<IList<int>>[candidates.Length + 1, target + 1];
            for(int row = 0; row <= candidates.Length; ++row)
            {
                res[row, 0] = new List<IList<int>>(1);
                res[row, 0].Add(new List<int>(0));
            }

            for (int index = 1; index <= candidates.Length; ++index)
            {
                int candidate = candidates[index - 1];
                for(int t = 0; t <= target; ++t)
                {
                    if (res[index - 1, t] != null)
                    {
                        foreach(var comb in res[index - 1, t])
                        {
                            if (comb.Count > 0)
                            {
                                if (res[index, t] == null)
                                    res[index, t] = new List<IList<int>>();

                                res[index, t].Add(new List<int>(comb));
                            }
                        }
                    }

                    if (t >= candidate)
                    {
                        if (res[index, t - candidate] != null)
                        {
                            foreach(var comb in res[index, t- candidate])
                            {
                                var copy = new List<int>(comb);
                                copy.Add(candidate);

                                if (res[index, t] == null)
                                    res[index, t] = new List<IList<int>>();

                                res[index, t].Add(new List<int>(copy));
                            }
                        }
                    }
                }
            }

            return res[candidates.Length, target] != null ? res[candidates.Length, target] : new List<IList<int>>(0);
        }

        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            // dp[t] are all the combinations that sums to t
            var dp = new List<IList<int>>[target + 1];
            dp[0] = new List<IList<int>>();
            dp[0].Add(new List<int>());
            foreach(int candidate in candidates)
            {
                for(int t = candidate; t <= target; ++t)
                {
                    if (dp[t - candidate] != null)
                    {
                        var combinations = new List<IList<int>>();
                        foreach (var comb in dp[t - candidate])
                        {
                            // comb sums to (t - candidate)
                            var copy = new List<int>(comb);

                            // add candidate so that copy sums to t
                            copy.Add(candidate);

                            // combinations track every copy that sums to t, then we put merge combinations with dp[t]
                            combinations.Add(copy);
                        }

                        if (dp[t] == null)
                            dp[t] = combinations;
                        else
                            dp[t].AddRange(combinations);
                    }
                }
            }

            return dp[target] != null ? dp[target] : new List<IList<int>>(0);
        }

        public IList<IList<int>> CombinationSum_DPv1(int[] candidates, int target)
        {
            var finalRes = new List<IList<int>>[candidates.Length, target + 1];
            finalRes[0, 0] = new List<IList<int>>();
            finalRes[0, 0].Add(new List<int>());
            var res0 = new List<int>();
            for(int t = candidates[0]; t <= target; t+=candidates[0])
            {
                res0.Add(candidates[0]);
                finalRes[0, t] = new List<IList<int>>();
                finalRes[0, t].Add(new List<int>(res0));
            }

            for(int index = 1; index < candidates.Length; ++index)
            {
                finalRes[index, 0] = new List<IList<int>>();
                finalRes[index, 0].Add(new List<int>());

                for (int t = 0; t <= target; ++t)
                {
                    // index - 1, t
                    if (finalRes[index - 1, t] != null)
                    {
                        finalRes[index, t] = new List<IList<int>>(finalRes[index - 1, t]);
                    }

                    if (t >= candidates[index])
                    {
                        // index, t - candidates[index]
                        if (finalRes[index, t - candidates[index]] != null)
                        {
                            List<IList<int>> res2 = new List<IList<int>>();
                            foreach (var copied in finalRes[index, t - candidates[index]])
                            {
                                var r = new List<int>(copied);
                                r.Add(candidates[index]);
                                res2.Add(r);
                            }

                            if (finalRes[index, t] == null)
                                finalRes[index, t] = res2;
                            else
                                finalRes[index, t].AddRange(res2);
                        }
                    }
                }
            }

            var res = finalRes[candidates.Length - 1, target];
            return res != null ? res : new List<IList<int>>(0);
        }

        public IList<IList<int>> CombinationSum_BackTracking(int[] candidates, int target)
        {
            Array.Sort(candidates);

            var res = new List<IList<int>>();
            for (int index = 0; index < candidates.Length; ++index)
            {
                //CombinationRecursive(candidates, index, 0, res, new LinkedList<int>(), target);
                CombinationIterative(candidates, index, res, target);
            }

            return res;
        }

        private void CombinationRecursive(int[] candidates, int index, int sum, IList<IList<int>> res, LinkedList<int> curList, int target)
        {
            curList.AddLast(candidates[index]);
            sum += candidates[index];
            if (sum <= target)
            {
                if (sum == target)
                {
                    res.Add(curList.ToList());
                }
                else
                {
                    for (int next = index; next < candidates.Length; ++next)
                    {
                        CombinationRecursive(candidates, next, sum, res, curList, target);
                    }
                }
            }

            curList.RemoveLast();
        }

        private void CombinationIterative(int[] candidates, int index, IList<IList<int>> res, int target)
        {
            Stack<(int, LinkedList<int>)> stack = new Stack<(int, LinkedList<int>)>();
            var first = new LinkedList<int>();
            first.AddLast(candidates[index]);
            stack.Push((index, first));

            while (stack.Count > 0)
            {
                var curList = stack.Pop();
                int sum = curList.Item2.Sum();
                int curIndex = curList.Item1;
                if (sum <= target)
                {
                    if (sum == target)
                    {
                        res.Add(curList.Item2.ToList());
                    }
                    else
                    {
                        for (int nextIndex = curIndex; nextIndex < candidates.Length; ++nextIndex)
                        {
                            if (sum + candidates[nextIndex] <= target)
                            {
                                var list = new LinkedList<int>(curList.Item2);
                                list.AddLast(candidates[nextIndex]);

                                stack.Push((nextIndex, list));
                            }
                        }
                    }
                }
            }
        }
    }
}
