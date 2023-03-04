using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _852PeakIndexInAMoutainArray
    {
        public int PeakIndexInMountainArray(int[] arr)
        {
            int start = 0, end = arr.Length - 1;
            while (start <= end)
            {
                int index = start + (end - start + 1) / 2;
                if (arr[index] > arr[index - 1] && arr[index] > arr[index + 1])
                {
                    return index;
                }
                else if (arr[index] < arr[index - 1])
                {
                    // index falls on the right then move 'end' to left/smaller
                    end = index - 1;
                }
                else if (arr[index] < arr[index + 1])
                {
                    // index falls on the left then move 'start' to right/bigger
                    start = index + 1;
                }
            }

            return -1;
        }
    }
}
