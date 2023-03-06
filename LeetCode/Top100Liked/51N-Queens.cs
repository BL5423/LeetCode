using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _51N_Queens
    {
        public IList<IList<string>> SolveNQueens(int n)
        {
            var res = new List<IList<string>>();
            //this.SolveRecursive(res, n, new LinkedList<int>(), new HashSet<string>());
            this.SolveIterative(res, n, new HashSet<string>());
            return res;
        }

        private void SolveIterative(IList<IList<string>> res, int n, HashSet<string> conflicts)
        {
            Stack<Queue> stack = new Stack<Queue>(n);
            stack.Push(new Queue());
            while (stack.Count > 0)
            {
                int row = stack.Count;
                var queue = stack.Peek();

                // cleanup before move to next column on the same row
                var colConflict = string.Concat("col:", queue.col);
                var ldConflict = string.Concat("ld:", row - queue.col);
                var rdConflict = string.Concat("rd:", row + queue.col);
                if (queue.colAdded)
                {
                    conflicts.Remove(colConflict);
                    queue.colAdded = false;
                }
                if (queue.ldAdded)
                {
                    conflicts.Remove(ldConflict);
                    queue.ldAdded = false;
                }
                if (queue.rdAdded)
                {
                    conflicts.Remove(rdConflict);
                    queue.rdAdded = false;
                }

                // move to next column on the same row
                ++queue.col;

                // run out of columns on current row
                if (queue.col == n)
                {
                    stack.Pop();
                }
                else
                {
                    colConflict = string.Concat("col:", queue.col);
                    ldConflict = string.Concat("ld:", row - queue.col);
                    rdConflict = string.Concat("rd:", row + queue.col);
                    queue.colAdded = conflicts.Add(colConflict);
                    queue.ldAdded = conflicts.Add(ldConflict);
                    queue.rdAdded = conflicts.Add(rdConflict);
                    if (queue.colAdded && queue.ldAdded && queue.rdAdded)
                    {
                        if (row == n)
                        {
                            // found a solution
                            var str = new List<string>(n);
                            foreach (var q in stack)
                            {
                                var p1 = new string('.', q.col);
                                var p2 = "Q";
                                var p3 = new string('.', n - q.col - 1);
                                str.Add(string.Concat(p1, p2, p3));
                            }
                            res.Add(str);
                        }
                        else
                        {
                            // move to next row
                            stack.Push(new Queue());
                        }
                    }
                }
            }
        }

        private void SolveRecursive(IList<IList<string>> res, int n, LinkedList<int> pos, HashSet<string> conflicts)
        {
            int row = pos.Count;
            if (row == n)
            {
                var str = new List<string>(n);
                foreach(var p in pos)
                {
                    var p1 = new string('.', p);
                    var p2 = "Q";
                    var p3 = new string('.', n - p - 1);
                    str.Add(string.Concat(p1, p2, p3));
                }
                res.Add(str);
            }
            else
            {
                for (int col = 0; col < n; ++col)
                {
                    string colConflict = string.Concat("col:", col);
                    string leftDiagConflict = string.Concat("ld:", row - col);
                    string rightDiagConflict = string.Concat("rd:", col + row);
                    bool colAllowed = conflicts.Add(colConflict);
                    bool ldAllowed = conflicts.Add(leftDiagConflict);
                    bool rdAllowed = conflicts.Add(rightDiagConflict);
                    if (colAllowed && ldAllowed && rdAllowed)
                    {
                        pos.AddLast(col);
                        this.SolveRecursive(res, n, pos, conflicts);
                        pos.RemoveLast();
                    }

                    if (colAllowed)
                        conflicts.Remove(colConflict);
                    if (ldAllowed)
                        conflicts.Remove(leftDiagConflict);
                    if (rdAllowed)
                        conflicts.Remove(rightDiagConflict);
                }
            }
        }
    }

    public class Queue
    {
        public int col;

        public bool colAdded, ldAdded, rdAdded;

        public Queue()
        {
            this.col = -1;
            this.colAdded = this.ldAdded = this.rdAdded = false;
        }
    }
}
