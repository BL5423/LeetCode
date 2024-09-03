using ConsoleApp2.Level1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class NodeComparor : IComparer<LinkedListNode<Data>>
    {
        public int Compare(LinkedListNode<Data> x, LinkedListNode<Data> y)
        {
            return x.Value.CompareTo(y.Value);
        }
    }

    public class MaxStack
    {
        private LinkedList<Data> stack;

        private SortedSet<LinkedListNode<Data>> bst;

        public MaxStack()
        {
            this.stack = new LinkedList<Data>();
            this.bst = new SortedSet<LinkedListNode<Data>>(new NodeComparor());
        }

        public void Push(int x)
        {
            this.stack.AddLast(new Data(x));
            this.bst.Add(this.stack.Last);
        }

        public int Pop()
        {
            var last = this.stack.Last;
            this.stack.RemoveLast();
            this.bst.Remove(last);

            return last.Value.val;
        }

        public int Top()
        {
            return this.stack.Last().val;
        }

        public int PeekMax()
        {
            return this.bst.Max.Value.val;
        }

        public int PopMax()
        {
            var max = this.bst.Max;
            this.bst.Remove(max);
            this.stack.Remove(max);

            return max.Value.val;
        }
    }

    public class Data : IComparable<Data>
    {
        private static int counter = 0;

        public int val;

        public int seq;

        public Data(int val)
        {
            this.val = val;
            this.seq = ++counter;
        }

        public int CompareTo(Data obj)
        {
            if (this.val != obj.val)
                return this.val - obj.val;
            else
                return this.seq - obj.seq;
        }
    }

    public class MaxHeap<T> where T : class, IComparable<T>
    {
        private T[] nums;

        private int last;

        public MaxHeap(int size)
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
                int biggestIndex = parentIndex;
                if (leftChildIndex <= this.last && this.nums[parentIndex].CompareTo(this.nums[leftChildIndex]) < 0)
                {
                    biggestIndex = leftChildIndex;
                }
                if (rightChildIndex <= this.last && this.nums[biggestIndex].CompareTo(this.nums[rightChildIndex]) < 0)
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

            return currentParentValue;
        }
    }

    public class MaxStackV1
    {
        private HashSet<(int, int)> removedNode;

        private MaxHeap<Data> maxHeap;

        private LinkedList<Data> list;

        public MaxStackV1()
        {
            this.removedNode = new HashSet<(int, int)>();
            this.maxHeap = new MaxHeap<Data>(100000);
            this.list = new LinkedList<Data>();
        }

        public void Push(int x)
        {
            var node = new Data(x);
            this.list.AddLast(node);
            this.maxHeap.Push(node);
        }

        public int Pop()
        {
            var top = this.Top();
            var last = this.list.Last();
            this.removedNode.Add((last.seq, last.val));

            return top;
        }

        public int Top()
        {
            var last = this.list.Last();
            var id = (last.seq, last.val);
            while (this.removedNode.Contains(id))
            {
                this.list.RemoveLast();
                last = this.list.Last();
                id = (last.seq, last.val);
            }

            return last.val;
        }

        public int PeekMax()
        {
            var top = this.maxHeap.Peek();
            var id = (top.seq, top.val);
            while (this.removedNode.Contains(id))
            {
                this.maxHeap.Pop();
                top = this.maxHeap.Peek();
                id = (top.seq, top.val);
            }

            return top.val;
        }

        public int PopMax()
        {
            var max = this.PeekMax();
            var top = this.maxHeap.Peek();
            var id = (top.seq, top.val);
            this.removedNode.Add(id);

            return max;
        }
    }
}
