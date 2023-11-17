using ConsoleApp117;
using ConsoleApp2;
using ConsoleApp2.Level3;
using LC;
using LC.DP;
using LC.Level3;
using LC.Top100Medium;
using System;
using TreeNode = ConsoleApp2.TreeNode;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var intervals = new int[3][];
            intervals[0] = new int[] { 0, 30 };
            intervals[1] = new int[] { 60, 240 };
            intervals[2] = new int[] { 90, 120 };
            new MergeIntervals().CanAttendMeetings(intervals);
        }
    }
}