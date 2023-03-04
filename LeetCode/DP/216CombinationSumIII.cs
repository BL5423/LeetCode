using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DP
{
    public class _216CombinationSumIII
    {
        const int K = 9;

        public IList<IList<int>> CombinationSum3(int k, int n)
        {
            var dp = new List<IList<int>>[K + 1, n + 1];
            for (int j = 0; j <= K; ++j)
            {
                dp[j, 0] = new List<IList<int>>(1);
                dp[j, 0].Add(new List<int>(0));
            }

            for (int i = 1; i <= K; ++i)
            {
                for(int target = n; target >= 0; --target)
                {
                    if (dp[i - 1, target] != null)
                    {
                        foreach (var comb in dp[i - 1, target])
                        {
                            if ((comb.Count > 0 && comb.Count < k && target != n) || (target == n && comb.Count == k))
                            {
                                if (dp[i, target] == null)
                                {
                                    dp[i, target] = new List<IList<int>>();
                                }

                                dp[i, target].Add(new List<int>(comb));
                            }
                        }
                    }

                    if (target >= i)
                    {
                        // since we iterate from n to 0, so any value(on the same row) less than target has NOT been calculated yet, thus we need to use the results from above row(i.e. i - 1).
                        // note, it is guaranteed that results from above row does not include i because i wasn't even considered when the results were calculated.
                        if (dp[i - 1, target - i] != null)
                        {
                            foreach (var comb in dp[i - 1, target - i])
                            {
                                if ((target != n && comb.Count < k) || (target == n && comb.Count == k - 1))
                                {
                                    var copy = new List<int>(comb);
                                    copy.Add(i);

                                    if (dp[i, target] == null)
                                    {
                                        dp[i, target] = new List<IList<int>>();
                                    }
                                    dp[i, target].Add(copy);
                                }
                            }
                        }
                    }
                }
            }

            return dp[K, n] != null ? dp[K, n] : new List<IList<int>>(0);
        }

        public IList<IList<int>> CombinationSum3_2DForward(int k, int n)
        {
            var dp = new List<IList<int>>[K + 1, n + 1];
            // We need to initialize the first column of table(i.e. dp[i, 0] where i is from 0 to K)
            // so that when we calculate dp[i, i] we can leverage either dp[i - 1, i - i] '+' i or dp[i, i - i] '+' i
            for (int j = 0; j <= K; ++j)
            {
                dp[j, 0] = new List<IList<int>>(1);
                dp[j, 0].Add(new List<int>(0));
            }

            for (int i = 1; i <= K; ++i)
            {
                for(int target = 0; target <= n; ++target)
                {
                    if (dp[i - 1, target] != null)
                    {
                        foreach (var comb in dp[i - 1, target])
                        {
                            if ((target != n && comb.Count > 0 && comb.Count <= k) || (target == n && comb.Count == k))
                            {
                                if (dp[i, target] == null)
                                    dp[i, target] = new List<IList<int>>();

                                dp[i, target].Add(new List<int>(comb));
                            }
                        }
                    }

                    if (target >= i)
                    {
                        // because we iterate from 0 to n, so when it comes to dp[i, target], any value(like target - i) that less than target has been calculated so that we can just leverage it
                        // the only thing we need to take care is dup: if dp[i, target] is based on dp[i, target - i] + i, then there will be two i's.
                        //if (dp[i, target - i] != null)
                        //{
                        //    foreach (var comb in dp[i, target - i])
                        //    {
                        //        if (comb.LastOrDefault() != i && ((target != n && comb.Count < k) || comb.Count == k - 1))
                        //        {
                        //            var copy = new List<int>(comb);
                        //            copy.Add(i);

                        //            if (dp[i, target] == null)
                        //                dp[i, target] = new List<IList<int>>();
                        //            dp[i, target].Add(copy);
                        //        }
                        //    }
                        //}

                        // if we iterate from 0 to target, then we can use results from above row(i.e. i - 1) to avoid dup i's.
                        if (dp[i - 1, target - i] != null)
                        {
                            foreach (var comb in dp[i - 1, target - i])
                            {
                                if ((target != n && comb.Count < k) || comb.Count == k - 1)
                                {
                                    var copy = new List<int>(comb);
                                    copy.Add(i);

                                    if (dp[i, target] == null)
                                        dp[i, target] = new List<IList<int>>();
                                    dp[i, target].Add(copy);
                                }
                            }
                        }
                    }
                }
            }

            return dp[K, n] != null ? dp[K, n] : new List<IList<int>>(0);
        }

        public IList<IList<int>> CombinationSum3_1DReverse(int k, int n)
        {
            var dp = new List<IList<int>>[n + 1];
            dp[0] = new List<IList<int>>(1);
            dp[0].Add(new List<int>(0));

            for(int i = 1; i <= K; ++i)
            {
                // iterate from n to i so that no duplicated i will be used
                // if we do it from i to n, then it is possible we used i for any target T between i and n,
                // and when we calculate T + i later, we could use i again, hence there would be 2 i's for T + i.
                for(int target = n; target >= i; --target)
                {
                    if (dp[target - i] != null)
                    {
                        List<IList<int>> combinations = null;
                        foreach(var comb in dp[target - i])
                        {
                            if (comb.Count < k && (target != n || comb.Count == k - 1))
                            {
                                var copy = new List<int>(comb);
                                copy.Add(i);
                                if (combinations == null)
                                    combinations = new List<IList<int>>(dp[target - i].Count);

                                combinations.Add(copy);
                            }
                        }

                        if (combinations != null)
                        {
                            if (dp[target] == null)
                                dp[target] = combinations;
                            else
                                dp[target].AddRange(combinations);
                        }
                    }
                }
            }

            return dp[n] != null ? dp[n] : new List<IList<int>>(0);
        }

        public IList<IList<int>> CombinationSum3_1D(int k, int n)
        {
            var res = new List<IList<int>>();
            var dp = new List<IList<int>>[n + 1];
            dp[0] = new List<IList<int>>(1);
            dp[0].Add(new List<int>(0));
            for (int i = 1; i <= K; ++i)
            {
                for (int target = i; target <= n; ++target)
                {
                    if (dp[target - i] != null)
                    {
                        var combinations = new List<IList<int>>();
                        foreach (var comb in dp[target - i])
                        {
                            if (comb.Count < k && comb.LastOrDefault() != i)
                            {
                                var copy = new List<int>(comb);
                                copy.Add(i);
                                combinations.Add(copy);
                            }
                        }

                        if (dp[target] == null)
                        {
                            dp[target] = combinations;
                        }
                        else
                        {
                            dp[target].AddRange(combinations);
                        }
                    }
                }
            }

            if (dp[n] != null)
            {
                foreach(var r in dp[n])
                {
                    if (r.Count == k)
                    {
                        res.Add(r);
                    }
                }
            }
            return res;
        }

        public IList<IList<int>> CombinationSum3_BackTracking(int k, int n)
        {
            var res = new List<IList<int>>();
            for(int i = 1; i <= K; ++i)
            {
                //DFS(i, k, n, new LinkedList<int>(), res);
                BFS_Iterative(i, k, n, res);
            }

            return res;
        }

        private void BFS_Iterative(int seed, int k, int targetSum, IList<IList<int>> res)
        {
            Queue<(int, LinkedList<int>)> queue = new Queue<(int, LinkedList<int>)>();
            var list = new LinkedList<int>();
            list.AddLast(seed);
            queue.Enqueue((seed, list));
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                int sum = head.Item2.Sum();
                if (sum == targetSum && head.Item2.Count == k)
                {
                    res.Add(head.Item2.ToList());
                }
                else if (sum < targetSum && head.Item2.Count < k && head.Item1 + 1 <= K)
                {
                    if (sum + head.Item1 + 1 <= targetSum)
                    {
                        var nextHeadWithNext = new LinkedList<int>(head.Item2);
                        nextHeadWithNext.AddLast(head.Item1 + 1);
                        queue.Enqueue((head.Item1 + 1, nextHeadWithNext));
                    }

                    var nextHeadWithoutNext = new LinkedList<int>(head.Item2);
                    queue.Enqueue((head.Item1 + 1, nextHeadWithoutNext));
                }
            }
        }

        private void DFS(int i, int k, int n, LinkedList<int> curRes, IList<IList<int>> res)
        {
            curRes.AddLast(i);
            int sum = curRes.Sum();
            if (!(sum > n || curRes.Count > k))
            {
                if (sum == n && k == curRes.Count)
                {
                    res.Add(curRes.ToList());
                }
                else
                {
                    for (int nextI = i + 1; nextI <= K && sum + nextI <= n && curRes.Count < k; ++nextI)
                    {
                        DFS(nextI, k, n, curRes, res);
                    }
                }
            }

            curRes.RemoveLast();
        }
    }
}
