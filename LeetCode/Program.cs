using ConsoleApp2;
using ConsoleApp2.Level1;
using LC;
using LC.Top100Liked;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new _154FindMinimuminRotatedSortedArrayII().FindMin(new int[] { 1, 1 }));
            Console.WriteLine(new _154FindMinimuminRotatedSortedArrayII().FindMin(new int[] { 1, 3, 3 }));
            Console.WriteLine(new _154FindMinimuminRotatedSortedArrayII().FindMin(new int[] { 3, 3, 1, 3 }));
            Console.WriteLine(new _154FindMinimuminRotatedSortedArrayII().FindMin(new int[] { 3, 3, 3, 3, 1, 3, 3 }));
            Console.WriteLine(new _154FindMinimuminRotatedSortedArrayII().FindMin(new int[] { 2, 0, 1, 1, 1 }));
        }
    }
}