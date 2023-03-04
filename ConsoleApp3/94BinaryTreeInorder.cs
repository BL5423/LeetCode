using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp94
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class _94BinaryTreeInorder
    {
        public IList<int> InorderTraversal(TreeNode root)
        {
            var results = new List<int>();
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
                    results.Add(top.val);

                    top = top.right;
                    if (top != null)
                    {
                        stack.Push(top);
                    }
                }
            }

            return results;
        }

        public IList<int> InorderTraversalV1(TreeNode root)
        {
            var results = new List<int>();
            if (root == null)
                return results;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode node = stack.Peek();
                TreeNode left = node.left;
                while (left != null)
                {
                    stack.Push(left);
                    left = left.left;
                }                

                if (stack.Count > 0)
                {
                    // root of the last known left
                    node = stack.Pop();
                    results.Add(node.val);

                    while (node.right == null && stack.Count > 0)
                    {
                        node = stack.Pop();
                        results.Add(node.val);
                    }
                    if (node.right != null)
                    {
                        stack.Push(node.right);
                    }
                }
            }

            return results;
        }

        public IList<int> Morris(TreeNode root)
        {
            var results = new List<int>();
            TreeNode current = root;
            while (current != null)
            {
                if (current.left == null)
                {
                    results.Add(current.val);
                    current = current.right;
                }
                else
                {
                    // move the tree(root of current) to the right-most node of current->left
                    TreeNode pre = current.left;
                    while (pre.right != null)
                    {
                        pre = pre.right;
                    }

                    TreeNode left = current.left;
                    pre.right = current;
                    current.left = null;
                    current = left;
                }
            }

            return results;
        }

        public IList<int> MorrisInPlace(TreeNode root)
        {
            var results = new List<int>();
            TreeNode current = root;
            while (current != null)
            {
                if (current.left == null)
                {
                    results.Add(current.val);
                    current = current.right;
                }
                else
                {
                    // move the tree(root of current) to the right-most node of current->left
                    TreeNode pre = current.left;
                    while (pre.right != null && pre.right != current)
                    {
                        pre = pre.right;
                    }

                    if (pre.right == null)
                    {
                        pre.right = current;
                        current = current.left;
                    }
                    else
                    {
                        // pre.right == current, which means we have processed all the nodes before current
                        // then we can just output current and start with its right node
                        pre.right = null;
                        results.Add(current.val);
                        current = current.right;
                    }
                }
            }

            return results;
        }
    }
}
