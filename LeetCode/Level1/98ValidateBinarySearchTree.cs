using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
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

    public class Wrapper
    {
        public TreeNode Node;

        public bool LeftProcessed;

        public Wrapper(TreeNode node, bool leftProcessed)
        {
            this.Node = node;
            this.LeftProcessed = leftProcessed;
        }
    }


    public class _98ValidateBinarySearchTree
    {
        public bool IsValidBST(TreeNode root)
        {
            return InOrderIterativeV2(root);
        }

        private bool InOrder(TreeNode root, ref long lastVal)
        {
            if (root == null)
                return true;

            if (!InOrder(root.left, ref lastVal))
                return false;

            if (lastVal >= root.val)
                return false;

            lastVal = root.val;

            if (!InOrder(root.right, ref lastVal))
                return false;

            return true;
        }

        private bool InOrderIterative(TreeNode root)
        {
            if (root == null)
                return true;

            int? preVal = null;
            Stack<Wrapper> stack = new Stack<Wrapper>();
            stack.Push(new Wrapper(root, false));
            while (stack.Count > 0)
            {
                var top = stack.Peek();
                if (!top.LeftProcessed)
                {
                    if (top.Node.left != null)
                    {
                        stack.Push(new Wrapper(top.Node.left, false));
                    }

                    top.LeftProcessed = true;
                }
                else
                {
                    if (preVal.HasValue && top.Node.val <= preVal)
                        return false;

                    preVal = top.Node.val;
                    stack.Pop();

                    if (top.Node.right != null)
                    {
                        stack.Push(new Wrapper(top.Node.right, false));
                    }
                }
            }

            return true;
        }

        private bool InOrderIterativeV2(TreeNode root)
        {
            if (root == null)
                return true;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            int? lastVal = null;
            do
            {
                var node = root;
                while (node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }

                var top = stack.Pop();
                if (lastVal.HasValue && top.val <= lastVal)
                    return false;

                lastVal = top.val;
                root = top.right;
            }
            while (root != null || stack.Count > 0);

            return true;
        }
    }
}
