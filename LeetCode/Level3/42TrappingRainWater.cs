using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _42TrappingRainWater
    {
        public int Trap(int[] height)
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
