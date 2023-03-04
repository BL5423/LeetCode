using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _210CourseScheduleII
    {
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            int[] indegree = new int[numCourses];
            LinkedList<int>[] adjacent = new LinkedList<int>[numCourses];
            foreach(var prerequisite in prerequisites)
            {
                int prev = prerequisite[1];
                int next = prerequisite[0];
                if (adjacent[prev] == null)
                {
                    adjacent[prev] = new LinkedList<int>();
                }

                adjacent[prev].AddLast(next);
                ++indegree[next];
            }

            List<int> res = new List<int>(numCourses);
            Queue<int> queue = new Queue<int>(numCourses);
            for(int c = 0; c < numCourses; ++c)
            {
                if (indegree[c] == 0)
                {
                    queue.Enqueue(c);
                }
            }

            while (queue.Count != 0)
            {
                var course = queue.Dequeue();
                res.Add(course);
                if (adjacent[course] != null)
                {
                    foreach (var nextCourse in adjacent[course])
                    {
                        if (--indegree[nextCourse] == 0)
                        {
                            queue.Enqueue(nextCourse);
                        }
                    }
                }
            }

            return res.Count == numCourses ? res.ToArray() : Array.Empty<int>();
        }
        
        public int[] FindOrderV1(int numCourses, int[][] prerequisites)
        {
            List<int> completed = new List<int>();
            Dictionary<int, int> predecessors = new Dictionary<int, int>();
            Dictionary<int, HashSet<int>> successors = new Dictionary<int, HashSet<int>>();
            for(int course = 0; course <numCourses; ++course)
            {
                // initially no predecssors for every course
                predecessors.Add(course, 0);
            }
            foreach (var prerequisite in prerequisites)
            {
                var sucCourse = prerequisite[0];
                var preCourse = prerequisite[1];
                if (!successors.TryGetValue(preCourse, out HashSet<int> sucCourses))
                {
                    sucCourses = new HashSet<int>();
                    successors.Add(preCourse, sucCourses);
                }
                sucCourses.Add(sucCourse);

                ++predecessors[sucCourse];
            }

            bool opened = true;
            while (completed.Count < numCourses && opened)
            {
                opened = false;
                foreach (var predecessor in predecessors)
                {
                    if (predecessor.Value != 0)
                        continue;

                    completed.Add(predecessor.Key);
                    opened = true;
                    if (successors.TryGetValue(predecessor.Key, out HashSet<int> openCourses))
                    {
                        foreach (var openCourse in openCourses)
                        {
                            --predecessors[openCourse];
                        }
                    }

                    predecessors.Remove(predecessor.Key);
                    break;
                }
            }

            return completed.Count == numCourses ? completed.ToArray() : Array.Empty<int>();
        }
    }
}
