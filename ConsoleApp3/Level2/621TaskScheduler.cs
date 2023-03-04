using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _621TaskScheduler
    {
        public int LeastInterval(char[] tasks, int n)
        {
            // Basic idea:
            // group the tasks into X buckets, X equals to the frequency of the most frequent task
            // the problem is to fill each bucket with n + 1 tasks(real or idle)
            // initially, put one of the most frequent tasks into each bucket
            // and then, fill each bucket with n idle tasks
            // next, each time put one real task into the next not full bucket, and remove an idle task from it
            // once all real tasks have been put into buckets, count the idle tasks left
            // edge case: we don't have to fill the last bucket with idle tasks
            int[] counts = new int['Z' - 'A' + 1];
            foreach(char task in tasks)
            {
                ++counts[task - 'A'];
            }

            Array.Sort(counts, (x, y) => y - x);
            int maxFrequency = counts[0];
            int slots = (maxFrequency - 1) * n; // # of idle tasks at beginning
            for (int i = 1; i < counts.Length && slots > 0; ++i)
            {
                // the last bucket does not have to be filled, so there are up to maxFrequency(X) - 1 buckets to put the task into.
                slots -= Math.Min(maxFrequency - 1, counts[i]);
            }

            return tasks.Length + (slots >= 0 ? slots : 0);
        }

        public int LeastIntervalV1(char[] tasks, int n)
        {
            int total = 0;
            int[] counts = new int['Z' - 'A' + 1];
            foreach(char task in tasks)
            {
                ++total;
                ++counts[task - 'A'];
            }

            int length = 0;
            while (true)
            {
                int index = 0;
                Array.Sort(counts, (x, y) => y - x);
                if (counts[index] == 0)
                    break;

                --counts[index];
                --total;
                ++length;
                int t = 0;
                for (int j = index + 1; j < counts.Length; ++j)
                {
                    if (counts[j] > 0)
                    {
                        --counts[j];
                        --total;
                        ++t;
                        if (t == n)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                length += t;
                if (t < n)
                {
                    if (total > 0)
                    {
                        length += (n - t);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return length;
        }
    }
}
