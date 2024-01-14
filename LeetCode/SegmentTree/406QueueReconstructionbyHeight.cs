using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.SegmentTree
{
    public class _406QueueReconstructionbyHeight
    {
        public int[][] ReconstructQueue(int[][] people)
        {
            // start with the shortest person,
            // if some of them share same height, then start with the one who's position is behind others
            // the reason behind this is that we need to keep empty slots as many as possible so that we can hold everyone
            // the algo is based on the assumtion there is no invalid input
            Array.Sort(people, (a, b) => (a[0] != b[0] ? a[0] - b[0] : b[1] - a[1]));
            var res = new int[people.Length][];
            foreach(var p in people)
            {
                int index = 0, slotsNeeded = p[1], slotsEmpty = 0;
                for (; index < res.Length && slotsEmpty < slotsNeeded; ++index)
                {
                    // find the index ahead of which there are enough empty slots
                    if (res[index] == null)
                    {
                        ++slotsEmpty;
                    }
                }

                // once we got enough empty slots infront, we find the first empty slot to place the person
                for(; index < res.Length; ++index)
                {
                    if (res[index] == null)
                        break;
                }

                res[index] = p;
            }

            return res;
        }

        public int[][] ReconstructQueueV1(int[][] people)
        {
            Array.Sort(people, (a, b) => (a[0] != b[0] ? a[0] - b[0] : b[1] - a[1]));

            var res = new int[people.Length][];
            var tree = new SegmentTreePos(people.Length);
            var map = new Dictionary<int, int[]>();
            for (int i = 0; i < people.Length; ++i)
            {
                var index = tree.QueryAndUpdate(people[i][1]);
                map.Add(index, people[i]);
            }

            var list = tree.Preorder();
            int k = 0;
            foreach (var item in list)
            {
                res[k++] = map[item];
            }

            return res;
        }
    }

    public class SegmentTreePos
    {
        private int[] treeNodes;

        private int offset;

        public SegmentTreePos(int n)
        {
            this.treeNodes = new int[n * 2];
            this.offset = n;
            this.Build();
        }

        private void Build()
        {
            for (int i = 0; i < this.offset; ++i)
                this.treeNodes[i + this.offset] = 1;

            for (int j = this.offset - 1; j > 0; --j)
                this.treeNodes[j] = this.treeNodes[j * 2] + this.treeNodes[j * 2 + 1];
        }

        public int QueryAndUpdate(int val)
        {
            int pos = 1;
            while (pos < this.treeNodes.Length)
            {
                int left = pos * 2;
                int right = pos * 2 + 1;

                if (left >= this.treeNodes.Length)
                {
                    --this.treeNodes[pos];
                    break;
                }

                if (this.treeNodes[left] - val > 0)
                {
                    --this.treeNodes[pos];
                    pos = left;
                }
                else
                {
                    --this.treeNodes[pos];
                    val -= this.treeNodes[left];
                    pos = right;
                }
            }

            return pos;
        }

        public IList<int> Preorder()
        {
            var res = new List<int>(this.offset);
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            while (stack.Count != 0)
            {
                int nodeIndex = stack.Pop();
                int left = nodeIndex * 2;
                int right = nodeIndex * 2 + 1;
                if (left >= this.treeNodes.Length)
                {
                    // leaf node
                    res.Add(nodeIndex);
                }
                else
                {
                    stack.Push(right);
                    stack.Push(left);
                }
            }

            return res;
        }
    }
}
