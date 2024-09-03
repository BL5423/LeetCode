using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _895MaximumFrequencyStack
    {
        public class FreqStack
        {
            private Dictionary<int, Stack<int>> freq2Nums;

            private Dictionary<int, int> numsFreq;

            private int maxFreq;

            public FreqStack()
            {
                this.freq2Nums = new();
                this.numsFreq = new();
                this.maxFreq = 0;
            }

            public void Push(int val)
            {
                if (!this.numsFreq.TryGetValue(val, out int freq))
                {
                    freq = 0;
                    this.numsFreq.Add(val, freq);
                }
                freq = ++this.numsFreq[val];
                this.maxFreq = Math.Max(freq, this.maxFreq);

                if (!this.freq2Nums.TryGetValue(freq, out Stack<int> stack))
                {
                    stack = new Stack<int>();
                    this.freq2Nums.Add(freq, stack);
                }
                stack.Push(val);
            }

            public int Pop()
            {
                var stack = this.freq2Nums[this.maxFreq];
                var val = stack.Pop();
                if (stack.Count == 0)
                    --this.maxFreq;

                --this.numsFreq[val];

                return val;
            }
        }

        public class FreqStackV1
        {
            private PriorityQueue<LinkedListNode<int>, int[]> maxHeap;

            private LinkedList<int> list;

            private Dictionary<int, int> numsFreq;

            private int seq;

            public FreqStackV1()
            {
                this.maxHeap = new PriorityQueue<LinkedListNode<int>, int[]>(new Comparer());
                this.list = new();
                this.numsFreq = new();
            }

            public void Push(int val)
            {
                this.list.AddLast(val);
                if (!this.numsFreq.TryGetValue(val, out int freq))
                {
                    freq = 0;
                    this.numsFreq.Add(val, freq);
                }

                freq = ++this.numsFreq[val];
                this.maxHeap.Enqueue(this.list.Last, new int[] { -freq, -++this.seq });
            }

            public int Pop()
            {
                var mostFreqNumNode = this.maxHeap.Dequeue();
                var val = mostFreqNumNode.Value;
                --this.numsFreq[val];
                this.list.Remove(mostFreqNumNode);
                return val;
            }
        }

        public class Comparer : IComparer<int[]>
        {
            public int Compare(int[] x, int[] y)
            {
                var c = x[0].CompareTo(y[0]);
                return c != 0 ? c : x[1].CompareTo(y[1]);
            }
        }
    }
}
