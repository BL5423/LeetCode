using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
{
    public class _1335MinimumDifficultyofaJobSchedule
    {
        public int MinDifficulty_Monotonic_Stack(int[] jobDifficulty, int totalDays)
        {
            int totalJobs = jobDifficulty.Length;
            if (totalDays > totalJobs)
            {
                return -1;
            }

            int[] difficultiesOnPrevDay = new int[totalJobs];
            int[] difficulitiesOnCurDay = new int[totalJobs];
            // initialize the results of prev day to something impossibly big
            for (int index = 0; index < totalJobs; index++)
            {
                difficultiesOnPrevDay[index] = 10000;
            }

            // iterate through the first day to last
            for (int day = 0; day < totalDays; ++day)
            {
                // use a stack to keep the jobs that are scheduled on current day
                Stack<int> jobsScheduledSoFar = new Stack<int>(totalJobs);

                // try to schedule all potential jobs for current day
                // note the jobIndex does not have to end at totalJobs since we always need to leave some jobs if it is not the last day
                for (int jobIndex = day; jobIndex < totalJobs - (totalDays - day - 1); ++jobIndex)
                //for(int jobIndex = day; jobIndex < totalJobs; ++jobIndex)
                {
                    // the initial difficulty is either the difficulty of job at jobIndex if it is day 1, or the diffiulties of prior day plush difficulty of job at jobIndex
                    difficulitiesOnCurDay[jobIndex] = jobIndex != 0 ? difficultiesOnPrevDay[jobIndex - 1] + jobDifficulty[jobIndex] : jobDifficulty[jobIndex];

                    // let's see if the difficulty can be updated with job at jobIndex if it is more/equally difficult
                    while (jobsScheduledSoFar.Count != 0 && jobDifficulty[jobsScheduledSoFar.Peek()] <= jobDifficulty[jobIndex])
                    {
                        int lastMostDifficultJobsIndex = jobsScheduledSoFar.Pop();

                        // try to update
                        difficulitiesOnCurDay[jobIndex] = Math.Min(difficulitiesOnCurDay[jobIndex],
                                                                   difficulitiesOnCurDay[lastMostDifficultJobsIndex] + jobDifficulty[jobIndex] - jobDifficulty[lastMostDifficultJobsIndex]);
                    }

                    if (jobsScheduledSoFar.Count != 0)
                    {
                        // if job at jobIndex is easier, then try to see if it is a better answer than the current last job for current day
                        difficulitiesOnCurDay[jobIndex] = Math.Min(difficulitiesOnCurDay[jobIndex], difficulitiesOnCurDay[jobsScheduledSoFar.Peek()]);
                    }

                    // let jobIndex be the index of last job for current day
                    jobsScheduledSoFar.Push(jobIndex);
                }

                int[] temp = difficulitiesOnCurDay;
                difficulitiesOnCurDay = difficultiesOnPrevDay;
                difficultiesOnPrevDay = temp;
            }

            return difficultiesOnPrevDay[totalJobs - 1];
        }

        public int MinDifficulty_BottomUp_1D(int[] jobDifficulty, int totalDays)
        {
            int totalJobs = jobDifficulty.Length;
            if (totalDays > totalJobs)
                return -1;

            // dp[i] represents the minimal difficulty to start with the i'th job on a particular day
            int[] dp = new int[totalJobs + 1];

            // base cases(of last day)
            // the jobIndex stops at totalDays - 1 because we need to leave *at least* these number of jobs for the days before the last day
            for (int job = totalJobs - 1; job >= totalDays - 1; --job)
                dp[job] = Math.Max(dp[job + 1], jobDifficulty[job]);
            
            // iterate backwards from the day before the last day to the first day
            for (int day = totalDays - 1; day >= 1; --day)
            {
                // this is the minimal number of jobs we need to leave for the prior days
                int daysAhead = day - 1;

                // this is the minimal number of jobs we can explore for current day
                int daysWeHave = totalDays - daysAhead;

                // start from the first job and last job that we can schedule on current day
                // since dp[i] of current day depends on dp[j] of next day, where j > i. In order to not overwrite dp[j]'s, we need to iterate i/firstJobIndexForCurDay increasingly.
                //for (int firstJobIndexForCurDay = totalJobs - daysWeHave; firstJobIndexForCurDay >= daysAhead; --firstJobIndexForCurDay)
                for (int firstJobIndexForCurDay = daysAhead; firstJobIndexForCurDay <= totalJobs - daysWeHave; ++firstJobIndexForCurDay)
                {
                    // once we pick up a particular job as the last job for current day, let's check the consequence of it with the result of the next day
                    int difficultySoFar = 0, totalDifficulties = int.MaxValue;
                    for (int lastJobIndexForCurDay = firstJobIndexForCurDay; lastJobIndexForCurDay < totalJobs - (daysWeHave - 1); ++lastJobIndexForCurDay)
                    {
                        difficultySoFar = Math.Max(jobDifficulty[lastJobIndexForCurDay], difficultySoFar);
                        totalDifficulties = Math.Min(totalDifficulties, difficultySoFar + dp[lastJobIndexForCurDay + 1]);
                    }

                    dp[firstJobIndexForCurDay] = totalDifficulties;
                }
            }

            return dp[0];
        }

        public int MinDifficulty_BottomUp_2D(int[] jobDifficulty, int totalDays)
        {
            int totalJobs = jobDifficulty.Length;
            if (totalDays > totalJobs)
                return -1;

            // dp[d, i] represents the minimal difficulty to start with the i'th job on day d
            int[,] dp = new int[totalDays + 1, totalJobs + 1];

            // base cases(of last day)
            for (int job = totalJobs - 1; job >= totalDays - 1; --job)
                dp[totalDays, job] = Math.Max(dp[totalDays, job + 1], jobDifficulty[job]);

            for(int day = totalDays - 1; day >= 1; --day)
            {
                int daysAhead = day - 1;
                int daysWeHave = totalDays - daysAhead;
                for (int firstJobIndexForCurDay = totalJobs - daysWeHave; firstJobIndexForCurDay >= daysAhead; --firstJobIndexForCurDay)
                {
                    int difficultySoFar = 0, totalDifficulties = int.MaxValue;
                    for (int lastJobIndexForCurDay = firstJobIndexForCurDay; lastJobIndexForCurDay < totalJobs - (daysWeHave - 1); ++lastJobIndexForCurDay)
                    {
                        difficultySoFar = Math.Max(jobDifficulty[lastJobIndexForCurDay], difficultySoFar);
                        totalDifficulties = Math.Min(totalDifficulties, difficultySoFar + dp[day + 1, lastJobIndexForCurDay + 1]);
                    }

                    dp[day, firstJobIndexForCurDay] = totalDifficulties;
                }
            }

            return dp[1, 0];
        }

        public int MinDifficulty_TopDown(int[] jobDifficulty, int d)
        {
            if (d > jobDifficulty.Length)
                return -1;

            int[] maxDifficultiesFromRight = new int[jobDifficulty.Length];
            maxDifficultiesFromRight[maxDifficultiesFromRight.Length - 1] = jobDifficulty[jobDifficulty.Length - 1];
            for(int i = 1; i < jobDifficulty.Length; ++i)
            {
                maxDifficultiesFromRight[jobDifficulty.Length - 1 - i] = Math.Max(jobDifficulty[jobDifficulty.Length - 1 - i], maxDifficultiesFromRight[jobDifficulty.Length - i]);
            }

            Dictionary<(int, int), int> difficulties = new Dictionary<(int, int), int>();
            this.MinDifficulty(jobDifficulty, d, 0, 1, difficulties, maxDifficultiesFromRight);
            return difficulties[(0, 1)];
        }

        private int MinDifficulty(int[] jobDifficulty, int days, int jobIndex, int day, Dictionary<(int, int), int> cache, int[] maxDifficultiesFromRight)
        {
            if (!cache.TryGetValue((jobIndex, day), out int res))
            {
                if (day == days)
                {
                    res = cache[(jobIndex, day)] = maxDifficultiesFromRight[jobIndex];
                }
                else
                {
                    int minDifficulty = int.MaxValue, maxDifficultySoFar = 0;
                    for (int i = jobIndex; i < jobDifficulty.Length - (days - day); i++)
                    {
                        maxDifficultySoFar = Math.Max(maxDifficultySoFar, jobDifficulty[i]);
                        minDifficulty = Math.Min(minDifficulty,
                                                 maxDifficultySoFar + this.MinDifficulty(jobDifficulty, days, i + 1, day + 1, cache, maxDifficultiesFromRight));
                    }

                    res = cache[(jobIndex, day)] = minDifficulty;
                }
            }

            return res;
        }
    }
}
