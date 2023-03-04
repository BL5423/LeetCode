using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _232ImplementQueueUsingStacks
    {
        public class MyQueue
        {
            Stack<int> inStack;
            Stack<int> outStack;

            public MyQueue()
            {
                this.inStack = new Stack<int>();
                this.outStack = new Stack<int>();
            }

            public void Push(int x)
            {
                this.inStack.Push(x);
            }

            public int Pop()
            {
                if (this.outStack.Count == 0)
                {
                    while (this.inStack.Count > 0)
                    {
                        this.outStack.Push(this.inStack.Pop());
                    }
                }

                if (this.outStack.Count > 0)
                {
                    return this.outStack.Pop();
                }

                return -1;
            }

            public int Peek()
            {
                if (this.outStack.Count == 0)
                {
                    while (this.inStack.Count > 0)
                    {
                        this.outStack.Push(this.inStack.Pop());
                    }
                }

                if (this.outStack.Count > 0)
                {
                    return this.outStack.Peek();
                }

                return -1;
            }

            public bool Empty()
            {
                return this.inStack.Count == 0 && this.outStack.Count == 0;
            }
        }
    }
}
