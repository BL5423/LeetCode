using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class MinStack
    {
        Stack<int> stack;

        Stack<int> mins;

        public MinStack()
        {
            this.stack = new Stack<int>();
            this.mins = new Stack<int>();
            this.mins.Push(int.MaxValue);
        }

        public void Push(int val)
        {
            this.stack.Push(val);

            var min = this.mins.Peek();
            if (min >= val)
            {
                this.mins.Push(val);
            }
        }

        public void Pop()
        {
            var top = this.stack.Pop();
            if (top == this.mins.Peek())
                this.mins.Pop();
        }

        public int Top()
        {
            return this.stack.Peek();
        }

        public int GetMin()
        {
            return this.mins.Peek();
        }
    }

    public class MinStackV1
    {
        int[] buffer;

        int topIndex = -1;

        IList<int> minsIndex;

        long currentMin = long.MaxValue;

        public MinStackV1()
        {
            buffer = new int[30000];
            minsIndex = new List<int>();
        }

        public void Push(int val)
        {
            ++this.topIndex;
            this.buffer[this.topIndex] = val;
            if (val < this.currentMin)
            {
                this.currentMin = val;
                this.minsIndex.Add(this.topIndex);
            }
        }

        public void Pop()
        {
            if (this.topIndex >= 0)
            {
                if (this.topIndex == this.minsIndex.Last())
                {
                    // pop curret min
                    this.minsIndex.RemoveAt(this.minsIndex.Count - 1);
                    if (this.minsIndex.Count > 0)
                    {
                        this.currentMin = this.GetMin();
                    }
                    else
                    {
                        this.currentMin = long.MaxValue;
                    }
                }

                --this.topIndex;
            }
        }

        public int Top()
        {
            return this.buffer[this.topIndex];
        }

        public int GetMin()
        {
            return this.buffer[this.minsIndex.Last()];
        }
    }
}
