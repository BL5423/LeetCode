using System;

namespace ConsoleApp2
{
    public class _11ContainerWithMostWater
    {
        public int MaxArea(int[] height)
        {
            int start = 0, end = height.Length - 1, maxArea = 0;
            while (start < end)
            {
                int area = Math.Min(height[start], height[end]) * (end - start);
                if (area > maxArea)
                    maxArea = area;

                if (height[end] < height[start])
                    --end;
                else
                    ++start;
            }

            return maxArea;
        }

        public int MaxArea1(int[] height)
        {
            int maxArea = 0;
            int left = 0, right = 0;
            for (int start = 0; start < height.Length; ++start)
            {
                if (height[start] < left)
                    continue;

                for (int end = height.Length - 1; end > start; --end)
                {
                    if (height[end] < right)
                        continue;

                    int currentArea = (end - start) * Math.Min(height[start], height[end]);
                    if (currentArea > maxArea)
                    {
                        left = height[start];
                        right = height[end];
                        maxArea = currentArea;
                    }
                }
            }

            return maxArea;
        }

        public int MaxArea2(int[] height)
        {
            int maxArea = 0;
            int left = 0, right = height.Length - 1;
            while(left < right)
            {
                int currentArea = (right - left) * Math.Min(height[left], height[right]);
                if (currentArea > maxArea)
                {
                    maxArea = currentArea;
                }

                if (height[left] < height[right])
                    ++left;
                else
                    --right;
            }

            return maxArea;
        }
    }
}
