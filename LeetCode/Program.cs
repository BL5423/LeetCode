using ConsoleApp117;
using ConsoleApp2;
using ConsoleApp2.Level2;
using ConsoleApp2.Level3;
using LC;
using LC.DP;
using LC.Level3;
using LC.SegmentTree;
using LC.Top100Medium;
using System;
using System.Text;
using TreeNode = ConsoleApp2.TreeNode;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new _621TaskScheduler().LeastInterval(new[] { 'A', 'A', 'A', 'B', 'B', 'B' }, 2));
            Console.WriteLine(new _621TaskScheduler().LeastInterval(new[] { 'A', 'A', 'A', 'B', 'B', 'B' }, 0));
            Console.WriteLine(new _621TaskScheduler().LeastInterval(new[] { 'A', 'A', 'A', 'A', 'A', 'A', 'B', 'C', 'D', 'E', 'F', 'G' }, 2));
        }
    }
}