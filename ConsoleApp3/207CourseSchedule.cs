using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _207CourseSchedule
    {
        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            if (prerequisites.Length == 0)
                return true;

            HashSet<int> courses = new HashSet<int>(numCourses);
            Dictionary<int, HashSet<int>> inEdges = new Dictionary<int, HashSet<int>>(prerequisites.Length);
            Dictionary<int, HashSet<int>> outEdges = new Dictionary<int, HashSet<int>>(prerequisites.Length);
            foreach (int[] prerequisite in prerequisites)
            {
                courses.Add(prerequisite[0]);
                courses.Add(prerequisite[1]);
                if (!inEdges.TryGetValue(prerequisite[0], out HashSet<int> pres))
                {
                    pres = new HashSet<int>();
                    inEdges[prerequisite[0]] = pres;
                }
                pres.Add(prerequisite[1]);

                if (!outEdges.TryGetValue(prerequisite[1], out HashSet<int> next))
                {
                    next = new HashSet<int>();
                    outEdges[prerequisite[1]] = next;
                }
                next.Add(prerequisite[0]);
            }

            var initials = new Queue<int>();
            foreach(int course in courses)
            {
                if (!inEdges.ContainsKey(course))
                {
                    initials.Enqueue(course);
                }
            }

            // BFS
            while (initials.Count > 0)
            {
                var course = initials.Dequeue();
                if (outEdges.TryGetValue(course, out HashSet<int> nexts))
                {
                    foreach (var next in nexts)
                    {
                        inEdges[next].Remove(course);
                        if (inEdges[next].Count == 0)
                        {
                            inEdges.Remove(next);
                            initials.Enqueue(next);
                        }
                    }
                }
            }

            return inEdges.Count == 0;
        }

        public bool CanFinishDFS(int numCourses, int[][] prerequisites)
        {
            bool[] visitedGlobal = new bool[numCourses];
            bool[] visitedLocal = new bool[numCourses];
            List<int>[] outEdges = new List<int>[numCourses];
            foreach(int[] prerequisite in prerequisites)
            {
                if (outEdges[prerequisite[0]] == null)
                    outEdges[prerequisite[0]] = new List<int>();
                outEdges[prerequisite[0]].Add(prerequisite[1]);
            }

            for(int course = 0; course < numCourses; ++course)
            {
                if (!visitedGlobal[course] && outEdges[course] != null)
                {
                    if (DFS(visitedGlobal, visitedLocal, course, outEdges))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool DFS(bool[] visitedGlobal, bool[] visitedLocal, int course, List<int>[] outEdges)
        {
            if (outEdges[course] != null)
            {
                visitedGlobal[course] = true;


                visitedLocal[course] = true;
                foreach(int next in outEdges[course])
                {
                    if (visitedGlobal[next] == false && DFS(visitedGlobal, visitedLocal, next, outEdges))
                        return true;
                    if (visitedLocal[next] == true)
                        // loop
                        return true;
                }

                visitedLocal[course] = false;
            }

            return false;
        }
    }
}
