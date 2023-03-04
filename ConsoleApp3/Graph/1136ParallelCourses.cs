using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _1136ParallelCourses
    {
        public int MinimumSemesters(int n, int[][] relations)
        {
            int[] indegrees = new int[n + 1];
            LinkedList<int>[] v2e = new LinkedList<int>[n + 1];
            foreach(var relation in relations)
            {
                int prev = relation[0];
                int next = relation[1];
                if (v2e[prev] == null)
                {
                    v2e[prev] = new LinkedList<int>();
                }
                v2e[prev].AddLast(next);
                ++indegrees[next];
            }

            Queue<int> queue = new Queue<int>(n + 1);
            for(int i = 1; i <= n;++i)
            {
                if (indegrees[i] == 0)
                    queue.Enqueue(i);
            }

            int completedCourses = 0;
            int semesters = 0;
            while (queue.Count > 0 && completedCourses < n)
            {
                ++semesters;
                int count = queue.Count;
                for(int c = 0; c < count; ++c)
                {
                    var curCourse = queue.Dequeue();
                    if (v2e[curCourse] != null)
                    {
                        foreach (var nextCourse in v2e[curCourse])
                        {
                            if (--indegrees[nextCourse] == 0)
                            {
                                queue.Enqueue(nextCourse);
                            }
                        }
                    }
                }

                completedCourses += count;
            }

            return completedCourses == n ? semesters : -1;
        }
    }
}
