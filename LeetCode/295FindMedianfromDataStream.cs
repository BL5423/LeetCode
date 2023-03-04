using ConsoleApp2.Level1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class MedianFinder
    {
        private PriorityQueue<int, int> leftQueue, rightQueue;

        public MedianFinder()
        {
            this.leftQueue = new PriorityQueue<int, int>();
            this.rightQueue = new PriorityQueue<int, int>();
        }

        public void AddNum(int num)
        {
            // we need to keep left and right queue in balance(difference in length should not exceed 1)
            int leftCount = this.leftQueue.Count;            
            int rightCount = this.rightQueue.Count;
            if (leftCount > rightCount)
            {
                // try to add num to right queue
                int leftMax = this.leftQueue.Peek();
                if (leftMax > num)
                {
                    this.leftQueue.Dequeue();
                    this.leftQueue.Enqueue(num, -num);
                    this.rightQueue.Enqueue(leftMax, leftMax);
                }
                else
                {
                    this.rightQueue.Enqueue(num, num);
                }
            }
            else // leftCount <= rightCount
            {
                if (rightCount == 0)
                    this.leftQueue.Enqueue(num, -num);
                else
                {
                    // try to add num to left queue
                    int rightMin = this.rightQueue.Peek();
                    if (rightMin < num)
                    {
                        this.rightQueue.Dequeue();
                        this.rightQueue.Enqueue(num, num);
                        this.leftQueue.Enqueue(rightMin, -rightMin);
                    }
                    else
                    {
                        this.leftQueue.Enqueue(num, -num);
                    }
                }
            }
        }

        public double FindMedian()
        {
            int leftCount = this.leftQueue.Count;
            int rightCount = this.rightQueue.Count;
            if (leftCount == rightCount)
            {
                return (this.leftQueue.Peek() + this.rightQueue.Peek()) / 2.0;
            }
            else if (leftCount > rightCount)
            {
                return this.leftQueue.Peek();
            }
            else
            {
                return this.rightQueue.Peek();
            }
        }
    }

    public class MedianFinderV1
    {
        const int N = 50000;

        private MyHeap leftHeap;
        private MinHeap rightHeap;

        public MedianFinderV1()
        {
            this.leftHeap = new MyHeap(N / 2 + 1);
            this.rightHeap = new MinHeap(N / 2 + 1);
        }

        public void AddNum(int num)
        {
            if (num >= this.rightHeap.Peek())
            {
                this.rightHeap.Push(num);
            }
            else
            {
                this.leftHeap.Push(num);
            }

            if (this.leftHeap.Count > this.rightHeap.Size() + 1)
            {
                var min = this.leftHeap.Pop();
                this.rightHeap.Push(min);
            }
            else if (this.leftHeap.Count + 1 < this.rightHeap.Size())
            {
                var max = this.rightHeap.Pop();
                this.leftHeap.Push(max);
            }
        }

        public void AddNumV1(int num)
        {
            this.leftHeap.Push(num);
            if (this.leftHeap.Peek() > this.rightHeap.Peek())
            {
                var min = this.leftHeap.Pop();
                this.rightHeap.Push(min);
            }
            if (this.leftHeap.Count > this.rightHeap.Size() + 1)
            {
                var min = this.leftHeap.Pop();
                this.rightHeap.Push(min);
            }
            else if (this.leftHeap.Count + 1 < this.rightHeap.Size())
            {
                var max = this.rightHeap.Pop();
                this.leftHeap.Push(max);
            }
        }

        public double FindMedian()
        {
            if (this.leftHeap.Count > this.rightHeap.Size())
            {
                return this.leftHeap.Peek();
            }
            else if (this.leftHeap.Count < this.rightHeap.Size())
            {
                return this.rightHeap.Peek();
            }

            return (this.leftHeap.Peek() + this.rightHeap.Peek()) / 2.0;
        }
    }
}
