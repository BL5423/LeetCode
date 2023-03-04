using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _230KthSmallestElementinaBST
    {
        public int KthSmallest(TreeNode root, int k)
        {
            if (root != null)
            {
                int numberOfNodes = 0;
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Count > 0)
                {
                    var top = stack.Pop();
                    if (top.right != null)
                    {
                        stack.Push(top.right);
                    }
                    if (top.left != null)
                    {
                        // push a placeholder for the parent node into stack
                        // since it is pushed before its left kid, so once left kid done processing
                        // the placeholder will be processed only once give no pointers to kids
                        stack.Push(new TreeNode(top.val));

                        stack.Push(top.left);
                    }
                    else // top.left == null
                    {
                        // it is time to process top given no left child/branch
                        if (++numberOfNodes == k)
                        {
                            return top.val;
                        }
                    }
                }
            }

            return -1;
        }

        public int KthSmallestV2(TreeNode root, int k)
        {
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                var top = root;
                while (top != null || stack.Count > 0)
                {
                    while (top != null && top.left != null)
                    {
                        stack.Push(top.left);
                        top = top.left;
                    }

                    // after exhaust all left nodes
                    top = stack.Pop();
                    if (--k == 0)
                        return top.val;

                    top = top.right;
                    if (top != null)
                    {
                        stack.Push(top);
                    }
                }
            }

            return -1;
        }

        public int KthSmallestV1(TreeNode root, int k)
        {
            int res = -1;
            InorderRecursive(root, ref k, ref res);

            return res;
        }

        private bool InorderRecursive(TreeNode root, ref int k, ref int res)
        {
            if (root.left != null && InorderRecursive(root.left, ref k, ref res))
            {
                return true;
            }

            if (--k == 0)
            {
                res = root.val;
                return true;
            }

            if (root.right != null && InorderRecursive(root.right, ref k, ref res))
            {
                return true;
            }

            return false;
        }
    }
}
