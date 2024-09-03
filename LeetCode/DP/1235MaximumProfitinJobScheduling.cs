using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _1235MaximumProfitinJobScheduling
    {
        public int JobScheduling(int[] startTimes, int[] endTimes, int[] profits)
        {
            int[][] jobs = new int[startTimes.Length][];
            for (int i = 0; i < startTimes.Length; ++i)
            {
                jobs[i] = new int[] { startTimes[i], endTimes[i], profits[i] };
            }

            Array.Sort(jobs, (j1, j2) => j1[0].CompareTo(j2[0]));

            //                         endTime, profit
            var pq = new PriorityQueue<(int, int), int>();
            int maxProfit = 0;
            for (int i = 0; i < jobs.Length; ++i)
            {
                while (pq.Count != 0 && jobs[i][0] >= pq.Peek().Item1)
                {
                    maxProfit = Math.Max(maxProfit, pq.Peek().Item2);
                    pq.Dequeue();
                }

                pq.Enqueue((jobs[i][1], maxProfit + jobs[i][2]), jobs[i][1]);
            }

            while (pq.Count != 0)
            {
                maxProfit = Math.Max(maxProfit, pq.Peek().Item2);
                pq.Dequeue();
            }

            return maxProfit;
        }

        public int JobSchedulingBottomUp(int[] startTime, int[] endTime, int[] profit)
        {
            int[] jobs = new int[startTime.Length];
            for (int i = 0; i < startTime.Length; ++i)
                jobs[i] = i;

            Array.Sort(jobs, (j1, j2) => startTime[j1].CompareTo(startTime[j2]));

            // dp[i] is the max profit to work on job at index i(of jobs array)
            int[] dp = new int[jobs.Length];
            for(int i = dp.Length - 1; i >= 0; --i)
            {
                // if do nothing
                int doNothing = i + 1 < jobs.Length ? dp[jobs[i + 1]] : 0;

                // try to do something
                int curProfit = profit[jobs[i]];
                int nextAvailableJob = BS(jobs, i, endTime[jobs[i]], startTime);
                if (nextAvailableJob != -1)
                    curProfit += dp[jobs[nextAvailableJob]];
                
                dp[jobs[i]] = Math.Max(doNothing, curProfit);
            }

            return dp.Max();
        }

        public int JobSchedulingTopDown(int[] startTime, int[] endTime, int[] profit)
        {
            int[] jobs = new int[startTime.Length];
            for (int i = 0; i < startTime.Length; ++i)
                jobs[i] = i;

            Array.Sort(jobs, (j1, j2) => startTime[j1].CompareTo(startTime[j2]));

            var cache = new int[startTime.Length];
            return MaxProfit(jobs, 0, startTime, endTime, profit, cache);
        }

        private static int MaxProfit(int[] jobs, int curJob, int[] startTimes, int[] endTimes, int[] profit, int[] cache)
        {
            if (curJob >= jobs.Length)
                return 0;

            if (cache[curJob] != 0)
                return cache[curJob];

            // do not schedule curJob
            int doNothing = MaxProfit(jobs, curJob + 1, startTimes, endTimes, profit, cache);

            // try to schedule curJob
            int nextJob = BS(jobs, curJob, endTimes[jobs[curJob]], startTimes);
            int doJob = profit[jobs[curJob]];
            if (nextJob != -1)
                doJob += MaxProfit(jobs, nextJob, startTimes, endTimes, profit, cache);

            return cache[curJob] = Math.Max(doNothing, doJob);
        }

        private static int BS(int[] jobs, int startFrom, int endTime, int[] startTimes)
        {
            // find the left-most jobs whose startTime >= endTime
            int left = startFrom + 1, right = jobs.Length - 1, index = -1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (startTimes[jobs[mid]] >= endTime)
                {
                    index = mid;
                    right = mid - 1;
                }
                else // startTimes[jobs[mid]] < endTime
                {
                    left = mid + 1;
                }
            }

            return index;
        }
    }
}
