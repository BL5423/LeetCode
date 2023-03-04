using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _1046LastStoneWeight
    {
        public int LastStoneWeight(int[] stones)
        {
            var heap = new MyHeap(stones.Length);
            foreach(var stone in stones)
            {
                heap.Push(stone);
            }

            while (heap.Count > 1)
            {
                int heaviest1 = heap.Pop();
                int heaviest2 = heap.Pop();
                var left = heaviest1 - heaviest2;
                if (left != 0)
                {
                    heap.Push(left);
                }
            }

            return heap.Count == 1 ? heap.Peek() : 0;
        }
    }

    public class MyHeap
    {
        private int count;

        private int[] nums;
        
        public MyHeap(int capacity)
        {
            this.nums = new int[capacity];
            this.count = 0;
        }

        public int Count { get { return this.count; } private set { this.count = value; } }

        public bool Push(int num)
        {
            bool done = false;
            if (this.Count < this.nums.Length)
            {
                int index = this.Count++;
                this.nums[index] = num;

                // bubble up if needed
                int parentIndex = (index - 1) / 2;
                while (parentIndex >= 0)
                {
                    // max heap
                    if (this.nums[parentIndex] < this.nums[index])
                    {
                        // swap
                        int temp = this.nums[parentIndex];
                        this.nums[parentIndex] = this.nums[index];
                        this.nums[index] = temp;

                        index = parentIndex;
                        parentIndex = (index - 1) / 2;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return done;
        }

        public int Pop()
        {
            int ret = this.Peek();
            this.nums[0] = this.nums[--this.Count];

            // sink if needed
            int parentIndex = 0;
            int leftKid = parentIndex * 2 + 1;
            int rightKid = parentIndex * 2 + 2;
            while (leftKid < this.Count || rightKid < this.Count)
            {
                int maxNumIndex = parentIndex;
                int maxNum = this.nums[parentIndex];
                if (leftKid < this.Count && this.nums[leftKid] > maxNum)
                {
                    maxNum = this.nums[leftKid];
                    maxNumIndex = leftKid;
                }
                if (rightKid < this.Count && this.nums[rightKid] > maxNum)
                {
                    maxNum = this.nums[rightKid];
                    maxNumIndex = rightKid;
                }

                if (maxNumIndex != parentIndex)
                {
                    // swap
                    int temp = this.nums[parentIndex];
                    this.nums[parentIndex] = maxNum;
                    this.nums[maxNumIndex] = temp;

                    parentIndex = maxNumIndex;
                    leftKid = parentIndex * 2 + 1;
                    rightKid = parentIndex * 2 + 2;
                }
                else
                {
                    break;
                }
            }

            return ret;
        }

        public int Peek()
        {
            return this.nums[0];
        }
    }
}
