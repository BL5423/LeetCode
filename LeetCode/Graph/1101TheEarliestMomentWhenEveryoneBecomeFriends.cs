using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _1101TheEarliestMomentWhenEveryoneBecomeFriends
    {
        public int EarliestAcq(int[][] logs, int n)
        {
            int connected = 0;            
            var dju = new DJU(n);

            var minHeap = new MinHeap<Log>(logs.Length);
            foreach(var log in logs)
            {
                minHeap.Push(new Log(log));
            }

            while (minHeap.Size() != 0)
            {
                var log = minHeap.Pop().log;
                if (dju.Union(log[1], log[2]))
                {
                    ++connected;
                }

                if (connected == n - 1)
                    return log[0];
            }

            return -1;
        }
    }

    public class DJU
    {
        private int[] parents, ranks;

        public DJU(int n)
        {
            this.parents = new int[n];
            this.ranks = new int[n];
            for(int i = 0; i < n; ++i)
            {
                this.parents[i] = i;
                this.ranks[i] = 1;
            }
        }

        public bool Union(int index1, int index2)
        {
            int parent1 = this.GetParent(index1);
            int parent2 = this.GetParent(index2);

            if (parent1 == parent2)
                return false;

            if (this.ranks[parent1] > this.ranks[parent2])
            {
                this.parents[parent2] = parent1;
            }
            else if (this.ranks[parent1] < this.ranks[parent2])
            {
                this.parents[parent1] = parent2;
            }
            else
            {
                this.parents[parent2] = parent1;
                ++this.ranks[parent1];
            }

            return true;
        }
        private int GetParent(int index)
        {
            if (this.parents[index] == index)
                return index;

            return this.parents[index] = this.GetParent(this.parents[index]);
        }
    }

    public class Log : IComparable<Log>
    {
        public int[] log;

        public Log(int[] log)
        {
            this.log = log;
        }

        public int CompareTo(Log other)
        {
            return this.log[0] - other.log[0];
        }
    }
}
