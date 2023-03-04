using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _1494ParallelCoursesII
    {
        public int MinNumberOfSemesters_DP(int n, int[][] relations, int k)
        {
            // dp[i] means the minimal semesters needed if we complete courses of bits set(as 1) in i            
            int[] dp = new int[1<<n];
            for (int i = 0; i < (1 << n); ++i)
                dp[i] = n; // in worst case, we can only take 1 course each semester

            int[] prev = new int[n];
            foreach(var relation in relations)
            {
                // if i-th bit is set(as 1) in prev[j], it means in order to complete course j, we need to complete i first
                prev[relation[1] - 1] |= (1 << (relation[0] - 1));
            }

            // base case
            dp[0] = 0;

            // iterate through all possible combinations of courses
            for (int i = 0; i < (1 << n); ++i)
            {
                // next set of courses can be complete
                int nextCourses = 0;
                for (int j = 0; j < n; ++j)
                {
                    // if all prerequisite of j have been completed(indiated as set bits in i)
                    // then j is open to complete
                    if ((prev[j] & i) == prev[j])
                    {
                        nextCourses |= (1 << j);
                    }
                }

                // exclude those courses have been completed in i of previous semesters
                nextCourses &= (~i);

                // now with all potential courses in nextCourses, we can check the combinations of them to find the optimal choice
                for (int courses = nextCourses; courses != 0; courses = (courses - 1) & nextCourses)
                {
                    // num of set bits in courses are the num of courses we complete if chosen
                    if (BitsOf(courses) <= k)
                    {
                        // check if we can combine the courses in 'courses' into the current completed courses in i, or
                        // we just take a new semester
                        dp[i | courses] = Math.Min(dp[i | courses], dp[i] + 1);
                    }
                }
            }

            return dp.Last();
        }

        private int BitsOf(int num)
        {
            int bits = 0;
            while (num != 0)
            {
                bits += (num & 1);
                num >>= 1;
            }

            return bits;
        }

        public int MinNumberOfSemesters_Greedy(int n, int[][] relations, int k)
        {
            // Failed, not applicable
            int[] indegree = new int[n + 1];
            var v2e = new LinkedList<int>[n + 1];
            foreach (var relation in relations)
            {
                int prev = relation[0];
                int next = relation[1];
                if (v2e[prev] == null)
                {
                    v2e[prev] = new LinkedList<int>();
                }
                v2e[prev].AddLast(next);
                ++indegree[next];
            }

            int[] heights = new int[n + 1];
            for(int i = 1; i <= n; ++i)
            {
                heights[i] = this.GetHeight(i, heights, v2e);
            }

            var maxHeap = new MaxHeap<Course>(n);
            for(int i = 1; i <= n; ++i)
            {
                if (indegree[i] == 0)
                {
                    maxHeap.Push(new Course(i, heights[i]));
                }
            }

            int semesters = 0;
            while (maxHeap.Size() != 0)
            {
                ++semesters;

                LinkedList<int> nextCourses = new LinkedList<int>();
                int num = Math.Min(k, maxHeap.Size());
                for(int i = 0; i < num; ++i)
                {
                    var course = maxHeap.Pop();
                    Console.Write(course.id + "(" + course.height + ") ");
                    if(v2e[course.id] != null)
                    {
                        foreach(var nextCourse in v2e[course.id])
                        {
                            if (--indegree[nextCourse] == 0)
                            {
                                nextCourses.AddLast(nextCourse);
                            }
                        }
                    }
                }
                Console.WriteLine();

                foreach (var nextCourse in nextCourses)
                {
                    maxHeap.Push(new Course(nextCourse, heights[nextCourse]));
                }
            }

            return semesters;
        }

        private int GetHeight(int course, int[] heights, LinkedList<int>[] v2e)
        {
            if (heights[course] == 0)
            {
                heights[course] = 1;
                if (v2e[course] != null)
                {
                    foreach (var nextCourse in v2e[course])
                    {
                        heights[course] = Math.Max(heights[course], 1 + this.GetHeight(nextCourse, heights, v2e));
                    }
                }
            }
            
            return heights[course];
        }
    }

    public class Course : IComparable<Course>
    {
        public int id, height;

        public Course(int id, int height)
        {
            this.id = id;
            this.height = height;
        }

        public int CompareTo([AllowNull] Course other)
        {
            return this.height - other.height;
        }
    }
}
