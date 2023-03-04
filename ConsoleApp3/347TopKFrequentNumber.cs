using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _347TopKFrequentNumber
    {
        public int[] TopKFrequent(int[] nums, int k)
        {
            int maxFrequency = 0;
            Dictionary<int, int> numsToFrequency = new Dictionary<int, int>();
            foreach(int num in nums)
            {
                if (numsToFrequency.TryGetValue(num, out int count))
                {
                    numsToFrequency[num] = ++count;
                }
                else
                {
                    count = 1;
                    numsToFrequency.Add(num, count);
                }

                if (count > maxFrequency)
                {
                    maxFrequency = count;
                }
            }

            LinkedList<int>[] frequencyTable = new LinkedList<int>[maxFrequency];
            foreach(var numToFrequency in numsToFrequency)
            {
                int frequency = numToFrequency.Value;
                int num = numToFrequency.Key;
                if (frequencyTable[frequency - 1] == null)
                {
                    frequencyTable[frequency - 1] = new LinkedList<int>();
                }

                frequencyTable[frequency - 1].AddLast(num);
            }

            int[] res = new int[k];
            int index = 0;
            for(int i = frequencyTable.Length - 1; i >= 0 && index < k; --i)
            {
                if (frequencyTable[i] != null)
                {
                    foreach(var num in frequencyTable[i])
                    {
                        res[index++] = num;
                        if (index == k)
                        {
                            break;
                        }
                    }
                }
            }

            return res;
        }
        
        public int[] TopKFrequentV1(int[] nums, int k)
        {
            int[] temp = new int[nums.Length];
            Array.Copy(nums, temp, nums.Length);
            Array.Sort(temp);
            // Console.WriteLine(string.Join(" ", temp));

            int index = -1;
            Dictionary<int, int> cache = new Dictionary<int, int>();
            while (index + 1 < temp.Length)
            {
                int startIndex = index + 1;
                index = BinarySearch(temp, startIndex);
                cache.Add(temp[startIndex], index - startIndex + 1);
            }

            var list = cache.ToArray();
            //Console.WriteLine(string.Join(" ", list));
            Array.Sort(list, (a, b) => { return b.Value - a.Value; });
            //Console.WriteLine(string.Join(" ", list));

            int[] res = new int[k];
            for (int i = 0; i < k; ++i)
            {
                res[i] = list[i].Key;
            }

            return res;
        }

        private int BinarySearch(int[] nums, int start)
        {
            // Try to find the rightmost pos of nums[start]
            int num = nums[start];
            int left = start;
            int right = nums.Length - 1;
            int leftMost = start;
            while (left <= right)
            {
                int middle = (left + right) / 2;
                if (nums[middle] > num)
                {
                    right = middle - 1;
                }
                else if (nums[middle] == num)
                {
                    if (middle > leftMost)
                        leftMost = middle;
                    left = middle + 1;
                }
            }

            return leftMost;
        }

        public int[] TopKFrequentWithHeap(int[] nums, int k)
        {
            Dictionary<int, TempNode> counters = new Dictionary<int, TempNode>();
            for(int i = 0; i < nums.Length; ++i)
            {
                if (counters.TryGetValue(nums[i], out TempNode node))
                    ++node.count;
                else
                {
                    node = new TempNode() { number = nums[i], count = 1 };
                    counters.Add(node.number, node);
                }
            }

            // push into a min heap
            MinHeap<TempNode> minHeap = new MinHeap<TempNode>(k);
            foreach(var node in counters.Values)
            {
                minHeap.Push(node);
            }

            int[] res = new int[k];
            for(int i = 0; i < k; ++i)
            {
                res[i] = minHeap.Pop().number;
            }

            return res;
        }
    }

    public class TempNode : IComparable<TempNode>
    {
        public int number;

        public int count;

        public int CompareTo(TempNode other)
        {
            return this.count - other.count;
        }
    }

    public class MinHeap<T> where T : class, IComparable<T>
    {
        private T[] nums;

        private int last;

        public MinHeap(int size)
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
                    if (this.nums[parent].CompareTo(this.nums[lastChildIndex]) > 0)
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
                return null;
            }

            var currentParentValue = this.Peek();

            T lastNumber = this.nums[--this.last];
            this.nums[0] = lastNumber;

            // sink down
            int parentIndex = 0;
            int leftChildIndex = parentIndex * 2 + 1;
            int rightChildIndex = parentIndex * 2 + 2;
            while (leftChildIndex <= this.last || rightChildIndex <= this.last)
            {
                int smallestIndex = parentIndex;
                if (leftChildIndex <= this.last && this.nums[parentIndex].CompareTo(this.nums[leftChildIndex]) > 0)
                {
                    smallestIndex = leftChildIndex;
                }
                if (rightChildIndex <= this.last && this.nums[smallestIndex].CompareTo(this.nums[rightChildIndex]) > 0)
                {
                    smallestIndex = rightChildIndex;
                }

                if (smallestIndex != parentIndex)
                {
                    // sawp parent and the biggest node(either left or right)
                    T parentValue = this.nums[parentIndex];
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
