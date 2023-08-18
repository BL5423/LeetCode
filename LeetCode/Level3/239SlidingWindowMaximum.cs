using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Level3
{
    public class _239SlidingWindowMaximum
    {
        public int[] MaxSlidingWindow_Queue(int[] nums, int k)
        {
            LinkedList<int> decreasingQueue = new LinkedList<int>();
            int[] res = new int[nums.Length - k + 1];
            int resIndex = 0;
            for(int i = 0; i < nums.Length; ++i)
            {
                int indexToRemove = i - k;
                int numToAdd = nums[i];
                while (decreasingQueue.Count != 0 && decreasingQueue.First() <= indexToRemove)
                    decreasingQueue.RemoveFirst();

                while (decreasingQueue.Count != 0 && nums[decreasingQueue.Last()] < numToAdd)
                    decreasingQueue.RemoveLast();

                decreasingQueue.AddLast(i);
                if (indexToRemove >= -1)
                {
                    res[resIndex++] = nums[decreasingQueue.First()];
                }
            }

            return res;
        }

        public int[] MaxSlidingWindow_V2(int[] nums, int k)
        {
            LinkedList<int> decreasingQueue = new LinkedList<int>();
            int[] res = new int[nums.Length - k + 1];
            int resIndex = 0;
            for(int i = 0; i < nums.Length; ++i)
            {
                int left = i - k + 1;

                // remove any element that is out of the current sliding window
                while (decreasingQueue.Count != 0 && decreasingQueue.First.Value < left)
                    decreasingQueue.RemoveFirst();

                int numToAdd = nums[i];
                // remove any element that is smaller than the next num to be added
                // this is to make sure that the leftmost element is always the largest within the window
                while (decreasingQueue.Count != 0 && nums[decreasingQueue.Last.Value] < numToAdd)
                    decreasingQueue.RemoveLast();

                decreasingQueue.AddLast(i);

                if (i >= k - 1)
                {
                    res[resIndex++] = nums[decreasingQueue.First.Value];
                }
            }

            return res;
        }

        public int[] MaxSlidingWindow_V1Heap(int[] nums, int k)
        {
            Dictionary<int, int> counters = new Dictionary<int, int>(nums.Length);
            int[] res = new int[(nums.Length - k) + 1];
            EditableMaxHeap<int> maxHeap = new EditableMaxHeap<int>(nums.Length);
            for(int i = 0; i < k; ++i)
            {
                maxHeap.Push(nums[i]);

                if (!counters.TryGetValue(nums[i], out int counter))
                {
                    counters.Add(nums[i], 0);
                }
                ++counters[nums[i]];
            }

            int left = 0, right = k - 1;
            for(int i = 0; i < res.Length; ++i)
            {
                while (counters[maxHeap.Peek()] <= 0) 
                    maxHeap.Pop();

                res[i] = maxHeap.Peek();
                --counters[nums[left++]];

                if (right < nums.Length - 1)
                {
                    int nextNum = nums[++right];
                    maxHeap.Push(nextNum);

                    if (!counters.TryGetValue(nextNum, out int counter))
                    {
                        counters.Add(nextNum, 0);
                    }
                    ++counters[nextNum];
                }
            }

            return res;
        }
    }

    public class EditableMaxHeap<T> where T : IComparable<T>
    {
        private T[] nums;

        private int last;

        public EditableMaxHeap(int size)
        {
            this.nums = new T[size + 1];
            this.last = 0;
        }

        public T Peek()
        {
            return this.nums[0];
        }

        public int Size()
        {
            return this.last;
        }

        public T Push(T val)
        {
            this.nums[this.last++] = val;

            // bubble up
            int lastChildIndex = this.last - 1;
            if (lastChildIndex > 0)
            {
                int parent = (lastChildIndex - 1) / 2;
                while (parent >= 0 && parent < lastChildIndex)
                {
                    if (this.nums[parent].CompareTo(this.nums[lastChildIndex]) < 0)
                    {
                        // swap
                        T parentValue = this.nums[parent];
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

        public T Pop()
        {
            if (this.Size() <= 0)
            {
                throw new InvalidOperationException("The heap is empty!");
            }

            var currentParentValue = this.Peek();
            this.RemoveAt(0);

            return currentParentValue;
        }

        public bool Remove(T t)
        {
            int pos = -1;
            Queue<int> queue = new Queue<int>(this.last);
            queue.Enqueue(0);
            while (queue.Count != 0)
            {
                var index = queue.Dequeue();
                if (index < this.last)
                {
                    int result = this.nums[index].CompareTo(t);
                    if (result == 0)
                    {
                        pos = index;
                        break;
                    }

                    int leftChildIndex = index * 2 + 1;
                    int rightChildIndex = index * 2 + 2;
                    queue.Enqueue(leftChildIndex);
                    queue.Enqueue(rightChildIndex);
                }
            }

            if (pos >= 0)
            {
                this.RemoveAt(pos);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            this.nums[index] = this.nums[--this.last];

            int parentIndex = (index - 1) / 2;
            if (parentIndex >= 0 && parentIndex != index && this.nums[parentIndex].CompareTo(this.nums[index]) < 0)
            {
                // bubble up
                while (parentIndex >= 0 && parentIndex < index)
                {
                    if (this.nums[parentIndex].CompareTo(this.nums[index]) < 0)
                    {
                        // swap
                        T parentValue = this.nums[parentIndex];
                        this.nums[parentIndex] = this.nums[index];
                        this.nums[index] = parentValue;
                    }
                    else
                    {
                        break;
                    }

                    // parent becomes new child
                    index = parentIndex;
                    parentIndex = (index - 1) / 2;
                }
            }
            else
            {
                // sink down
                parentIndex = index;
                int leftChildIndex = parentIndex * 2 + 1;
                int rightChildIndex = parentIndex * 2 + 2;
                while (leftChildIndex < this.last || rightChildIndex < this.last)
                {
                    int biggestIndex = parentIndex;
                    if (leftChildIndex < this.last && this.nums[parentIndex].CompareTo(this.nums[leftChildIndex]) < 0)
                    {
                        biggestIndex = leftChildIndex;
                    }
                    if (rightChildIndex < this.last && this.nums[biggestIndex].CompareTo(this.nums[rightChildIndex]) < 0)
                    {
                        biggestIndex = rightChildIndex;
                    }

                    if (biggestIndex != parentIndex)
                    {
                        // sawp parent and the biggest node(either left or right)
                        T parentValue = this.nums[parentIndex];
                        this.nums[parentIndex] = this.nums[biggestIndex];
                        this.nums[biggestIndex] = parentValue;

                        parentIndex = biggestIndex;
                        leftChildIndex = parentIndex * 2 + 1;
                        rightChildIndex = parentIndex * 2 + 2;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
