using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class KthLargest
    {
        private MinHeap heap;

        private int k;

        public KthLargest(int k, int[] nums)
        {
            this.k = k;
            this.heap = new MinHeap(k);
            foreach(int num in nums)
            {
                this.heap.Push(num);
            }
        }

        public int Add(int val)
        {
            return this.heap.Push(val);
        }
    }

    public class MinHeap
    {
        private int[] nums;

        private int last;

        public MinHeap(int size)
        {
            this.nums = new int[size + 1];
            this.last = 0;
        }

        public int Peek()
        {
            return this.nums[0];
        }
        
        public int Size()
        {
            return this.last;
        }

        public int Push(int val)
        {
            this.nums[this.last++] = val;

            // bubble up
            int lastChildIndex = this.last - 1;
            if (lastChildIndex > 0)
            {
                int parent = (lastChildIndex - 1) / 2;
                while (parent >= 0)
                {
                    if (this.nums[parent] > this.nums[lastChildIndex])
                    {
                        // swap
                        int parentValue = this.nums[parent];
                        this.nums[parent] = this.nums[lastChildIndex];
                        this.nums[lastChildIndex] = parentValue;
                    }
                    else
                    {
                        break;
                    }

                    // parent becomes new child
                    lastChildIndex = parent;
                    parent = (lastChildIndex - 1) / 2;
                }
            }

            if (this.Size() == this.nums.Length)
            {
                this.Pop();
            }

            return this.Peek();
        }

        public int Pop()
        {
            if (this.Size() <= 0)
            {
                return -1;
            }

            var currentParentValue = this.Peek();

            int lastNumber = this.nums[--this.last];
            this.nums[0] = lastNumber;

            // sink down
            int parentIndex = 0;
            int leftChildIndex = parentIndex * 2 + 1;
            int rightChildIndex = parentIndex * 2 + 2;
            while (leftChildIndex <= this.last || rightChildIndex <= this.last)
            {
                int smallestIndex = parentIndex;
                if (leftChildIndex <= this.last && this.nums[parentIndex] > this.nums[leftChildIndex])
                {
                    smallestIndex = leftChildIndex;
                }
                if (rightChildIndex <= this.last && this.nums[smallestIndex] > this.nums[rightChildIndex])
                {
                    smallestIndex = rightChildIndex;
                }

                if (smallestIndex != parentIndex)
                {
                    // sawp parent and the biggest node(either left or right)
                    int parentValue = this.nums[parentIndex];
                    this.nums[parentIndex] = this.nums[smallestIndex];
                    this.nums[smallestIndex] = parentValue;

                    parentIndex = smallestIndex;
                    leftChildIndex = parentIndex * 2 + 1;
                    rightChildIndex = parentIndex * 2 + 2;
                }
                else
                {
                    break;
                }
            }

            return currentParentValue;
        }
    }
}
