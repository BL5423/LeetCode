using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public enum Flag
    {
        Root,

        LeftBoundary,

        RightBoundary,

        Middle
    }

    public class _545BoundaryofBinaryTree
    {
        public IList<int> BoundaryOfBinaryTree(TreeNode root)
        {
            var res = new List<int>();

            if (root != null)
            {
                var left = new LinkedList<int>();
                var leaves = new LinkedList<int>();
                var right = new LinkedList<int>();

                Stack<(TreeNode, Flag)> stack = new Stack<(TreeNode, Flag)>();
                stack.Push((root, Flag.Root));

                while (stack.Count > 0)
                {
                    var top = stack.Pop();
                    var node = top.Item1;
                    var flag = top.Item2;

                    if (node.left == null && node.right == null)
                    {
                        leaves.AddLast(node.val);
                    }
                    else
                    {
                        if (flag == Flag.LeftBoundary || flag == Flag.Root)
                            left.AddLast(node.val);
                        else if (flag == Flag.RightBoundary)
                            right.AddFirst(node.val);

                        if (node.right != null)
                            stack.Push((node.right, this.GetRightChildFlag(node, flag)));
                        if (node.left != null)
                            stack.Push((node.left, this.GetLeftChildFlag(node, flag)));
                    }
                }

                res.AddRange(left);
                res.AddRange(leaves);
                res.AddRange(right);
            }

            return res;
        }

        private Flag GetLeftChildFlag(TreeNode parent, Flag parentFlag)
        {
            if (parentFlag == Flag.Root)
                return Flag.LeftBoundary;
            else if (parentFlag == Flag.LeftBoundary)
                return Flag.LeftBoundary;
            else if (parentFlag == Flag.RightBoundary && parent.right == null)
                return Flag.RightBoundary;

            return Flag.Middle;
        }

        private Flag GetRightChildFlag(TreeNode parent, Flag parentFlag)
        {
            if (parentFlag == Flag.Root)
                return Flag.RightBoundary;
            else if (parentFlag == Flag.RightBoundary)
                return Flag.RightBoundary;
            else if (parentFlag == Flag.LeftBoundary && parent.left == null)
                return Flag.LeftBoundary;

            return Flag.Middle;
        }


        public IList<int> BoundaryOfBinaryTree_Divide(TreeNode root)
        {
            var res = new List<int>();
            if (root != null)
            {
                res.Add(root.val);

                if (root.left != null)
                    this.GetLeftReview(root.left, res);

                this.GetLeaves(root, res);

                if (root.right != null)
                    this.GetRightReview(root.right, res);
            }

            return res;
        }

        private void GetLeftReview(TreeNode node, IList<int> res)
        {
            var queue = new Queue<TreeNode>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                var head = queue.Peek();
                if (head.left != null || head.right != null)
                    res.Add(head.val);
                else
                    break;

                int count = queue.Count;
                while (--count >= 0)
                {
                    head = queue.Dequeue();
                    if (head.left != null)
                        queue.Enqueue(head.left);
                    if (head.right != null)
                        queue.Enqueue(head.right);
                }
            }
        }

        private void GetRightReview(TreeNode node, List<int> res)
        {
            var right = new LinkedList<int>();
            var queue = new Queue<TreeNode>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                TreeNode head = null;
                int count = queue.Count;
                while (--count >= 0)
                {
                    head = queue.Dequeue();
                    if (head.left != null)
                        queue.Enqueue(head.left);
                    if (head.right != null)
                        queue.Enqueue(head.right);
                }

                if (head.left != null || head.right != null)
                    right.AddFirst(head.val);
                else
                    break;
            }

            res.AddRange(right);
        }

        private void GetLeaves(TreeNode root, IList<int> res)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            if (root.right != null)
                stack.Push(root.right);
            if (root.left != null)
                stack.Push(root.left);

            while (stack.Count > 0)
            {
                var top = stack.Pop();
                if (top.left == null && top.right == null)
                    res.Add(top.val);
                else
                {
                    if (top.right != null)
                        stack.Push(top.right);
                    if (top.left != null)
                        stack.Push(top.left);
                }
            }            
        }
    }
}
