using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _42TrappingRainWater
    {
        public int Trap(int[] heights)
        {
            int water = 0;
            int left = 0, leftMaxSoFar = heights[left], right = heights.Length - 1, rightMaxSoFar = heights[right];
            while (left < right)
            {
                while (leftMaxSoFar <= rightMaxSoFar)
                {
                    water += leftMaxSoFar - heights[left];
                    if (++left < right)
                        leftMaxSoFar = Math.Max(leftMaxSoFar, heights[left]);
                    else
                        break;
                }

                while (left < right && leftMaxSoFar > rightMaxSoFar)
                {
                    water += rightMaxSoFar - heights[right];
                    if (--right > left)
                        rightMaxSoFar = Math.Max(rightMaxSoFar, heights[right]);
                    else
                        break;
                }
            }

            return water;
        }

        public int TrapDP(int[] height)
        {
            int[] leftMax = new int[height.Length], rightMax = new int[height.Length];
            for (int i = 0, j = height.Length - 1; i < height.Length && j >= 0; ++i, --j)
            {
                leftMax[i] = Math.Max(height[i], i > 0 ? leftMax[i - 1] : 0);
                rightMax[j] = Math.Max(height[j], j < height.Length - 1 ? rightMax[j + 1] : 0);
            }

            int water = 0;
            for (int i = 0; i < height.Length; ++i)
            {
                water += Math.Min(leftMax[i], rightMax[i]) - height[i];
            }

            return water;
        }

        public int TrapV3(int[] heights)
        {
            int water = 0;
            // use a stack to store the heights on right that are increasing
            Stack<int> stack = new Stack<int>(heights.Length);
            for (int i = heights.Length - 1; i >= 0; --i)
            {
                while (stack.Count != 0 && heights[i] > heights[stack.Peek()])
                {
                    // top is shorter than current height, which means it is able to hold some water before current height and another height(taller) on its right
                    int top = stack.Pop();
                    if (stack.Count != 0)
                    {
                        int width = stack.Peek() - i - 1;
                        int height = Math.Min(heights[i], heights[stack.Peek()]) - heights[top];
                        water += width * height;
                    }
                }

                stack.Push(i);
            }

            return water;
        }

        public int TrapV2(int[] height)
        {
            int left = 0, right = height.Length - 1, trappedWater = 0, heightsFilled = 0;
            while (left < right)
            {
                int leftHeight = height[left];
                int rightHeight = height[right];
                if (leftHeight > 0 && rightHeight > 0)
                {
                    var minHeight = Math.Min(leftHeight, rightHeight);
                    if (heightsFilled < minHeight)
                    {
                        heightsFilled = minHeight;
                    }
                }

                if (leftHeight < heightsFilled)
                {
                    trappedWater += (heightsFilled - leftHeight);
                }
                if (rightHeight < heightsFilled)
                {
                    trappedWater += (heightsFilled - rightHeight);
                }

                if (leftHeight < rightHeight)
                {
                    // move left to right
                    ++left;
                }
                else
                {
                    // move right to left
                    --right;
                }
            }

            return trappedWater;
        }

        public int TrapV1(int[] height)
        {
            int left = 0, right = height.Length - 1, trappedWater = 0;
            while (left < right - 1)
            {
                int leftHeight = height[left];
                int rightHeight = height[right];
                if (leftHeight > 0 && rightHeight > 0)
                {
                    var minHeight = Math.Min(leftHeight, rightHeight);

                    // calculate trapped water between the two heights
                    for (int index = left + 1; index < right; ++index)
                    {
                        if (height[index] < minHeight)
                        {
                            trappedWater += minHeight - height[index];
                            height[index] = minHeight; // fill the gap with water so that it wont be counted again
                        }
                    }
                }

                if (leftHeight < rightHeight)
                {
                    // move left to right
                    while (left < right - 1)
                    {
                        if (height[++left] > leftHeight)
                            break;
                    }
                }
                else
                {
                    // move right to left
                    while (right > left + 1)
                    {
                        if (height[--right] > rightHeight)
                            break;
                    }
                }
            }

            return trappedWater;
        }
    }
}
