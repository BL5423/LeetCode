using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _84LargestRectangleinHistogram
    {
        public int LargestRectangleArea(int[] heights)
        {
            int[] leftBoundaries = new int[heights.Length];
            int[] rightBoundaries = new int[heights.Length];

            Stack<int> rightStack = new Stack<int>();
            for (int i = heights.Length - 1; i >= 0; --i)
            {
                int height = heights[i];
                while (rightStack.Count != 0 && heights[rightStack.Peek()] >= height)
                {
                    // keep finding the first shorter bar on the right
                    rightStack.Pop();
                }

                if (rightStack.Count != 0)
                    rightBoundaries[i] = rightStack.Peek();
                else
                    rightBoundaries[i] = heights.Length; // height is the shortest so far

                rightStack.Push(i);
            }

            Stack<int> leftStack = new Stack<int>();
            for(int i = 0; i < heights.Length; ++i)
            {
                int height = heights[i];
                while (leftStack.Count != 0 && heights[leftStack.Peek()] >= height)
                {
                    // keep finding the first shorter bar on the left
                    leftStack.Pop();
                }

                if (leftStack.Count != 0)
                    leftBoundaries[i] = leftStack.Peek();
                else
                    leftBoundaries[i] = -1; // height is the shortest so far

                leftStack.Push(i);
            }

            int maxArea = 0;
            for(int i = 0; i < heights.Length; ++i)
            {
                maxArea = Math.Max(maxArea, heights[i] * (rightBoundaries[i] - leftBoundaries[i] - 1));
            }

            return maxArea;
        }

        public int LargestRectangleAreaV2(int[] heights)
        {
            int maxArea = 0;
            Stack<int> increasingStack = new Stack<int>();
            increasingStack.Push(-1); // -1 is used to accommadate edges, say for the last(also the minimal) height(indexed at heights.Length - 1), its width should be heights.Length
            for(int i = 0; i < heights.Length; i++)
            {
                while (increasingStack.Peek() != -1 && heights[increasingStack.Peek()] > heights[i])
                {
                    int heightIndex = increasingStack.Pop();
                    int height = heights[heightIndex];
                    int rightIndex = i - 1; // anything between(inclusive) heightIndex and i - 1 is higher than height
                    int leftIndex = increasingStack.Peek(); // anything before(inclusive) leftIndex is shorter than height
                    maxArea = Math.Max(maxArea, height * (rightIndex - leftIndex));
                }

                increasingStack.Push(i);
            }

            while (increasingStack.Peek() != -1)
            {
                int heightIndex = increasingStack.Pop();
                int height = heights[heightIndex];
                int rightIndex = heights.Length - 1;
                int leftIndex = increasingStack.Peek();
                maxArea = Math.Max(maxArea, height * (rightIndex - leftIndex));
            }

            return maxArea;
        }

        public int LargestRectangleAreaV1(int[] heights)
        {
            // TLE
            int largestArea = 0;
            Stack<(int, int)> stack = new Stack<(int, int)>();
            for (int i = 0; i < heights.Length; ++i)
            {
                int height = heights[i];
                if (height == 0)
                {
                    stack.Clear();
                    continue;
                }

                if (largestArea < height)
                    largestArea = height;

                if (stack.Count != 0)
                {
                    bool merged = false;
                    LinkedList<(int, int)> newAreas = new LinkedList<(int, int)>();
                    while (stack.Count != 0)
                    {
                        var area = stack.Pop();
                        int prevWidth = area.Item1;
                        int prevHeight = area.Item2;
                        int newWidth = prevWidth + 1;
                        int newHeight = Math.Min(height, prevHeight);
                        int newArea = newWidth * newHeight;
                        if (newArea > largestArea)
                            largestArea = newArea;
                                                    
                        newAreas.AddLast((newWidth, newHeight));

                        if (newHeight >= height)
                        {
                            // height got merged
                            merged = true;
                        }
                    }

                    if (!merged)
                    {
                        newAreas.AddLast((1, height));
                    }

                    foreach (var newArea in newAreas)
                    {
                        stack.Push(newArea);
                    }
                }
                else
                {
                    stack.Push((1, height));
                }
            }

            return largestArea;
        }
    }
}
