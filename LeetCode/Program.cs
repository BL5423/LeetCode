using ConsoleApp2;
using ConsoleApp2.DP;
using ConsoleApp2.Level1;
using ConsoleApp2.Level3;
using LC;
using LC.DP;
using LC.Level3;
using LC.Top100Liked;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LengthOfLongestSubstring("abcabcbb"));
        }

        public static int LengthOfLongestSubstring(string s)
        {
            int[] pos = new int[128];
            int left = 0, right = 0, maxLen = 0;
            while (left <= right && right < s.Length)
            {
                if (pos[s[right]] > 0)
                {
                    left = Math.Max(pos[s[right]], left);
                }

                maxLen = Math.Max(maxLen, right - left + 1);
                pos[s[right]] = right + 1;
                ++right;
            }

            return maxLen;
        }
    }
}