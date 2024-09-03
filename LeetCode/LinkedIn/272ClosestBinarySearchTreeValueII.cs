using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _272ClosestBinarySearchTreeValueII
    {
        public IList<int> ClosestKValues(TreeNode root, double target, int k)
        {
            LinkedList<int> res = new LinkedList<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                if (node.right != null)
                    stack.Push(node.right);

                if (node.left != null)
                {
                    stack.Push(new TreeNode(node.val)); // placeholder
                    stack.Push(node.left);
                }
                else
                {
                    if (res.Count < k)
                        res.AddLast(node.val);
                    else // res.Count == k
                    {
                        double diff1 = Math.Abs(res.First() - target);
                        double diff2 = Math.Abs(target - node.val);
                        if (diff1 > diff2)
                        {
                            res.RemoveFirst();
                            res.AddLast(node.val);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return res.ToList();
        }
    }
}
